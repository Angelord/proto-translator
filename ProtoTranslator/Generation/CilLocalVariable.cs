using System.Reflection;
using System.Reflection.Emit;

namespace ProtoTranslator.Generation {
    public class CilLocalVariable : ILocalVariable {

        private readonly ILGenerator ilGenerator;
        private readonly LocalVariableInfo variableInfo;
        
        public CilLocalVariable(ILGenerator ilGenerator, LocalVariableInfo variableInfo) {
            this.ilGenerator = ilGenerator;
            this.variableInfo = variableInfo;
        }
        
        public void EmitValue() {
            ilGenerator.Emit(OpCodes.Ldloc, (LocalBuilder) variableInfo);
        }

        public void EmitAssignment() {
            ilGenerator.Emit(OpCodes.Stloc, (LocalBuilder) variableInfo);
        }
    }
}