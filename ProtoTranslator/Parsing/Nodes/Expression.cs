using System;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public abstract class Expression : Node {

        public abstract Type DetermineType();
        
        public abstract Expression GetLValue(CilEmitter emitter);

        public abstract Expression GetRValue(CilEmitter emitter);
        
        public abstract void Push(CilEmitter emitter);
    }
}