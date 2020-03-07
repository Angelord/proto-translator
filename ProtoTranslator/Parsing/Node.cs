namespace ProtoTranslator.Parsing {
    public abstract class Node {
    }

    /*

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