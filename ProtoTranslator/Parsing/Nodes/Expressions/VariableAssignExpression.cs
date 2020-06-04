using ProtoTranslator.Debug;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes.Expressions {
    
    public class VariableAssignExpression : Expression {
        private readonly VariableUseExpression variableUseExpr;
        private readonly Expression valueExpr;

        public VariableAssignExpression(VariableUseExpression variableUseExpr, Expression valueExpr) : base(variableUseExpr.ReturnType) {
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
        
        public override void EmitRValue(CilEmitter emitter) {
            valueExpr.EmitRValue(emitter);
            if (valueExpr.ReturnType == typeof(string)) { emitter.EmitParse(variableUseExpr.ReturnType); }
            
            variableUseExpr.EmitAssignment();
            variableUseExpr.EmitRValue(emitter);
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