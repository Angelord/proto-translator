using System;
using System.Linq.Expressions;
using ProtoTranslator.Generation;
using ProtoTranslator.Lexer;
using ProtoTranslator.Lexer.Tokens;
using ProtoTranslator.Parsing.Exceptions;
using ProtoTranslator.Parsing.Nodes;

namespace ProtoTranslator.Parsing {
    public class Parser {
        
        private LexicalAnalyser lexer;
        private Token lookaheadToken;
        private Token nextToken;
        private SymbolTable symbolTable;
        private CilEmitter emitter;
        
        // TODO : Return syntax tree
        public void Parse(LexicalAnalyser lexer, CilEmitter emitter) {

            this.lexer = lexer;
            this.emitter = emitter;

            symbolTable = new SymbolTable();
            
            lookaheadToken = lexer.ScanToken();
            nextToken = lexer.ScanToken();
            
            do {
                ParseStatement();
            } while (lookaheadToken != null);
        }

        private void Program() {
            Statement stmt = Block();
//            MatCh
        }

        private Statement Block() {
            throw new NotImplementedException();
        }

        private void ParseStatement() {
            if (lookaheadToken.Tag == Tag.Keyword) {
                ParseKeywordStatement();
            }
            else if(lookaheadToken.Tag == Tag.Type) {
                ParseVarDeclaration();
            }
            else if(lookaheadToken.Tag == Tag.Id) {
                ParseIdStatement();
            }
            
            Match(';');
        }

        private void ParseKeywordStatement() {
            WordToken keyword = GetTokenAs<WordToken>();

            if (keyword.Lexeme == Keywords.If) ParseIf();
            else if(keyword.Lexeme == Keywords.While) ParseWhile();
            else if(keyword.Lexeme == Keywords.Break) ParseBreak();
            else if(keyword.Lexeme == Keywords.Continue) ParseContinue();
            else if (keyword.Lexeme == Keywords.Return) ParseReturn();
        }
        
        private void ParseIf() {
            MatchWord(Keywords.If);
            Match('(');
            // Expression
            Match(')');
            Match('{');
            ParseStatement();
            Match('}');
        }

        private void ParseWhile() {
            throw new NotImplementedException();
        }

        private void ParseBreak() {
            throw new NotImplementedException();
        }

        private void ParseContinue() {
            throw new NotImplementedException();
        }

        private void ParseReturn() {
            throw new NotImplementedException();
        }
        
        private void ParseIdStatement() {
            throw new NotImplementedException();
        }

        private void ParseSimpleExpr() {

            if (lookaheadToken.Tag == Tag.Operator) {
                // TODO : Check operator symbol
                emitter.EmitUnaryOp(GetTokenAs<OperatorToken>().OpString);
                ParsePrimaryExpr();
            }
            else {
                ParsePrimaryExpr();
                if (lookaheadToken.Tag == Tag.Operator) {
                    // TODO : Check operator symbol
                    emitter.EmitUnaryOp(GetTokenAs<OperatorToken>().OpString);
                }
            }

        }

        // [14] PrimaryExpr = Constant | Variable | VarIdent [('='|'+='|'-='|'*='|'/='|'%=') Expression] |
        // '*' VarIdent | '&' VarIdent | FuncIdent '(' [Expression] ')' | '(' Expression ')'
        private void ParsePrimaryExpr() {
            
            if (lookaheadToken.Tag == Tag.Float || lookaheadToken.Tag == Tag.Integer) {
                ParseConstant();
            }
            else if (lookaheadToken.Tag == Tag.Operator) {
                throw new NotImplementedException();
            }
            else if (lookaheadToken.Tag == Tag.Id) {
                ParseVariableId();
                if (lookaheadToken.Tag == Tag.Operator) {
                    
                }
            }

        }

        private void ParseVariableId() {
            throw new NotImplementedException();
        }

        private void ParseConstant() {
            throw new NotImplementedException();
        }

        private void ParseVarDeclaration() {
            WordToken typeSpecifier = GetTokenAs<WordToken>();

            Type type = Type.GetType(typeSpecifier.Lexeme);
            Move();
            string name = GetTokenAs<WordToken>().Lexeme;
            emitter.EmitLocalVarDeclaration(name, type);
            
            Move();
            if (lookaheadToken.Tag == Tag.Misc) {
                Match(';');
            }
            else if(lookaheadToken.Tag == Tag.Operator) {
                // TODO : Handle assignment
            }
        }
        
        // Called when beginning a new scope
        private void PushSymTable() {
            symbolTable = new SymbolTable(symbolTable);
        }

        // Called when leaving a scope
        private void PopSymTable() {
            symbolTable = symbolTable.Prev;
        }
        
        private void Move() {
            lookaheadToken = nextToken;
            if (lookaheadToken != null) { nextToken = lexer.ScanToken(); }
        }

        private T GetTokenAs<T>() where T : Token {
            return lookaheadToken as T;
        }

        private T GetNextTokenAs<T>() where T : Token {
            return nextToken as T;
        }

        private void Match(char value) {
            MiscToken misc = lookaheadToken as MiscToken;

            if (misc == null || misc.Symbol != value) {
                throw new ParseException($"Unexpected token {misc}, expecting {value}", lookaheadToken.Line);
            }
            Move();
        }

        private void MatchWord(string lexeme) {
            WordToken word = lookaheadToken as WordToken;
            
            if (word == null || word.Lexeme != lexeme) {
                throw new ParseException($"Unexpected token {word}, expecting {lexeme}", lookaheadToken.Line);
            }
            Move();
        }
    }
}