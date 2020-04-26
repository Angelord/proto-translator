using ProtoTranslator.Debug;
using ProtoTranslator.Generation;
using ProtoTranslator.Lexer.Tokens;

namespace ProtoTranslator.Parsing.Nodes.Expressions {
    public class VariableUseExpression : Expression {
        
        // Offset ?

        private readonly LocalVariableSymbol symbol;
        
        public string Name => symbol.Name;

        public VariableUseExpression(LocalVariableSymbol symbol) : base(symbol.VariableType) {
            this.symbol = symbol;
        }

        public void EmitAssignment() {
            if (symbol.Variable == null) {
                Error("Use of undeclared variable");
            }

            symbol.Variable.EmitAssignment();
        }

        public override void EmitRValue(CilEmitter emitter) {
            if (symbol.Variable == null) {
                Error("Use of undeclared variable");
            }
            
            symbol.Variable.EmitValue();
        }

        public override void Log(Logger logger) {
            logger.LogLine("Variable use " + symbol.Name);
        }
    }
}