namespace ProtoTranslator.Parsing {
    public class Seq : Statement {

        private readonly Statement first;
        private readonly Statement second;

        public Seq(Statement first, Statement second) {
            this.first = first;
            this.second = second;
        }

        public override void Generate(IntermediateCodeBuilder builder) {
            
            first.Generate(builder); 
            
            second.Generate(builder);
        }
    }
}