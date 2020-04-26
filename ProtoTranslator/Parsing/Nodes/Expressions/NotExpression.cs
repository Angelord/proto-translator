using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes.Expressions {
    
    public class NotExpression : Expression {
        
        private readonly Expression expression;

        public NotExpression(Expression expression) : base(typeof(bool)) {
            this.expression = expression;
            
            if(expression.ReturnType != typeof(bool)) Error("Type Error");
        }
        
        public override void EmitRValue(CilEmitter emitter) {
            expression.EmitRValue(emitter);
            emitter.EmitUnaryOp("!");
        }
    }
}