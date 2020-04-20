using System;
using ProtoTranslator.Generation;
using ProtoTranslator.Parsing.Nodes;

namespace ProtoTranslator.Parsing {
    public class SyntaxTree {

        private Statement root;
        
        public SyntaxTree(Statement root) {
            this.root = root;
        }

        // Traverses the tree and generates output code 
        public void Generate(CilEmitter emitter) {
            throw new NotImplementedException();
        }
    }
}