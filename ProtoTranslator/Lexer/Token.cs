namespace ProtoTranslator.Lexer {
    public abstract class Token {

        public readonly Tag Tag;

        protected Token(Tag tag) {
            Tag = tag;
        }
    }
}