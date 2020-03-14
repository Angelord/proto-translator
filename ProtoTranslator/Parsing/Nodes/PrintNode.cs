﻿using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public class PrintNode : Statement {

        private readonly Expression valExpr;
        
        public PrintNode(Expression valExpr) {
            this.valExpr = valExpr;
        }

        public override void Generate(CilEmitter emitter) {
            
            Expression exprRVal = valExpr.GetRValue(emitter);

            exprRVal.Push(emitter);
            
            emitter.EmitWrite(exprRVal.DetermineType());
        }
    }
}