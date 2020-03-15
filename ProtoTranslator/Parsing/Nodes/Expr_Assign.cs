using System;
using System.Reflection;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public class Expr_Assign : Expression {
        
        private readonly Expression lhs;
        private readonly Expression rhs;

        public Expr_Assign(Expression lhs, Expression rhs) {
            this.lhs = lhs;
            this.rhs = rhs;
        }

        public override Type DetermineType() {
            return lhs.DetermineType();
        }

        public override LValue GetLValue(CilEmitter emitter) {
            throw new NotImplementedException();
        }

        public override Expression EmitRValue(CilEmitter emitter) {
            LValue lhsLVal = lhs.GetLValue(emitter);
            rhs.EmitRValue(emitter);
            lhsLVal.EmitAssignment(emitter);
            
            return this;
        }
    }
}