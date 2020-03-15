using System;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public class Literal_Bool : Expr_Literal {

        private readonly bool value;

        public Literal_Bool(bool value) { this.value = value; }

        public override Type DetermineType() {
            return typeof(bool);
        }

        public override Expression EmitRValue(CilEmitter emitter) {
            emitter.EmitBool(value);
            return this;
        }
    }
}