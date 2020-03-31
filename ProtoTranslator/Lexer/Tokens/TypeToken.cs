namespace ProtoTranslator.Lexer.Tokens {
    public class TypeToken : WordToken {

        public static readonly TypeToken Int = new TypeToken("int", 4);
        public static readonly TypeToken Float = new TypeToken("float", 8);
        public static readonly TypeToken Char = new TypeToken("char", 1);
        public static readonly TypeToken Bool = new TypeToken("bool", 1);

        public readonly int Width;
        
        public TypeToken(string lexeme, int width) : base(Tag.BasicType, lexeme) {
            Width = width;
        }
    }
}