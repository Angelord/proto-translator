namespace ProtoTranslator.Parsing {
    public class Constant : Expression {

        public override Expression GenerateLValue(IntermediateCodeBuilder builder) {
            return this;
        }

        public override Expression GenerateRValue(IntermediateCodeBuilder builder) {
            return this;
        }
    }
}