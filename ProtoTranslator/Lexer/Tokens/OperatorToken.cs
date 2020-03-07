namespace ProtoTranslator.Lexer.Tokens {
    public class OperatorToken : Token {
        
        public readonly string OpString;

        public OperatorToken(string opString) : base(Tag.Operator) {
            OpString = opString;
        }

        public override string ToString() {
            return $"<{Tag}, {OpString}>";
        }
    }
}