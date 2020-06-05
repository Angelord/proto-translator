using System;
using System.Collections.Generic;
using ProtoTranslator.Lexer;
using ProtoTranslator.Lexer.Tokens;
using ProtoTranslator.Parsing.Exceptions;
using ProtoTranslator.Parsing.Nodes;
using ProtoTranslator.Parsing.Nodes.Expressions;
using ProtoTranslator.Parsing.Nodes.Statements;

namespace ProtoTranslator.Parsing {
    // Reads a stream of tokens and builds a syntax tree.
    public class Parser {

        private LexicalAnalyser lexer;
        private Token look;
        private Token prev;
        private SymbolTable symbolTable;
        private int memoryUsed = 0; // Memory used for declarations

        public Parser(LexicalAnalyser lexer) {
            this.lexer = lexer;
            Move();
        }

        public SyntaxTree Parse() {

            return new SyntaxTree(Program());
        }

        private Statement Program() {
            
            symbolTable = new SymbolTable();
            
            Statement root = new SeqStatement(new StandardLibraryDefinitionsStatement(symbolTable), Statements());

            return root;
        }

        private Statement Block() {
            Match('{'); 
            SymbolTable savedTable = symbolTable;
            symbolTable = new SymbolTable(symbolTable);

            Statement statements = Statements();
            
            Match('}');
            symbolTable = savedTable;

            return statements;
        }

        private TypeToken Type() {
            TypeToken type = (TypeToken) look;
            Match(Tags.BASIC_TYPE);
            return type;
        }

        private Statement Statements() {
            if (look == null || look.Tag == '}') return Nodes.Statement.Null;
            return new SeqStatement(Stmt(), Statements());
        }

        private Statement Stmt() {

            switch (look.Tag) {
                case ';':
                    Move();
                    return Nodes.Statement.Null;
                case Tags.IF:
                    return If();
                case Tags.WHILE:
                    return While();
                case Tags.DO:
                    return Do();
                case Tags.BREAK:
                    return Break();
                case '{':
                    return Block();
                case Tags.BASIC_TYPE:
                    return VariableDeclaration();
            }
            
            Statement expressionStmt = new ExpressionStatement(Additive());
            Match(';');

            return expressionStmt;
        }

        private Statement If() {
            
            Match(Tags.IF);
            Match('(');
            Expression condition = Bool();
            Match(')');
            
            Statement ifContents = Stmt();
            if (look.Tag != Tags.ELSE) {
                return new IfStatement(condition, ifContents);
            }

            Match(Tags.ELSE);
            Statement elseContents = Stmt();
            
            return new ElseStatement(condition, ifContents, elseContents);
        }

        private Statement While() {
            WhileStatement whileNode = new WhileStatement();
            Statement stavedStmt = Statement.Enclosing;
            Statement.Enclosing = whileNode;
            
            Match(Tags.WHILE);
            Match('(');
            Expression condition = Bool();
            Match(')');
            
            whileNode.Init(condition, Stmt());
            Nodes.Statement.Enclosing = stavedStmt; // Reset enclosing statement

            return whileNode;
        }

        private Statement Do() {
            DoStatement doNode = new DoStatement();
            Statement saved = Statement.Enclosing;
            Statement.Enclosing = doNode;
            
            Match(Tags.DO);
            Statement doContents = Stmt();
            Match(Tags.WHILE);
            Match('(');
            Expression condition = Bool();
            Match(')');
            Match(';');
            doNode.Init(condition, doContents);

            Statement.Enclosing = saved;    // Reset enclosing statement
            
            return doNode;
        }

        private Statement Break() {
            Match(Tags.BREAK);
            Match(';');
            return new BreakStatement();
        }

        private Statement VariableDeclaration() {

            // TODO : multiple declarations style int a, b, c;

            TypeToken type = Type();
            WordToken identifierToken = look as WordToken;
            Match(Tags.ID);

            Expression initialValue = null;
            if (look.Tag == '=') {
                Match('=');
                initialValue = Bool();
            }

            Match(';');
            
            LocalVariableSymbol symbol = symbolTable.PutVar(identifierToken.Lexeme, type.DotNetType);
            memoryUsed += type.Width;
            
            return new VariableDeclarationStatement(symbol, initialValue);
        }

        private Statement FuncCallStatement() {
            return new FuncCallStatement(FuncCallExpression());
        }

        private Expression Bool() {
            Expression lhs = Join();
            while (look.Tag == Tags.OR) {
                Move();
                lhs = new BinaryOperatorExpression(prev, lhs, Join());
            }

            return lhs;
        }

        private Expression Join() {
            Expression lhs = BitwiseOr();
            while (look.Tag == Tags.AND) {
                Move();
                lhs = new BinaryOperatorExpression(prev, lhs, BitwiseOr());
            }

            return lhs;
        }

        private Expression BitwiseOr() {
            Expression lhs = BitwiseAnd();
            while (look.Tag == '|') {
                Move();
                lhs = new BinaryOperatorExpression(prev, lhs, BitwiseAnd());
            }

            return lhs;
        }

        private Expression BitwiseAnd() {
            Expression lhs = Equality();
            while (look.Tag == '&') {
                Move();
                lhs = new BinaryOperatorExpression(prev, lhs, Equality());
            }

            return lhs;
        }

        private Expression Equality() {
            Expression lhs = Relational();
            while (look.Tag == Tags.EQ || look.Tag == Tags.NE) {
                Move();
                lhs = new ComparisonExpression((prev as WordToken), lhs, Relational());
            }

            return lhs;
        }

        private Expression Relational() {
            Expression lhs = Additive();
            switch (look.Tag) {
                case Tags.LE:
                case Tags.GE:
                case Tags.LT:
                case Tags.GT:
                    Move();
                    return new ComparisonExpression((prev as WordToken), lhs, Additive());
                default:
                    return lhs;
            }
        }

        private Expression Additive() {
            Expression lhs = Mult();
            while (look.Tag == '+' || look.Tag == '-') {
                Move();
                lhs = new BinaryOperatorExpression(prev, lhs, Mult());
            }

            return lhs;
        }

        private Expression Mult() {
            Expression expr = Unary();
            while (look.Tag == '*' || look.Tag == '/' || look.Tag == '%') {
                Token cur = look;
                Move();
                expr = new BinaryOperatorExpression(cur, expr, Unary());
            }

            return expr;
        }

        private Expression Unary() {
            if (look.Tag == '-') {
                Move();
                return new NegateExpression(Unary());
            }
            if (look.Tag == '!') {
                Move();
                return new NotExpression(Unary());
            }
            if (look.Tag == Tags.INCR) {
                Move();
                Move();
                return UnaryArithmeticExpression.CreatePreIncrement(VariableUseExpression());
            }
            if (look.Tag == Tags.DECR) {
                Move();
                Move();
                return UnaryArithmeticExpression.CreatePreDecrement(VariableUseExpression());
            }

            return Factor();
        }

        private Expression Factor() {
            Expression expr = null;
            switch (look.Tag) {
                case '(':
                    Move();
                    expr = Bool(); 
                    Match(')');
                    return expr;
                case Tags.NUMBER:
                    IntegerToken token = (IntegerToken) look;
                    expr = new IntConstantExpression(token.Value);
                    Move();
                    return expr;
                case Tags.REAL:
                    RealToken realToken = (RealToken) look;
                    expr = new FloatConstantExpression(realToken.Value);
                    Move();
                    return expr;
                case Tags.TRUE:
                    Move(); 
                    return BoolConstantExpression.True;;
                case Tags.FALSE:
                    Move();
                    return BoolConstantExpression.False;
                case Tags.ID:
                    Move();

                    if (look.Tag == '(') {
                        return FuncCallExpression();
                    }
                    else {
                        VariableUseExpression varUseExpr = VariableUseExpression();
                        if (look.Tag == Tags.INCR) {
                            Expression incrExpr = UnaryArithmeticExpression.CreatePostIncrement(varUseExpr);
                            Move();
                            return incrExpr;
                        }
                        if (look.Tag == Tags.DECR) {
                            Expression decrExpr = UnaryArithmeticExpression.CreatePostDecrement(varUseExpr);
                            Move();
                            return decrExpr;
                        }
                        if (look.Tag == '=') {
                            Move();
                            return new VariableAssignExpression(varUseExpr, Bool());
                        }

                        return varUseExpr;
                    }
            }
            
            Error("Syntax error");
            return null;
        }

        private FuncCallExpression FuncCallExpression() {
            WordToken idToken = prev as WordToken;
            
            Match('(');
            Expression[] parameters = Parameters();
            Match(')');
            Type[] parameterTypes = ParseUtils.ExpressionArrayToTypes(parameters);

            FunctionSymbol func = symbolTable.GetFunc(idToken.Lexeme, parameterTypes);
            if(func == null) Error("Undeclared identifier : " + idToken);
                            
            return new FuncCallExpression(func, parameters);
        }

        private VariableUseExpression VariableUseExpression() {
            LocalVariableSymbol var = symbolTable.GetVar((prev as WordToken).Lexeme);
            if(var == null) Error("Undeclared identifier : " + prev);
            return new VariableUseExpression(var);
        }

        // Matches a list of parameters for a function
        private Expression[] Parameters() {

            List<Expression> parameters = new List<Expression>();
            while (look.Tag != ')') {
                parameters.Add(Bool());

                if (look.Tag != ')') {
                    Match(',');
                }
            }

            return parameters.ToArray();
        }

        private void Move() {
            prev = look;   
            look = lexer.ScanToken();
        }

        private void Match(int tag) {
            if (look.Tag == tag) { Move(); return; }
            Error($"Unexpected token '{look.GetLexeme()}', expected '{(char)tag}'");
        }

        private void Error(string message) {
            throw new ParseException(message, look.Line);
        }
    }
}