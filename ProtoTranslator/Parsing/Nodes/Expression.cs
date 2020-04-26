using System;
using System.Globalization;
using System.Reflection;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public abstract class Expression : Node {
        
        public Type ReturnType { get; protected set; }

        protected Expression(Type returnType) { ReturnType = returnType; }
        
        public abstract void EmitRValue(CilEmitter emitter);
    }
}