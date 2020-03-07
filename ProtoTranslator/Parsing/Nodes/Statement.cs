using System;
using System.Text;

namespace ProtoTranslator.Parsing {
    public abstract class Statement : Node {

        public abstract void Generate(IntermediateCodeBuilder builder);

        // TODO : Generate a globally unique label name
        public string GenerateLabelName() { throw new NotImplementedException(); }
    }
}