using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public class DoStatement : Statement {

        private Expression logicalExpr;

        private Statement statement;

        public DoStatement() {
            logicalExpr = null;
            statement = null;
        }

        public void Init(Expression logicalExpr, Statement statement) {
            this.logicalExpr = logicalExpr;
            this.statement = statement;
            if (logicalExpr.ReturnType != typeof(bool)) 
                logicalExpr.Error("Expression in while must evaluate to bool"); 
        }

        public override void Generate(CilEmitter emitter, ILabel begin, ILabel after) {

            ILabel doEnd = emitter.GenerateLabel();
            
            statement.Generate(emitter, begin, doEnd);
            
            doEnd.Emit();
            
            logicalExpr.EmitRValue(emitter);
            
            begin.EmitJumpIfFalse();
        }
    }
}