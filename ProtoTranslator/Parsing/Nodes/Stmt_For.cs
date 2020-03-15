using ProtoTranslator.Generation;
using ProtoTranslator.Parsing.Nodes;

namespace ProtoTranslator.Parsing {
    public class Stmt_For : Statement {

        private readonly Statement definition;
        private readonly Expression condition;
        private readonly Statement increment;
        private readonly Statement statement;
        private readonly string beginLabel;
        private readonly string afterLabel;
        
        public Stmt_For(Statement definition, Expression condition, Statement increment, Statement statement) {
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