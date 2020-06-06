namespace ProtoTranslator.Lexer.Tokens {
    public class WordToken : Token {

        public static readonly WordToken
            And = new WordToken(Tags.AND, "&&"),
            Or = new WordToken(Tags.OR, "||"),
            Eq = new WordToken(Tags.EQ, "=="),
            Ne = new WordToken(Tags.NE, "!="),
            Le = new WordToken(Tags.LE, "<="),
            Ge = new WordToken(Tags.GE, ">="),
            Increment = new WordToken(Tags.INCR, "++"),
            Decrement = new WordToken(Tags.DECR, "--"),
            Minus = new WordToken(Tags.MINUS, "minus"),
            True = new WordToken(Tags.TRUE, "true"),
            False = new WordToken(Tags.FALSE, "false"),
            Temp = new WordToken(Tags.TEMP, "t"),
            PrintF = new WordToken(Tags.ID, "printf"),
            ScanF = new WordToken(Tags.ID, "scanf");
            

        public readonly string Lexeme;

        public WordToken(int tag, string lexeme) : base(tag) {
            Lexeme = lexeme;
        }

        public override string GetLexeme() { 
            return Lexeme;
        }

        public override string ToString() {
            return $"<{Tag}, {Lexeme}>";
        }
    }
}