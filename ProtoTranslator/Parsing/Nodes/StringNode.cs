using System;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public class StringNode : Literal {

        private readonly string value;

        public StringNode(string value) {
            this.value = value;
        }

        public override Type DetermineType() {
            return typeof(string);
        }

        public override void Push(CilEmitter emitter) {
            emitter.EmitString(value);
        }
    }
}