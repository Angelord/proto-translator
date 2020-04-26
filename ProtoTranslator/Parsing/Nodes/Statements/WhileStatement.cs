using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes.Statements {
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
            if (logicalExpr.ReturnType != typeof(bool)) 
                logicalExpr.Error("Expression in while must evaluate to bool"); 
        }

        public override void Generate(CilEmitter emitter, ILabel begin, ILabel after) {

            ILabel whileLabel = emitter.GenerateLabel();

            logicalExpr.EmitRValue(emitter);
            
            after.EmitJumpIfFalse();
            whileLabel.Emit();
            
            statement.Generate(emitter, whileLabel, begin);
            
            begin.EmitJump();
            
            /*
             
            logicalExpression.jumping(0, after)
            
            create label
            
            emit label
            
            stmt.Gen(label, begin)
            
            emit goto begin
        
            */
        }
    }
}