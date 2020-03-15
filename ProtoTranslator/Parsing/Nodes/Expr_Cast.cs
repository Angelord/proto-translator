using System;
using System.Reflection;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public class Expr_Cast : Expression {

        private readonly Type targetType;
        private readonly Expression expressionToCast;
        
        public Expr_Cast(Type targetType, Expression expressionToCast) {
            this.targetType = targetType;
            this.expressionToCast = expressionToCast;
        }
        
        public override Type DetermineType() {
            return targetType;
        }

        public override LValue GetLValue(CilEmitter emitter) {
            // TODO : Use custom exception type
            throw new InvalidOperationException("Invalid LValue reference.");
        }

        public override Expression EmitRValue(CilEmitter emitter) {
            Expression exprRValue = expressionToCast.EmitRValue(emitter);

            emitter.EmitCast(targetType, exprRValue.DetermineType());

            return this;
        }
    }
}