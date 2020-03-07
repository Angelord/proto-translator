using System;

namespace ProtoTranslator.Parsing {
    /// <summary>
    /// Represents array access expressions
    /// </summary>
    public class Access : Expression {

        private readonly Id arrayNode;
        private readonly Expression indexNode;
        
        public Access(Id arrayNode, Expression indexNode) {
            this.arrayNode = arrayNode;
            this.indexNode = indexNode;
        }

        public override Expression GenerateLValue(IntermediateCodeBuilder builder) {
            return new Access(arrayNode, indexNode.GenerateRValue(builder));
        }

        public override Expression GenerateRValue(IntermediateCodeBuilder builder) {
            // Create temporary T
            // Get this.LValue
            // emit string for t = the LValue
            
            throw new NotImplementedException();
        }
    }
}