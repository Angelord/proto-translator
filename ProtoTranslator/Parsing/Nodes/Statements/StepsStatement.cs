using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes.Statements {
    public class StepsStatement : Statement {

        private ILocalVariable variable;
        
        private Expression stepCountExpr;

        private Statement statement;

        public StepsStatement() {
            stepCountExpr = null;
            statement = null;
        }

        public void Init(Expression stepCountEpxr, Statement statement) {
            this.stepCountExpr = stepCountEpxr;
            this.statement = statement;
            if (!TypeUtils.Numeric(stepCountExpr.ReturnType)) 
                stepCountEpxr.Error("Expression in steps must evaluate to a numeric type " + stepCountEpxr.ReturnType); 
        }

        protected override void DoGenerate(CilEmitter emitter) {

            ILabel comparisonLabel = emitter.GenerateLabel();
            ILabel contentsLabel = emitter.GenerateLabel();

            variable = emitter.EmitLocalVarDeclaration("STEPS_LOOP_VAR" + stepCountExpr.lexline + "_" + stepCountExpr.lexline, typeof(int));
            stepCountExpr.EmitRValue(emitter);
            variable.EmitAssignment();
            
            comparisonLabel.Emit();
            
            variable.EmitValue();
            emitter.EmitInt32(0);
            emitter.EmitComparison(">");
            
            after.EmitJumpIfFalse();
            contentsLabel.Emit();
            
            statement.Generate(emitter, contentsLabel, comparisonLabel);

            variable.EmitValue();
            emitter.EmitInt32(1);
            emitter.EmitBinaryOperator("-");
            variable.EmitAssignment();

            comparisonLabel.EmitJump();
            
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