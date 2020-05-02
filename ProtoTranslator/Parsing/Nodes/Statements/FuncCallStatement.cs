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
        }

        public override void Log(Logger logger) {
            logger.LogLine("Function call");
            logger.IncreaseIndent();
            callExpr.Log(logger);
            logger.DecreaseIndent();
        }
    }
}