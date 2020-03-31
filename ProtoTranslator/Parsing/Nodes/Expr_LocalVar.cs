using System;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public class Expr_LocalVar : Expression {

        private readonly string identifier;

        public Expr_LocalVar(string identifier) {
            this.identifier = identifier;
        }

        public override Type DetermineType() { return GetSymbol().Type; }

        public override LValue GetLValue(CilEmitter emitter) {
            return new LValue_LocalVar(GetSymbol().VariableInfo);
        }

        public override Expression EmitRValue(CilEmitter emitter) {
            
            emitter.EmitLocalVar(GetSymbol().VariableInfo);

            return this;
        }

        private LocalVariableSymbol GetSymbol() {
            return SymTable.Get(identifier) as LocalVariableSymbol;
        }
    }
}