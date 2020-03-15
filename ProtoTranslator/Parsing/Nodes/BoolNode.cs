using System;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public class BoolNode : LiteralNode {

        private readonly bool value;

        public BoolNode(bool value) { this.value = value; }

        public override Type DetermineType() {
            return typeof(bool);
        }

        public override void Push(CilEmitter emitter) {
            emitter.EmitBool(value);
        }
    }
}