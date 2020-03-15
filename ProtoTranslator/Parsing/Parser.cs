using System;
using ProtoTranslator.Lexer;
using ProtoTranslator.Lexer.Tokens;
using ProtoTranslator.Parsing.Nodes;

namespace ProtoTranslator.Parsing {
    public class Parser {
        
        private LexicalAnalyser lexer;
        private Statement main;
        
        // TODO : Return syntax tree
        public void Parse(LexicalAnalyser lexer) {

            this.lexer = lexer;

            Token curToken = null;
            do {
                curToken = lexer.ScanToken();
            } while (curToken != null);

//            main = (Statement)ParseToken();

//            IntermediateCodeBuilder builder = new IntermediateCodeBuilder();
//            
//            main.Generate(builder);
//
//            return builder.ToString();
        }

        private Node ParseToken() {

            Token token = lexer.ScanToken();
            
            switch (token.Tag) {
                case Tag.Keyword:
                    return ParseKeyword(token);
            }

            return null;
        }

        private Statement ParseKeyword(Token token) {
            WordToken wordToken = token as WordToken;
            switch (wordToken.Lexeme) {
                case "if":
                    Match(Tag.Misc, '(');
                    Expression cond = ParseToken() as Expression;
                    Match(Tag.Misc, ')');
                    Match(Tag.Misc, '{');
                    Statement stmt = ParseToken() as Statement;
                    Match(Tag.Misc, '}');
                    return new Stmt_If(cond , stmt);
            }

            return null;
        }

        private void Match<T>(Tag tag, T value) {
            Token next = lexer.ScanToken();

            if (next.Tag != tag) {
                
            }
        }
    }
}