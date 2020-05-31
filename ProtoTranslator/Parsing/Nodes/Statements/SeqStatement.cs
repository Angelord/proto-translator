using ProtoTranslator.Debug;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes.Statements {
    // Implements a sequence of statements
    public class SeqStatement : Statement {
        private readonly Statement first;
        private readonly Statement second;

        public SeqStatement(Statement first, Statement second) {
            this.first = first;
            this.second = second;
        }

        public override void Generate(CilEmitter emitter, ILabel begin, ILabel after) {
            if (first == Statement.Null) {
                second.Generate(emitter, begin, after);
            }
            else if (second == Statement.Null) {
                first.Generate(emitter, begin, after);
            }
            else {
                // Create new label between the two statements
                ILabel midpoint = emitter.GenerateLabel();
                
                first.Generate(emitter, begin, midpoint);
                
                midpoint.Emit();
                
                second.Generate(emitter, midpoint, after);
            }
        }

        public override void Log(ILogger logger) {
            first.Log(logger);
            second.Log(logger);
        }
    }
}