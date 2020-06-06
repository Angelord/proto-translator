using ProtoTranslator.Debug;
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

        protected override void DoGenerate(CilEmitter emitter) {

            ILabel doEnd = emitter.GenerateLabel();
            
            statement.Generate(emitter, begin, doEnd);
            
            doEnd.Emit();
            
            logicalExpr.EmitRValue(emitter);
            
            begin.EmitJumpIfTrue();
        }

        public override void Log(ILogger logger) {
            logger.LogLine("Do Statement");
            logger.IncreaseIndent();
                logger.LogLine("Statement : ");
                statement.Log(logger);
                logger.LogLine("Condition : ");
                logicalExpr.Log(logger);
            logger.DecreaseIndent();
        }
    }
}