using ProtoTranslator.Debug;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes.Expressions {
    public class BoolConstantExpression : Expression {

        public static readonly BoolConstantExpression True = new BoolConstantExpression(true);
        
        public static readonly BoolConstantExpression False = new BoolConstantExpression(false);
        
        private readonly bool value;

        private BoolConstantExpression(bool value) : base(typeof(bool)) {
            this.value = value;
        }

        public override void EmitRValue(CilEmitter emitter) {
            emitter.EmitBool(value);
        }
        
        public override void Log(Logger logger) {
            logger.LogLine("Bool " + value);
        }
    }
}