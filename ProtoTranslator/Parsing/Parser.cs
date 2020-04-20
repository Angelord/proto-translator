using ProtoTranslator.Lexer;
using ProtoTranslator.Lexer.Tokens;
using ProtoTranslator.Parsing.Exceptions;
using ProtoTranslator.Parsing.Nodes;

namespace ProtoTranslator.Parsing {
    // Reads a stream of tokens and builds a syntax tree.
    public class Parser {
        
        private LexicalAnalyser lexer;
        private Token look;
        private Token prev;
        private SymbolTable top;
        private int memoryUsed = 0; // Memory used for declarations

        public Parser(LexicalAnalyser lexer) {
            this.lexer = lexer;
            Move();
        }

        public SyntaxTree Parse() {
            
            // TODO : Rework to not require { } around entire program
            
            return new SyntaxTree(Block());
        }

        private Statement Block() {
            Match('{'); 
            SymbolTable savedTable = top;
            top = new SymbolTable(top);

            Declarations();
            Statement statements = Statements();
            
            Match('}');
            top = savedTable;

            return statements;
        }

        private void Declarations() {
            while (look.Tag == Tags.BASIC_TYPE) {
                TypeToken type = Type();
                Token curToken = look;
                Match(Tags.ID);
                Match(';');
                
                IdExpr id = new IdExpr((WordToken)curToken, type, memoryUsed);
                top.Put(curToken, id);
                memoryUsed += type.Width;
            }
        }

        private TypeToken Type() {
            TypeToken type = (TypeToken) look;
            Match(Tags.BASIC_TYPE);
            return type;
        }

        private Statement Statements() {
            if (look.Tag == '}') return Nodes.Statement.Null;
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
            }
            
            return Assign();
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

        private Statement Assign() {
            Token curToken = look;
            Match(Tags.ID);
            IdExpr id = top.Get(curToken);
            if(id == null) Error(curToken.ToString() + " undeclared identifier");
            
            Match('=');
            Statement stmt = new SetStatement(id, Bool());
            
            Match(';');
            return stmt;
        }

        private Expression Bool() {
            Expression lhs = Join();
            while (look.Tag == Tags.OR) {
                Move();
                lhs = new OrExpression(prev, lhs, Join());
            }

            return lhs;
        }

        private Expression Join() {
            Expression lhs = Equality();
            while (look.Tag == Tags.AND) {
                Move();
                lhs = new AndExpression(prev, lhs, Equality());
            }

            return lhs;
        }

        private Expression Equality() {
            Expression lhs = Relational();
            while (look.Tag == Tags.EQ || look.Tag == Tags.NE) {
                Move();
                lhs = new RelationalExpr(prev, lhs, Relational());
            }

            return lhs;
        }

        private Expression Relational() {
            Expression lhs = Expr();
            switch (look.Tag) {
                case Tags.LE: 
                case Tags.GE: 
                case '<':
                case '>':   
                    Move();
                    return new RelationalExpr(prev, lhs, Expr());
                default:
                    return lhs;
            }
        }

        private Expression Expr() {
            Expression lhs = Term();
            while (look.Tag == '+' || look.Tag == '-') {
                Move();
                lhs = new ArithmeticExpr(prev, lhs, Term());
            }

            return lhs;
        }

        private Expression Term() {
            Expression expr = Unary();
            while (look.Tag == '*' || look.Tag == '/') {
                Token cur = look;
                Move();
                expr = new ArithmeticExpr(cur, expr, Unary());
            }

            return expr;
        }

        private Expression Unary() {
            if (look.Tag == '-') {
                Move();
                return new UnaryExpr(WordToken.Minus, Unary());
            }
            if (look.Tag == '!') {
                Move();
                return new NotExpression(prev, Unary());
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
                    expr = new ConstantExpr(look, TypeToken.Int);
                    Move();
                    return expr;
                case Tags.REAL:
                    expr = new ConstantExpr(look, TypeToken.Float);
                    Move();
                    return expr;
                case Tags.TRUE:
                    Move(); 
                    return ConstantExpr.True;
                case Tags.FALSE:
                    Move();
                    return ConstantExpr.False;
                case Tags.ID:
                    IdExpr id = top.Get(look);
                    if(id == null) Error(look.ToString() + " undeclared identifier");
                    Move();
                    return id;
            }
            
            Error("Syntax error");
            return null;
        }

        private void Move() {
            prev = look;   
            look = lexer.ScanToken();
        }
        
        private void Error(string message) {
            throw new ParseException(message, look.Line);
        }

        private void Match(int tag) {
            if (look.Tag == tag) { Move(); return; }
            Error("Unexpected token " + look.Tag);
        }
    }
}