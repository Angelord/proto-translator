namespace ProtoTranslator.Lexer {
    public class WordToken : Token {

        public readonly string Lexeme;

        public WordToken(Tag tag, string lexeme) : base(tag) {
            Lexeme = lexeme;
        }
        
        public override string ToString() {
            return $"<{Tag}, {Lexeme}>";
        }
    }
}