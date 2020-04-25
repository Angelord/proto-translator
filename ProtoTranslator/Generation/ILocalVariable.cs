namespace ProtoTranslator.Generation {
    public interface ILocalVariable {
        
        void EmitValue();
        
        void EmitAssignment();
    }
}