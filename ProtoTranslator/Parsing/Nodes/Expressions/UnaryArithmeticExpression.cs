using ProtoTranslator.Debug;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes.Expressions {
    public class UnaryArithmeticExpression : Expression {

        private readonly VariableUseExpression variableExpr;
        private readonly bool prefix;
        private readonly bool add;
        
        public static UnaryArithmeticExpression CreatePostIncrement(VariableUseExpression variableExpr) {
            return new UnaryArithmeticExpression(variableExpr, true, false);
        }
        
        public static UnaryArithmeticExpression CreatePostDecrement(VariableUseExpression variableExpr) {
            return new UnaryArithmeticExpression(variableExpr, false, false);
        }

        public static UnaryArithmeticExpression CreatePreIncrement(VariableUseExpression variableExpr) {
            return new UnaryArithmeticExpression(variableExpr, true, true);
        }

        public static UnaryArithmeticExpression CreatePreDecrement(VariableUseExpression variableExpr) {
            return new UnaryArithmeticExpression(variableExpr, false, true);
        }
        
        private UnaryArithmeticExpression(VariableUseExpression variableExpr, bool add, bool prefix) : base(variableExpr.ReturnType) {
            this.variableExpr = variableExpr;
            this.add = add;
            this.prefix = prefix;

            if (this.variableExpr.ReturnType != typeof(int) && variableExpr.ReturnType != typeof(char)) {
                Error("Invalid expression type to increment!");
            }
        }

        public override void EmitRValue(CilEmitter emitter) {
            if (!prefix) { variableExpr.EmitRValue(emitter); }

            variableExpr.EmitRValue(emitter);
            emitter.EmitInt32(1);
            emitter.EmitBinaryOperator(add ? "+" : "-");
            variableExpr.EmitAssignment();

            if (prefix) { variableExpr.EmitRValue(emitter); }
        }

        public override void Log(ILogger logger) {
            logger.LogLine("Prefix Increment");
            logger.IncreaseIndent();
            variableExpr.Log(logger);
            logger.DecreaseIndent();
        }
    }
}