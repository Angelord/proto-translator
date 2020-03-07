namespace ProtoTranslator.Lexer.Tokens {
    public class RelationalToken : Token {
        
        public readonly string OpString;

        public RelationalToken(string opString) : base(Tag.Relational) {
            OpString = opString;
        }

        public override string ToString() {
            return $"<{Tag}, {OpString}>";
        }
    }
}