using ProtoTranslator.Debug;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes.Statements {
    public class IfStatement : Statement {

        private readonly Expression logicalExpr;

        private readonly Statement contents;

        public IfStatement(Expression logicalExpr, Statement contents) {
            this.logicalExpr = logicalExpr;
            this.contents = contents;
            if (logicalExpr.ReturnType != typeof(bool)) logicalExpr.Error("Expression in if must evaluate to bool");
        }

        public override void Generate(CilEmitter emitter, ILabel begin, ILabel after) {

            ILabel contentsLabel = emitter.GenerateLabel();

            logicalExpr.EmitRValue(emitter);    // Generate rvalue from logical expression.
            
            after.EmitJumpIfFalse();    // Skip the if body if false
            
            contentsLabel.Emit();
            
            contents.Generate(emitter, contentsLabel, after);
            
            /*
             generate new label
             
             call logicalExpr.Jumping(0, after) to have it generate the jumping code.
             
             emitLabel(new label);
             
             stmt.Gen(label, after);
             
             */
        }

        public override void Log(ILogger logger) {
            logger.LogLine("If");
            logger.IncreaseIndent();
            logicalExpr.Log(logger);
            contents.Log(logger);
            logger.DecreaseIndent();
        }
    }
}