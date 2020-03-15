using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    /// <summary>
    /// Represents a call to printf
    /// </summary>
    public class Stmt_Print : Statement {

        private readonly Expression valExpr;
        
        public Stmt_Print(Expression valExpr) {
            this.valExpr = valExpr;
        }

        public override void Generate(CilEmitter emitter) {
            
            Expression exprRVal = valExpr.EmitRValue(emitter);
            
            emitter.EmitWrite(exprRVal.DetermineType());
        }
    }
}