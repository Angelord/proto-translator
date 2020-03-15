using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public abstract class Expr_Literal : Expression {

        public override Expression GetLValue(CilEmitter emitter) { return this; }

        public override Expression GetRValue(CilEmitter emitter) { return this; }
    }
}