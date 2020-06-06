using System.Reflection.Emit;
using ProtoTranslator.Parsing;

namespace ProtoTranslator.Generation {
    public class CilLabel : ILabel {
        
        private readonly ILGenerator generator;
        private readonly Label label;
        
        public CilLabel(ILGenerator generator) {
            this.generator = generator;
            label = generator.DefineLabel();
        }

        public void Emit() {
            generator.MarkLabel(label);
        }

        public void EmitJump() {
            generator.Emit(OpCodes.Br, label);
        }

        public void EmitJumpIfFalse() {
            generator.Emit(OpCodes.Brfalse, label);
        }

        public void EmitJumpIfTrue() {
            generator.Emit(OpCodes.Brtrue, label);
        }
    }
}