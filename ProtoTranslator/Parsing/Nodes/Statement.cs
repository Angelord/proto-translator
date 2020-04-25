using ProtoTranslator.Debug;
using ProtoTranslator.Generation;
using ProtoTranslator.Lexer.Tokens;

namespace ProtoTranslator.Parsing.Nodes {

    public class Statement : Node {

        public static readonly Statement Null = new Statement();

        public static Statement Enclosing = Statement.Null; // Used for break stmts

        public virtual void Generate(CilEmitter emitter, ILabel begin, ILabel after) { }
    }
}