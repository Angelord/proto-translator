using ProtoTranslator.Generation;
using ProtoTranslator.Parsing.Nodes;

namespace ProtoTranslator.Parsing {
    public class For : Statement {

        private readonly Expression definition;
        private readonly Expression condition;
        private readonly Expression increment;
        private readonly Statement statement;
        private readonly string beginLabel;
        private readonly string afterLabel;
        
        public For(Expression definition, Expression condition, Expression increment, Statement statement) {
            this.definition = definition;
            this.condition = condition;
            this.increment = increment;
            this.statement = statement;

//            beginLabel = GenerateLabelName();
//            afterLabel = GenerateLabelName();
        }

        public override void Generate(CilEmitter builder) {
//            Expression conditionRVal = condition.GenerateRValue(builder);
//            
//            definition.GenerateRValue(builder);
//            builder.Append(beginLabel);
//            builder.Append($"ifFalse {conditionRVal} goto {afterLabel}");
//            increment.GenerateRValue(builder);
//            
//            statement.Generate(builder);
//            
//            builder.Append($"goto {beginLabel}");
//            builder.Append(afterLabel);
        }
    }
}