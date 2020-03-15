using System;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public class Expr_LocalVar : Expression {

        private readonly string identifier;

        public Expr_LocalVar(string identifier) {
            this.identifier = identifier;
        }

        public override Type DetermineType() { return SymTable.Get(identifier).Type; }

        public override LValue GetLValue(CilEmitter emitter) {
            return new LValue_LocalVar(SymTable.Get(identifier).LocalVariableInfo);
        }

        public override Expression EmitRValue(CilEmitter emitter) {

            Symbol varSymbol = SymTable.Get(identifier);
            
            emitter.EmitLocalVar(varSymbol.LocalVariableInfo);

            return this;
        }
    }
}