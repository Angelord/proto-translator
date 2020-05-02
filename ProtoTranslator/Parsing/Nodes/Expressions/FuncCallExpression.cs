using System.Collections.Generic;
using ProtoTranslator.Debug;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes.Expressions {
    public class FuncCallExpression : Expression {

        private readonly FunctionSymbol symbol;
        private readonly Expression[] parameters;

        public FuncCallExpression(FunctionSymbol symbol, Expression[] parameters) : base(symbol.ReturnType) {
            this.symbol = symbol;
            this.parameters = parameters;
            
            // TODO : Check parameter types against function signature.
        }

        public override void EmitRValue(CilEmitter emitter) {
            foreach (Expression param in parameters) {
                param.EmitRValue(emitter);     // Make sure the parameters are on the stack.
            }
            symbol.Function.EmitCall();
        }

        public override void Log(Logger logger) {
            logger.LogLine("Function call ");
        }
    }
}