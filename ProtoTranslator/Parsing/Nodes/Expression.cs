using System;
using System.Reflection;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public abstract class Expression : Node {

        protected SymbolTable SymTable => null;
        
        public abstract Type DetermineType();
        
        public abstract LValue GetLValue(CilEmitter emitter);

        public abstract Expression EmitRValue(CilEmitter emitter);
    }
}