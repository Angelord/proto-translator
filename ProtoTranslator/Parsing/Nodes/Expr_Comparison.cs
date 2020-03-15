using System;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public class Expr_Comparison : Expression {
        public override Type DetermineType() {
            throw new NotImplementedException();
        }

        public override Expression GetLValue(CilEmitter emitter) {
            throw new NotImplementedException();
        }

        public override Expression GetRValue(CilEmitter emitter) {
            throw new NotImplementedException();
        }

        public override void Push(CilEmitter emitter) {
            throw new NotImplementedException();
        }
    }
}