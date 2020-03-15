using System;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public class Literal_String : Expr_Literal {

        private readonly string value;

        public Literal_String(string value) {
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