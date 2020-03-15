using System;
using System.Reflection;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public class Expr_Comparison : Expression {

        private readonly string comparisonOp;
        private readonly Expression lhs;
        private readonly Expression rhs;

        // TODO : Make sure both sides are of the same type, if not -> Create cast nodes to handle that.

        public Expr_Comparison(string comparisonOp, Expression lhs, Expression rhs) {
            this.comparisonOp = comparisonOp;
            this.lhs = lhs;
            this.rhs = rhs;
        }

        public override Type DetermineType() {
            return typeof(bool);
        }

        public override LValue GetLValue(CilEmitter emitter) {
            // TODO : Use custom exception type
            throw new InvalidOperationException("Invalid LValue reference.");
        }

        public override Expression EmitRValue(CilEmitter emitter) {
            lhs.EmitRValue(emitter);
            rhs.EmitRValue(emitter);
            
            emitter.EmitComparison(comparisonOp);
            
            return this;
        }
    }
}