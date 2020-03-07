namespace ProtoTranslator.Parsing {
    public class Id : Expression {
        
        public override Expression GenerateLValue(IntermediateCodeBuilder builder) {
            return this;
        }

        public override Expression GenerateRValue(IntermediateCodeBuilder builder) {
            return this;
        }
    }
}