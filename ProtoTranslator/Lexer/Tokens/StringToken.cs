namespace ProtoTranslator.Lexer.Tokens {
    
    public class StringToken : Token {

        public readonly string Value;

        public StringToken(string value) : base(Tag.String) {
            Value = value;
        }
        
        public override string ToString() {
            return $"<{Tag}, {Value}>";
        }
    }
}