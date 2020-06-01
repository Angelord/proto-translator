using System;

namespace ProtoTranslator.Lexer {
    public abstract class Token {

        public readonly int Tag;
        public int Line;
        
        protected Token(int tag) {
            Tag = tag;
        }

        /// <summary> Returns the lexeme the token was matched to, as found in the original source. </summary>
        public virtual string GetLexeme() {
            return char.ToString((char)Tag);
        }
    }
}