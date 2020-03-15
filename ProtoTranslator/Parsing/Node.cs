using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing {
    public abstract class Node {
    }

    /*
//using ProtoTranslator.Generation;
//
//namespace ProtoTranslator.Parsing.Nodes {
//    public class Id : Expression {
//        
//        public override Expression GetLValue(CilEmitter builder) {
//            return this;
//        }
//
//        public override Expression GetRValue(CilEmitter builder) {
//            return this;
//        }
//
//        public override void Push(CilEmitter emitter) {
//            throw new System.NotImplementedException();
//        }
//    }
//}

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

    public class Else : Statement { }

    public class While : Statement { }

    public class Assign : Expression {
    
        y z
    
        // Think of chaining assignments a = b = c = 5
        public override Expression GetRValue() {
            t = z.GetRValue();
            emit string for y.GetLvalue() = t
            return t
        } 
    }

    // new Relational("<", lhs.n, rhs.n);
    public class Relational : Expression { 
    
        public override Expression GetRValue() {
            t = new temporary;
            emit string for t = rvalue(y) op rvalue(z)
            return a new node for t
        } 
    }
    
    // new Operator('+', lhs.n, rhs.n);
    public class Operator : Expression {
    
        public override Expression GetRValue() {
            t = new temporary;
            emit string for t = rvalue(y) op rvalue(z)
            return a new node for t
        } 
    }
    
    // new Number(num.value);
    public class Number : Statement { }

*/
}