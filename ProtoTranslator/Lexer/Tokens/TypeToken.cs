namespace ProtoTranslator.Lexer.Tokens {
    public class TypeToken : WordToken {

        public static readonly TypeToken Int = new TypeToken(Tags.BASIC_TYPE, "int", 4);
        public static readonly TypeToken Float = new TypeToken(Tags.BASIC_TYPE, "float", 8);
        public static readonly TypeToken Char = new TypeToken(Tags.BASIC_TYPE, "char", 1);
        public static readonly TypeToken Bool = new TypeToken(Tags.BASIC_TYPE, "bool", 1);

        public readonly int Width;
        
        private TypeToken(int tag, string lexeme, int width) : base(tag, lexeme) {
            Width = width;
        }

        public static bool Numeric(TypeToken type) {
            return (type == Int || type == Float || type == Char);
        }

        public static TypeToken Max(TypeToken first, TypeToken second) {
            if (!Numeric(first) || !Numeric(second)) return null;
            if (first == Float || second == Float) return Float;
            if (first == Int || second == Int) return Int;
            return Char;
        }
    }
}