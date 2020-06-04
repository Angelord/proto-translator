using ProtoTranslator.Debug;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes.Expressions {
    public class PrefixArithmeticExpression : Expression {

        private readonly VariableUseExpression variableExpr;
        private readonly bool add;

        public static PrefixArithmeticExpression CreateIncrement(VariableUseExpression variableExpr) {
            return new PrefixArithmeticExpression(variableExpr, true);
        }

        public static PrefixArithmeticExpression CreateDecrement(VariableUseExpression variableExpr) {
            return new PrefixArithmeticExpression(variableExpr, false);
        }
        
        private PrefixArithmeticExpression(VariableUseExpression variableExpr, bool add) : base(variableExpr.ReturnType) {
            this.variableExpr = variableExpr;
            this.add = add;

            if (this.variableExpr.ReturnType != typeof(int) && variableExpr.ReturnType != typeof(char)) {
                Error("Invalid expression type to increment!");
            }
        }

        public override void EmitRValue(CilEmitter emitter) {
            variableExpr.EmitRValue(emitter);
            emitter.EmitInt32(1);
            emitter.EmitBinaryOperator(add ? "+" : "-");
            variableExpr.EmitAssignment();
            
            variableExpr.EmitRValue(emitter);
        }

        public override void Log(ILogger logger) {
            logger.LogLine("Prefix Increment");
            logger.IncreaseIndent();
            variableExpr.Log(logger);
            logger.DecreaseIndent();
        }
    }
}