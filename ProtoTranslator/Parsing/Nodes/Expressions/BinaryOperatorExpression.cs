﻿using ProtoTranslator.Debug;
using ProtoTranslator.Generation;
using ProtoTranslator.Lexer;

namespace ProtoTranslator.Parsing.Nodes.Expressions {
    // Implements binary operators such as &&, ||, +, -, /, *
    public class BinaryOperatorExpression : Expression {

        private readonly Token operatorToken;
        private readonly Expression lhs;
        private readonly Expression rhs;

        public BinaryOperatorExpression(Token operatorToken, Expression lhs, Expression rhs) : base(null) {
            this.operatorToken = operatorToken;
            this.lhs = lhs;
            this.rhs = rhs;

            ReturnType = TypeUtils.Max(lhs.ReturnType, rhs.ReturnType);
            
            if(ReturnType == null) Error("Type error");
        }
        
        public override void EmitRValue(CilEmitter emitter) {
            lhs.EmitRValue(emitter);
            rhs.EmitRValue(emitter);
            emitter.EmitBinaryOperator(operatorToken.GetLexeme());
        }

        public override void Log(ILogger logger) {
            logger.LogLine("Operator " + operatorToken.Tag);
            logger.IncreaseIndent();
            lhs.Log(logger);
            rhs.Log(logger);
            logger.DecreaseIndent();
        }

        public override string ToString() {
            return $"{lhs} {operatorToken.Tag} {rhs}";
        }
    }
}