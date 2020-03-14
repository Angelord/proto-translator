using System;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    
    public abstract class Statement : Node {
        
        public abstract void Generate(CilEmitter emitter);
    }
}