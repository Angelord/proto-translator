using ProtoTranslator.Debug;
using ProtoTranslator.Generation;
using ProtoTranslator.Lexer.Tokens;

namespace ProtoTranslator.Parsing.Nodes {

    public class Statement : Node {

        public static readonly Statement Null = new Statement();

        public static Statement Enclosing = Statement.Null; // Used for break stmts

        public ILabel begin;

        public ILabel after;

        public void Generate(CilEmitter emitter, ILabel begin, ILabel after) {
            this.begin = begin;
            this.after = after;
            DoGenerate(emitter);
        }

        protected virtual void DoGenerate(CilEmitter emitter) { }
    }
}