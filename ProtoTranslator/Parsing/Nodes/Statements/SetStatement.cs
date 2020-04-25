using ProtoTranslator.Debug;
using ProtoTranslator.Generation;
using ProtoTranslator.Parsing.Nodes.Expressions;

namespace ProtoTranslator.Parsing.Nodes {
    // Implements assignment to identifiers
    public class SetStatement : Statement {

        private readonly IdExpression idExpr;
        private readonly Expression valueExpr;

        public SetStatement(IdExpression idExpr, Expression valueExpr) {
            this.idExpr = idExpr;
            this.valueExpr = valueExpr;
         
            CheckTypes();
        }

        private void CheckTypes() {
            if(TypeUtils.Numeric(idExpr.Type) && TypeUtils.Numeric(valueExpr.Type)) return;
            if(idExpr.Type == typeof(bool) && valueExpr.Type == typeof(bool)) return;
            Error("Type Error");
        }

        public override void Generate(CilEmitter emitter, ILabel begin, ILabel after) {
            idExpr.Declare(emitter);
            valueExpr.EmitRValue(emitter);
            idExpr.EmitAssignment(emitter);
        }

        public override void Log(Logger logger) {
            logger.LogLine("Assignment");
            logger.IncreaseIndent();
            idExpr.Log(logger);
            valueExpr.Log(logger);
            logger.DecreaseIndent();
        }
    }
}