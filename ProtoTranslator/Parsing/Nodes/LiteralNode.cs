using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public abstract class LiteralNode : Expression {

        public override Expression GetLValue(CilEmitter builder) { return this; }

        public override Expression GetRValue(CilEmitter builder) { return this; }
    }
}