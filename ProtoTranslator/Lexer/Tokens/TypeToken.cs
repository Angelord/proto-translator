using System;

namespace ProtoTranslator.Lexer.Tokens {
    public class TypeToken : WordToken {

        public static readonly TypeToken Int = new TypeToken(Tags.BASIC_TYPE, "int", 4, typeof(int));
        public static readonly TypeToken Float = new TypeToken(Tags.BASIC_TYPE, "float", 8, typeof(float));
        public static readonly TypeToken Char = new TypeToken(Tags.BASIC_TYPE, "char", 1, typeof(char));
        public static readonly TypeToken Bool = new TypeToken(Tags.BASIC_TYPE, "bool", 1, typeof(bool));

        public readonly int Width;

        private Type dotNetType;

        public Type DotNetType => dotNetType;

        private TypeToken(int tag, string lexeme, int width, Type dotNetType) : base(tag, lexeme) {
            Width = width;
            this.dotNetType = dotNetType;
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