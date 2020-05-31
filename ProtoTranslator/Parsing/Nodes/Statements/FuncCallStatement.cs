using ProtoTranslator.Debug;
using ProtoTranslator.Generation;
using ProtoTranslator.Parsing.Nodes.Expressions;

namespace ProtoTranslator.Parsing.Nodes.Statements {
    public class FuncCallStatement : Statement {

        private readonly FuncCallExpression callExpr;

        public FuncCallStatement(FuncCallExpression callExpr) {
            this.callExpr = callExpr;
        }

        public override void Generate(CilEmitter emitter, ILabel begin, ILabel after) {
            callExpr.EmitRValue(emitter);
            if (callExpr.ReturnType != typeof(void)) { emitter.EmitPop(); }
        }

        public override void Log(ILogger logger) {
            logger.LogLine("Function call statement : ");
            logger.IncreaseIndent();
            callExpr.Log(logger);
            logger.DecreaseIndent();
        }
    }
}