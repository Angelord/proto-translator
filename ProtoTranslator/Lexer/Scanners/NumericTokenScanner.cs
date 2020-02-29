using ProtoTranslator.Lexer.Tokens;

namespace ProtoTranslator.Lexer.Scanners {
    public class NumericTokenScanner : ITokenScanner {
        
        public bool TryScan(Pointer pointer, out Token token) {
            token = null;
            
            if (!char.IsDigit(pointer.Current)) { return false; }

            int value = 0;
            do {
                value = 10 * value + (int)char.GetNumericValue(pointer.Current);
            } while (pointer.Move() && char.IsDigit(pointer.Current));
            
            token = new NumberToken(value);
            
            return true;
        }
    }
}