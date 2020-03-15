using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public abstract class LValue {

        public abstract void EmitAssignment(CilEmitter emitter);
    }
}