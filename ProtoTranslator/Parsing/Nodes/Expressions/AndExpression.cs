using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes.Expressions {
    public class AndExpression : BinaryLogicalExpression {

        public AndExpression(Expression lhs, Expression rhs) : base(lhs, rhs) {
            if(lhs.ReturnType != typeof(bool) || rhs.ReturnType != typeof(bool)) Error("Type Error! Both sides of && expressions must evaluate to bool!");
        }

        protected override void EmitOperator(CilEmitter emitter) {
            emitter.EmitBinaryOperator("&&");
        }
    }
}