using ProtoTranslator.Debug;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes.Expressions {
    public class FloatConstantExpression : Expression {

        private readonly float value;

        public FloatConstantExpression(float value) : base(typeof(float)) {
            this.value = value;
        }

        public override void EmitRValue(CilEmitter emitter) {
            emitter.EmitDouble(value);
        }
        
        public override void Log(Logger logger) {
            logger.LogLine("Float " + value);
        }
    }
}