using System.Reflection;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public class LValue_LocalVar : LValue {

        private readonly LocalVariableInfo localVariableInfo;

        public LValue_LocalVar(LocalVariableInfo localVariableInfo) {
            this.localVariableInfo = localVariableInfo;
        }
        
        public override void EmitAssignment(CilEmitter emitter) {
            emitter.EmitLocalVarAssignment(localVariableInfo);
        }
    }
}