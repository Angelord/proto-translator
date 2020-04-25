using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes.Expressions {
    // Provides common functionality for Or, And and Relation Operators
    public abstract class BinaryLogicalExpression : Expression {
        
        protected readonly Expression lhs;
        protected readonly Expression rhs;

        public BinaryLogicalExpression(Expression lhs, Expression rhs) : base(typeof(bool)) {
            this.lhs = lhs;
            this.rhs = rhs;
        }
        
        public override void EmitRValue(CilEmitter emitter) {
            lhs.EmitRValue(emitter);
            rhs.EmitRValue(emitter);
            EmitOperator(emitter);
        }

        protected abstract void EmitOperator(CilEmitter emitter);
    }
}