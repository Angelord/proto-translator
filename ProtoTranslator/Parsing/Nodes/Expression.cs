using System;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public abstract class Expression : Node {

        public abstract Type DetermineType();
        
        public abstract Expression GetLValue(CilEmitter builder);

        public abstract Expression GetRValue(CilEmitter builder);
        
        public abstract void Push(CilEmitter emitter);
    }
}