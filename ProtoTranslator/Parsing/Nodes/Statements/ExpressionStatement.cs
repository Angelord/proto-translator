using ProtoTranslator.Debug;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes.Statements {
    public class ExpressionStatement : Statement {

        private readonly Expression expression;
        
        public ExpressionStatement(Expression expression) {
            this.expression = expression;
        }

        public override void Generate(CilEmitter emitter, ILabel begin, ILabel after) {
            
            expression.EmitRValue(emitter);

            if (expression.ReturnType != typeof(void)) { emitter.EmitPop(); }
        }

        public override void Log(ILogger logger) {
            expression.Log(logger);
        }
    }
}