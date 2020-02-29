namespace ProtoTranslator.Lexer.Tokens {
    public class RelationalToken : Token {
        
        public readonly string OpString;

        public RelationalToken(string opString) : base(Tag.Relational) {
            this.OpString = opString;
        }

        public override string ToString() {
            return $"<{Tag}, {OpString}>";
        }
    }
}