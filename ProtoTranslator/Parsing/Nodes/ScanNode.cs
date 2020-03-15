using System;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    /// <summary>
    /// Represents a call to scanf
    /// </summary>
    public class ScanNode : Expression {
        
        public override Type DetermineType() {
            return typeof(string);
        }

        public override Expression GetLValue(CilEmitter builder) {
            throw new InvalidOperationException();
        }

        public override Expression GetRValue(CilEmitter builder) {
            return this;
        }

        public override void Push(CilEmitter emitter) {
            emitter.EmitRead();
        }
    }
}