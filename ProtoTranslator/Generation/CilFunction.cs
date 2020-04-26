using System.Reflection;
using System.Reflection.Emit;

namespace ProtoTranslator.Generation {
    public class CilFunction : IFunction {

        private readonly ILGenerator generator;
        private readonly MethodInfo methodInfo;
        
        public CilFunction(ILGenerator generator, MethodInfo methodInfo) {
            this.generator = generator;
            this.methodInfo = methodInfo;
        }

        public void EmitCall() {
            generator.Emit(OpCodes.Call, methodInfo);
        }
    }
}