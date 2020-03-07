using System;

namespace ProtoTranslator.Lexer {
    public abstract class Token {

        public readonly Tag Tag;
        public int Line;
        
        protected Token(Tag tag) {
            Tag = tag;
        }
    }
}