using System;
using System.Text;

namespace ProtoTranslator.Parsing {
    public class IntermediateCodeBuilder {
        
        private readonly StringBuilder builder = new StringBuilder();

        public void Append(string value) {
            builder.Append(value);
        }

        public override string ToString() {
            return builder.ToString();
        }
    }
}