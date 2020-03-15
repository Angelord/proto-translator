using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public class Stmt_Seq : Statement {

        private readonly Statement first;
        private readonly Statement second;

        public Stmt_Seq(Statement first, Statement second) {
            this.first = first;
            this.second = second;
        }

        public override void Generate(CilEmitter builder) {
            
            first.Generate(builder); 
            
            second.Generate(builder);
        }
    }
}