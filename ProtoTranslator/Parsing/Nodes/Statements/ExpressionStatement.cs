using ProtoTranslator.Debug;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes.Statements {
    public class ExpressionStatement : Statement {

        private readonly Expression expression;
        
        public ExpressionStatement(Expression expression) {
            this.expression = expression;
        }

        protected override void DoGenerate(CilEmitter emitter) {
            
            expression.EmitRValue(emitter);

            if (expression.ReturnType != typeof(void)) { emitter.EmitPop(); }
        }

        public override void Log(ILogger logger) {
            expression.Log(logger);
        }
    }
}