using System;

namespace ProtoTranslator.Lexer {
    public abstract class Token {

        public readonly int Tag;
        public int Line;
        
        protected Token(int tag) {
            Tag = tag;
        }
    }
}