using ProtoTranslator.Lexer.Tokens;

namespace ProtoTranslator.Lexer.Scanners {
    public class DefaultTokenScanner : ITokenScanner {
        
        public bool TryScan(Pointer pointer, out Token token) {
            
            token = new MiscToken(pointer.Current);
            
            pointer.Move();

            return true;
        }
    }
}