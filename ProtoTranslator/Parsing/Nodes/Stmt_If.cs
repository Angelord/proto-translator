using System.Reflection.Emit;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public class Stmt_If : Statement {

        private readonly Expression condition;
        private readonly Statement statement;
        
        public Stmt_If(Expression condition, Statement statement) {
            this.condition = condition;
            this.statement = statement;
        }

        public override void Generate(CilEmitter emitter) {
            Label afterLabel = emitter.GenerateLabel();
            
            condition.EmitRValue(emitter);
            emitter.EmitIfFalse(afterLabel);

            statement.Generate(emitter);
            
            emitter.EmitLabel(afterLabel);
        }
    }
}