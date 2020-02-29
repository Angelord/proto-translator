namespace ProtoTranslator.Lexer.Scanners {
    
    public interface ITokenScanner {

        bool TryScan(Pointer pointer, out Token token);
    }
}