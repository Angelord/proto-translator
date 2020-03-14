//using System;
//using ProtoTranslator.Generation;
//using ProtoTranslator.Parsing.Nodes;
//
//namespace ProtoTranslator.Parsing {
//    /// <summary>
//    /// Represents array access expressions
//    /// </summary>
//    public class Access : Expression {
//
//        private readonly Id arrayNode;
//        private readonly Expression indexNode;
//        
//        public Access(Id arrayNode, Expression indexNode) {
//            this.arrayNode = arrayNode;
//            this.indexNode = indexNode;
//        }
//
//        public override Expression GetLValue(CilEmitter builder) {
//            return new Access(arrayNode, indexNode.GetRValue(builder));
//        }
//
//        public override Expression GetRValue(CilEmitter builder) {
//            // Create temporary T
//            // Get this.LValue
//            // emit string for t = the LValue
//            
//            throw new NotImplementedException();
//        }
//
//        public override void Push(CilEmitter emitter) {
//            throw new NotImplementedException();
//        }
//    }
//}