using System;
using System.Reflection;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    /// <summary>
    /// Represents a call to scanf
    /// </summary>
    public class Expr_Scan : Expression {
        
        public override Type DetermineType() {
            return typeof(string);
        }

        public override LValue GetLValue(CilEmitter emitter) {
            throw new InvalidOperationException();
        }

        public override Expression EmitRValue(CilEmitter emitter) {
            emitter.EmitRead();
            return this;
        }
    }
}