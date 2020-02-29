namespace ProtoTranslator.Lexer.Tokens {
    public class FloatToken : Token {

        public readonly float Value;

        public FloatToken(float value) : base(Tag.Float) {
            Value = value;
        }
        
        public override string ToString() {
            return $"<{Tag}, {Value}>";
        }
    }
}