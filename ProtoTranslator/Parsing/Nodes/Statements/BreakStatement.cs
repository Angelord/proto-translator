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

        protected override void DoGenerate(CilEmitter emitter) {
            loopStatement.after.EmitJump();
        }
    }
}