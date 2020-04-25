using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes.Statements {
    public class BreakStatement : Statement {
        private Statement loopStatement;

        public BreakStatement() {

            if (Statement.Enclosing == Statement.Null) {
                Error("Unenclosed break");
            }
            loopStatement = Statement.Enclosing;
        }

        public override void Generate(CilEmitter emitter, ILabel begin, ILabel after) {
            after.EmitJump();
        }
    }
}