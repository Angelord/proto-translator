using System.Text;

namespace ProtoTranslator.Parsing {
    public class If : Statement {

        private readonly Expression condition;
        private readonly Statement statement;
        private readonly string afterLabel;
        
        public If(Expression condition, Statement statement) {
            this.condition = condition;
            this.statement = statement;
            
            afterLabel = GenerateLabelName();
        }

        public override void Generate(IntermediateCodeBuilder builder) {
            Expression conditionRVal = condition.GenerateRValue(builder);

            builder.Append($"ifFalse {conditionRVal} goto {afterLabel}");
            statement.Generate(builder);
            builder.Append(afterLabel);
        }
    }
}