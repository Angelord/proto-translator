using System;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public class ParseNode : Expression {

        private readonly Type targetType;
        private readonly Expression exprToParse;

        public ParseNode(Type targetType, Expression exprToParse) {
            this.targetType = targetType;
            this.exprToParse = exprToParse;
        }

        public override Type DetermineType() {
            throw new NotImplementedException();
        }

        public override Expression GetLValue(CilEmitter builder) {
            throw new InvalidOperationException("Invalid LValue reference.");
        }

        public override Expression GetRValue(CilEmitter builder) {
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