using System;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes.Expressions {
    public class NegateExpression : Expression {
        
        private readonly Expression expression;
        
        public NegateExpression(Expression expression) : base(null) {
            this.expression = expression;
            Type = TypeUtils.Max(typeof(int), this.expression.Type);
            
            if(Type == null) Error("Type error"); 
        }
        
        public override void EmitRValue(CilEmitter emitter) {
            
            expression.EmitRValue(emitter);
            
            emitter.EmitUnaryOp("-");
            
            throw new NotImplementedException();
        }

        public override string ToString() {
            return $"- {expression}";
        }
    }
}