using System;
using System.Reflection;
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

        public override LValue GetLValue(CilEmitter emitter) {
            // TODO : Use custom exception type
            throw new InvalidOperationException("Invalid LValue reference.");
        }

        public override Expression EmitRValue(CilEmitter emitter) {
            Expression exprRValue = exprToParse.EmitRValue(emitter);

            if (exprRValue.DetermineType() != typeof(string)) {
                // TODO : Throw custom exception
                throw new InvalidOperationException("Invalid parse! The target expression is not of type string!");
            }

            emitter.EmitParse(targetType);
            
            return this;
        }
    }
}