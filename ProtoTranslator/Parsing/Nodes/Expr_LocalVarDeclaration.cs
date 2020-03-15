using System;
using System.Reflection;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public class Expr_LocalVarDeclaration : Expression {

        private readonly Type type;
        private readonly string identifier;
        
        public Expr_LocalVarDeclaration(Type type, string identifier) {
            this.type = type;
            this.identifier = identifier;
        }

        public override Type DetermineType() { return type; }

        public override LValue GetLValue(CilEmitter emitter) {
            LocalVariableInfo localVarInfo = emitter.EmitLocalVarDeclaration(identifier, type);
            SymTable.Put(identifier, new Symbol(type, localVarInfo));
            return new LValue_LocalVar(SymTable.Get(identifier).LocalVariableInfo);
        }

        public override Expression EmitRValue(CilEmitter emitter) {
            throw new InvalidOperationException("Declaration used where an RValue was expected!");
        }
    }
}