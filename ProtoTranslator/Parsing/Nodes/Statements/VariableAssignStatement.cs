using ProtoTranslator.Debug;
using ProtoTranslator.Generation;
using ProtoTranslator.Parsing.Nodes.Expressions;

namespace ProtoTranslator.Parsing.Nodes.Statements {
    // Implements assignment to identifiers
    public class VariableAssignStatement : Statement {

        private readonly VariableUseExpression variableUseExpr;
        private readonly Expression valueExpr;

        public VariableAssignStatement(VariableUseExpression variableUseExpr, Expression valueExpr) {
            this.variableUseExpr = variableUseExpr;
            this.valueExpr = valueExpr;
         
            CheckTypes();
        }

        private void CheckTypes() {
            if(TypeUtils.Numeric(variableUseExpr.ReturnType) && TypeUtils.Numeric(valueExpr.ReturnType)) return;
            if(variableUseExpr.ReturnType == typeof(bool) && valueExpr.ReturnType == typeof(bool)) return;
            if(variableUseExpr.ReturnType == typeof(string)) return;
            Error("Type Error");
        }

        public override void Generate(CilEmitter emitter, ILabel begin, ILabel after) {
            valueExpr.EmitRValue(emitter);
            if (valueExpr.ReturnType == typeof(string)) { emitter.EmitParse(variableUseExpr.ReturnType); }

            variableUseExpr.EmitAssignment();
        }

        public override void Log(ILogger logger) {
            logger.LogLine("Assignment");
            logger.IncreaseIndent();
            variableUseExpr.Log(logger);
            valueExpr.Log(logger);
            logger.DecreaseIndent();
        }
    }
}