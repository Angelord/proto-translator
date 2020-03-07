namespace ProtoTranslator.Parsing {
    public abstract class Expression : Node {

        public abstract Expression GenerateLValue(IntermediateCodeBuilder builder);

        public abstract Expression GenerateRValue(IntermediateCodeBuilder builder);
    }
}