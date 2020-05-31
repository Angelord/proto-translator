using System;
using ProtoTranslator.Debug;
using ProtoTranslator.Generation;
using ProtoTranslator.Parsing.Nodes;

namespace ProtoTranslator.Parsing {
    public class SyntaxTree {

        private readonly Statement root;
        
        public SyntaxTree(Statement root) {
            this.root = root;
        }

        public void Log(ILogger logger) {
            root.Log(logger);
        }

        // Traverses the tree and generates output code 
        public void Generate(CilEmitter emitter) {

            emitter.BeginProgram();

            ILabel begin = emitter.GenerateLabel();
            ILabel end = emitter.GenerateLabel();
            
            begin.Emit();
            root.Generate(emitter, begin, end);
            end.Emit();
            
            emitter.WriteExecutable();
        }
    }
}