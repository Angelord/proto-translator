using ProtoTranslator.Debug;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes.Statements {
    public class InfiniteLoopStatement : Statement {

        private Statement contents;

        public void Init(Statement contents) {
            this.contents = contents;
        }

        protected override void DoGenerate(CilEmitter emitter) {
            ILabel contentsLabel = emitter.GenerateLabel();
           
            emitter.EmitBool(true);
            after.EmitJumpIfFalse();
            contentsLabel.Emit();
            
            contents.Generate(emitter, contentsLabel, begin);
            
            begin.EmitJump();
        }

        public override void Log(ILogger logger) {
            logger.LogLine("Infinite");
            logger.IncreaseIndent();
            contents.Log(logger);
            logger.DecreaseIndent();
            logger.LogLine("Loop");
        }
    }
}