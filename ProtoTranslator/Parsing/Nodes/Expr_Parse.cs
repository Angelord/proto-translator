using System;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public class Expr_Parse : Expression {

        private readonly Type targetType;
        private readonly Expression exprToParse;

        public Expr_Parse(Type targetType, Expression exprToParse) {
            this.targetType = targetType;
            this.exprToParse = exprToParse;
        }

        public override Type DetermineType() {
            return targetType;
        }

        public override Expression GetLValue(CilEmitter emitter) {
            // TODO : Use custom exception type
            throw new InvalidOperationException("Invalid LValue reference.");
        }

        public override Expression GetRValue(CilEmitter emitter) {
            return this;
        }

        public override void Push(CilEmitter emitter) {
            Expression exprRValue = exprToParse.GetRValue(emitter);

            if (exprRValue.DetermineType() != typeof(string)) {
                // TODO : Throw custom exception
                throw new InvalidOperationException("Invalid parse! The target expression is not of type string!");
            }

            exprRValue.Push(emitter);
            emitter.EmitParse(targetType);
        }
    }
}