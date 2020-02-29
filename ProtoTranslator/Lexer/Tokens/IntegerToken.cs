namespace ProtoTranslator.Lexer.Tokens {
    public class IntegerToken : Token {

        public readonly int Value;
        
        public IntegerToken(int value) : base(Tag.Integer) {
            Value = value;
        }
        
        public override string ToString() {
            return $"<{Tag}, {Value}>";
        }
    }
}