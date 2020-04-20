using ProtoTranslator.Lexer.Tokens;

namespace ProtoTranslator.Parsing.Nodes {

    public class Statement : Node {

        public static readonly Statement Null = new Statement();

        public static Statement Enclosing = Statement.Null; // Used for break stmts

        public virtual void Gen(Label begin, Label after) {
        } // Called with labels begin and after
    }

    public class IfStatement : Statement {

        private Expression logicalExpr;

        private Statement statement;

        public IfStatement(Expression logicalExpr, Statement statement) {
            this.logicalExpr = logicalExpr;
            this.statement = statement;
            if (logicalExpr.Type != TypeToken.Bool) logicalExpr.Error("Expression in if must evaluate to bool");
        }

        public override void Gen(Label begin, Label after) {
            /*
             generate label
             
             call logicalExpr.Jumping(0, a) to have it generate the jumping code.
             
             emitLabel(label);
             
             stmt.Gen(label, after);
             
             */
        }
    }

    // Handles conditional with else parts
    public class ElseStatement : Statement {

        private Expression logicalExpression;

        private Statement ifStatement;

        private Statement elseStatement;

        public ElseStatement(Expression logicalExpression, Statement ifStatement, Statement elseStatement) {
            this.logicalExpression = logicalExpression;
            this.ifStatement = ifStatement;
            this.elseStatement = elseStatement;
            if (logicalExpression.Type != TypeToken.Bool) 
                logicalExpression.Error("Expression in if must evaluate to bool");
        }

        public override void Gen(Label begin, Label after) {

            /*
                create label1
                create label2
                
                call Jumping on expression(0, label2)
                
                emit label 1 
                ifStmt.gen(label1, after); 
                emit goto label after
                
                emit label 2
                elseStmt.gen(label2, after);
            */
        }
    }

    
    public class WhileStatement : Statement {

        private Expression logicalExpr;

        private Statement statement;

        public WhileStatement() {
            logicalExpr = null;
            statement = null;
        }

        public void Init(Expression logicalExpr, Statement statement) {
            this.logicalExpr = logicalExpr;
            this.statement = statement;
            if (logicalExpr.Type != TypeToken.Bool) 
                logicalExpr.Error("Expression in while must evaluate to bool"); 
        }

        public override void Gen(Label begin, Label after) {
            /*
             
            logicalExpression.jumping(0, after)
            
            create label
            
            emit label
            
            stmt.Gen(label, begin)
            
            emit goto begin
        
            */
        }
    }

    public class DoStatement : Statement {

        private Expression logicalExpr;

        private Statement statement;

        public DoStatement() {
            logicalExpr = null;
            statement = null;
        }

        public void Init(Expression logicalExpr, Statement statement) {
            this.logicalExpr = logicalExpr;
            this.statement = statement;
            if (logicalExpr.Type != TypeToken.Bool) 
                logicalExpr.Error("Expression in while must evaluate to bool"); 
        }

        public override void Gen(Label begin, Label after) {
            /*
             
            create label
            
            stmt.Gen(begin, label)
            
            emit label
        
            expr.jumping(begin, 0)

            */
        }
    }

    public class BreakStatement : Statement {
        private Statement loopStatement;

        public BreakStatement() {

            if (Statement.Enclosing == Statement.Null) {
                Error("Unenclosed break");
            }
            loopStatement = Statement.Enclosing;
        }

        public override void Gen(Label begin, Label after) {
            // Emit goto after
        }
    }

    // Implement assignment to identifiers
    public class SetStatement : Statement {

        private IdExpr id;
        private Expression valueExpr;

        public SetStatement(IdExpr id, Expression valueExpr) {
            this.id = id;
            this.valueExpr = valueExpr;
         
            CheckTypes();
        }

        private void CheckTypes() {
            if(TypeToken.Numeric(id.Type) && TypeToken.Numeric(valueExpr.Type)) return;
            if(id.Type == TypeToken.Bool && valueExpr.Type == TypeToken.Bool) return;
            Error("Type Error");
        }

        public override void Gen(Label begin, Label after) {
            // emit id = valueExpr.Gen()
        }
    }

    // Implements as sequence of statements
    public class SeqStatement : Statement {
        private Statement first;
        private Statement second;

        public SeqStatement(Statement first, Statement second) {
            this.first = first;
            this.second = second;
        }

        public override void Gen(Label begin, Label after) {
            /*
            if first is Stmt.null -> second.Gen(begin, after)
            if second is Stmt.null -> first.Gen(begin, after)
            else 
                create new label
                first.Gen(begin, label)
                emit label
                second.Gen(label, after)
             */
        }
    }
}