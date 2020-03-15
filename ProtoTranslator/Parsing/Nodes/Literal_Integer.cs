using System;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public class Literal_Integer : Expr_Literal {

        private readonly int value;
        
        public Literal_Integer(int value) { this.value = value; }

        public override Type DetermineType() {
            return typeof(bool);
        }
        
        public override Expression EmitRValue(CilEmitter emitter) {
            emitter.EmitInt32(value);
            return this;
        }
    }
}