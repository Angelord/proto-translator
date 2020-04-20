using System;
using System.Globalization;
using ProtoTranslator.Generation;
using ProtoTranslator.Lexer;
using ProtoTranslator.Lexer.Tokens;

namespace ProtoTranslator.Parsing.Nodes {
    public abstract class Expression : Node {

        protected Token Operator;

        public TypeToken Type;

        protected Expression(Token op, TypeToken type) {
            Operator = op;
            Type = type;
        }

        // Returns a "term" that can fit the right side of a three address instruction. 
        public virtual Expression Gen() { return this; }

        // Computes/Reduces an expression down to a single address. That is, it returns a constant, and identifier or a temporary name.
        public virtual Expression Reduce() { return this; }

        public override string ToString() { return Operator.ToString(); }
    }

    public class IdExpr : Expression {
        
        // Offset ?
        
        public IdExpr(Token op, TypeToken type, int offset) : base(op, type) { }
    }
    
    public class TemporaryExpr : Expression {

        private static int count = 0;

        public readonly int Number;

        public TemporaryExpr(TypeToken type) : base(WordToken.Temp, type) {
            Number = ++count;
        }

        public override string ToString() { return "t" + Number; }
    }

    public class ConstantExpr : Expression {
        
        public static readonly ConstantExpr True = new ConstantExpr(WordToken.True, TypeToken.Bool);
        public static readonly ConstantExpr False = new ConstantExpr(WordToken.False, TypeToken.Bool);

        public ConstantExpr(Token tok, TypeToken type) : base(tok, type) { }

        public ConstantExpr(int i) : base(new IntegerToken(i), TypeToken.Int) { }

        public void EmitJumping(int trueLabel, int falseLabel) {
//            if this is true -> emit goto trueLabel
//            if this is False -> emit Goto falseLabel
        }
    }

    // Base class for arithmetic and unary operators.
    public class OpExpr : Expression {
        
        public OpExpr(Token op, TypeToken type) : base(op, type) { }

        public override Expression Reduce() {
            /*
            Expression x = Gen();
            Temp t = new Temp(type);
            emit (t.toString() = x.toString())
            return t;
            */

            throw new NotImplementedException();
        }
    }

    // Implements binary operators like + and *
    public class ArithmeticExpr : OpExpr {

        private Expression lhs;
        private Expression rhs;

        public ArithmeticExpr(Token op, Expression lhs, Expression rhs) : base(op, null) {
            this.lhs = lhs;
            this.rhs = rhs;

            Type = TypeToken.Max(lhs.Type, rhs.Type);
            
            if(Type == null) Error("Type error");
        }

        public override Expression Gen() {
            return new ArithmeticExpr(Operator, lhs.Reduce(), rhs.Reduce());
        }

        public override string ToString() {
            return $"{lhs} {Operator} {rhs}";
        }
    }

    public class UnaryExpr : OpExpr {

        public Expression Expression;
        
        public UnaryExpr(Token op, Expression expression) : base(op, null) {
            Expression = expression;
            Type = TypeToken.Max(TypeToken.Int, Expression.Type);
            if(Type == null) Error("Type error"); 
        }

        public override Expression Gen() {
            return new UnaryExpr(Operator, Expression.Reduce());
        }

        public override string ToString() {
            return $"{Operator} {Expression}";
        }
    }

    // Provides commong functionality for Or, And and Not
    public class LogicalExpr : Expression {
        
        public LogicalExpr(Token op) : base(op, TypeToken.Bool) { }
        
        public override Expression Gen() {
            /*
            create new TemporaryExpr
             
            call jumping logic abstract method
            
            emit true or false based on that
            */
            
            throw new NotImplementedException();
        }
    }

    public class OrExpression : LogicalExpr {
        
        private Expression lhs;
        private Expression rhs;

        public OrExpression(Token op, Expression lhs, Expression rhs) : base(op) {
            this.lhs = lhs;
            this.rhs = rhs;

            if(lhs.Type != TypeToken.Bool || rhs.Type != TypeToken.Bool) Error("Type Error");
        }
        
        // jumping code
    }

    public class AndExpression : LogicalExpr {
        
        private Expression lhs;
        private Expression rhs;
        
        public AndExpression(Token op, Expression lhs, Expression rhs) : base(op) {
            this.lhs = lhs;
            this.rhs = rhs;
            
            if(lhs.Type != TypeToken.Bool || rhs.Type != TypeToken.Bool) Error("Type Error");
        }
        
        // jumping code
    }

    // Implement the negation ! operator.
    public class NotExpression : LogicalExpr {
        
        private Expression expression;

        public NotExpression(Token op, Expression expression) : base(op) {
            this.expression = expression;
            
            if(expression.Type != TypeToken.Bool) Error("Type Error");
        }
        
        // jumping code
    }

    // Implements the operators <, <=, ==, !=, >= and >
    public class RelationalExpr : LogicalExpr {

        private Expression lhs;
        private Expression rhs;
        
        public RelationalExpr(Token op, Expression lhs, Expression rhs) : base(op) {
            this.lhs = lhs;
            this.rhs = rhs;
            
            if (lhs.Type != rhs.Type) Error("Type Error"); 
        }
        
        // jumping code
    }
}