using ProtoTranslator.Debug;
using ProtoTranslator.Generation;
using ProtoTranslator.Lexer;

namespace ProtoTranslator.Parsing.Nodes.Expressions {
    // Implements binary operators like + and *
    public class ArithmeticExpression : Expression {

        private readonly Token operatorToken;
        private readonly Expression lhs;
        private readonly Expression rhs;

        public ArithmeticExpression(Token operatorToken, Expression lhs, Expression rhs) : base(null) {
            this.operatorToken = operatorToken;
            this.lhs = lhs;
            this.rhs = rhs;

            ReturnType = TypeUtils.Max(lhs.ReturnType, rhs.ReturnType);
            
            if(ReturnType == null) Error("Type error");
        }
        
        public override void EmitRValue(CilEmitter emitter) {
            lhs.EmitRValue(emitter);
            rhs.EmitRValue(emitter);
            emitter.EmitBinaryOperator(((char)operatorToken.Tag).ToString());
        }

        public override void Log(Logger logger) {
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