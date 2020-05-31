namespace ProtoTranslator.Lexer.Tokens {
    // Token representing integers
    public class IntegerToken : Token {

        public readonly int Value;
        
        public IntegerToken(int value) : base(Tags.NUMBER) {
            Value = value;
        }

        public override string GetLexeme() {
            return Value.ToString();
        }

        public override string ToString() {
            return $"<{Tag}, {Value}>";
        }
    }
}