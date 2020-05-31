using ProtoTranslator.Debug;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes.Expressions {
    public class IntConstantExpression : Expression {

        private readonly int value;
        
        public IntConstantExpression(int value) : base(typeof(int)) {
            this.value = value;
        }

        public override void EmitRValue(CilEmitter emitter) {
            emitter.EmitInt32(value);   
        }

        public override void Log(ILogger logger) {
            logger.LogLine("Integer " + value);
        }
    }
}