using ProtoTranslator.Debug;
using ProtoTranslator.Generation;
using ProtoTranslator.Lexer;

namespace ProtoTranslator.Parsing.Nodes.Expressions {
    // Implements the operators <, <=, ==, !=, >= and >
    public class ComparisonExpression : BinaryLogicalExpression {

        private readonly string comparisonOperator;

        public ComparisonExpression(Token token, Expression lhs, Expression rhs) : base(lhs, rhs) {
            comparisonOperator = token.GetLexeme();
        }

        protected override void EmitOperator(CilEmitter emitter) {
            emitter.EmitComparison(comparisonOperator);
        }

        public override void Log(ILogger logger) {
            logger.LogLine("Relational " + comparisonOperator);
            logger.IncreaseIndent();
            lhs.Log(logger);
            rhs.Log(logger);
            logger.DecreaseIndent();
        }
    }
}