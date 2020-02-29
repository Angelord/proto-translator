namespace ProtoTranslator.Lexer.Tokens {
    public class NumberToken : Token {

        public readonly int Value;
        
        public NumberToken(int value) : base(Tag.Num) {
            Value = value;
        }
        
        public override string ToString() {
            return $"<{Tag}, {Value}>";
        }
    }
}