using ProtoTranslator.Debug;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes.Statements {
    public class VariableDeclarationStatement : Statement {
        
        private readonly LocalVariableSymbol symbol;
        private readonly Expression initialValue;

        public VariableDeclarationStatement(LocalVariableSymbol symbol, Expression initialValue = null) {
            this.symbol = symbol;
            this.initialValue = initialValue;
            
            // TODO : Type checking
        }
        
        protected override void DoGenerate(CilEmitter emitter) {
            symbol.Variable = emitter.EmitLocalVarDeclaration(symbol.Name, symbol.VariableType);

            if (initialValue != null) {
                initialValue.EmitRValue(emitter);
                if (initialValue.ReturnType == typeof(string)) { emitter.EmitParse(symbol.VariableType); }
                symbol.Variable.EmitAssignment();
            }
        }

        public override void Log(ILogger logger) {
            logger.LogLine("Var Declaration " + symbol.VariableType + " " + symbol.Name);
            if(initialValue != null) {
                logger.IncreaseIndent();
                initialValue.Log(logger);
                logger.DecreaseIndent();
            }
        }
    }
}