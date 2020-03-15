using System.Reflection.Emit;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public class IfNode : Statement {

        private readonly Expression condition;
        private readonly Statement statement;
        
        public IfNode(Expression condition, Statement statement) {
            this.condition = condition;
            this.statement = statement;
        }

        public override void Generate(CilEmitter emitter) {
            Expression conditionRVal = condition.GetRValue(emitter);

            Label afterLabel = emitter.GenerateLabel();
            
            conditionRVal.GetRValue(emitter).Push(emitter);
            emitter.EmitIfFalse(afterLabel);

            statement.Generate(emitter);
            
            emitter.EmitLabel(afterLabel);
        }
    }
}