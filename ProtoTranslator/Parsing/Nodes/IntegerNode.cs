using System;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public class IntegerNode : Literal {

        private readonly int value;
        
        public IntegerNode(int value) { this.value = value; }

        public override Type DetermineType() {
            return typeof(bool);
        }

        public override void Push(CilEmitter emitter) {
            emitter.EmitInt32(value);
        }
    }
}