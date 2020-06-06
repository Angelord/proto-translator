namespace ProtoTranslator.Generation {
    public interface ILabel {

        /// <summary> Emits this label. </summary>
        void Emit();

        /// <summary> Emits a jump to this particular label. </summary>
        void EmitJump();

        /// <summary> Emits a jump to this label if a condition is false. </summary>
        void EmitJumpIfFalse();

        /// <summary> Emits a jump to this label if a condition is true. </summary>
        void EmitJumpIfTrue();
    }
}