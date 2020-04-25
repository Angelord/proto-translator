using System;
using System.Globalization;
using System.Reflection;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public abstract class Expression : Node {
        
        public Type Type { get; protected set; }

        protected Expression(Type type) { Type = type; }
        
        public abstract void EmitRValue(CilEmitter emitter);
    }
}