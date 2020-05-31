using ProtoTranslator.Debug;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes.Statements {
    // Handles conditional with else parts
    public class ElseStatement : Statement {

        private readonly Expression logicalExpression;

        private readonly Statement ifStatement;

        private readonly Statement elseStatement;

        public ElseStatement(Expression logicalExpression, Statement ifStatement, Statement elseStatement) {
            this.logicalExpression = logicalExpression;
            this.ifStatement = ifStatement;
            this.elseStatement = elseStatement;
            if (logicalExpression.ReturnType != typeof(bool)) 
                logicalExpression.Error("Expression in if must evaluate to bool");
        }

        public override void Generate(CilEmitter emitter, ILabel begin, ILabel after) {

            ILabel ifLabel = emitter.GenerateLabel();
            ILabel elseLabel = emitter.GenerateLabel();

            logicalExpression.EmitRValue(emitter);
            
            elseLabel.EmitJumpIfFalse();
            
            ifLabel.Emit();
            ifStatement.Generate(emitter, ifLabel, after);
            after.EmitJump();
            
            elseLabel.Emit();
            elseStatement.Generate(emitter, elseLabel, after);
        }

        public override void Log(ILogger logger) {
            logger.LogLine("If");
            logger.IncreaseIndent();
                logicalExpression.Log(logger);
                ifStatement.Log(logger);
            logger.DecreaseIndent();
            
            logger.LogLine("Else");
            logger.IncreaseIndent();
                elseStatement.Log(logger);
            logger.DecreaseIndent();
        }
    }
}