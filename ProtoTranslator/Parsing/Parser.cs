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
//                ParseStatement();
            } while (lookaheadToken != null);
        }

        private void Program() {
            Statement stmt = Block();
            
        }

        private Statement Block() {
            Match('{');
            SymbolTable savedTable = symbolTable;
            symbolTable = new SymbolTable(savedTable);
            
            Statement stmts = Statements();
            
            Match('}');
            symbolTable = savedTable;

            return stmts;
        }

        private Statement Statements() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generates symbol table entries for identifiers.
        /// </summary>
        private void Declarations() {
            while (lookaheadToken.Tag == Tag.Misc) {
                
            }
        }

        private void Move() {
            lookaheadToken = nextToken;
            if (lookaheadToken != null) { nextToken = lexer.ScanToken(); }
        }

        private void Error(string message) {
            throw new ParseException(message, lookaheadToken.Line);
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