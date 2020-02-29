namespace ProtoTranslator.Lexer.Tokens {
    public class MiscToken : Token {

        public readonly char Symbol;
        
        public MiscToken(char symbol) : base(Tag.Misc) {
            Symbol = symbol;
        }

        public override string ToString() {
            return $"<{Symbol}>";
        }
    }
}