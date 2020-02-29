using System.Text;
using ProtoTranslator.Lexer.Tokens;

namespace ProtoTranslator.Lexer.Scanners {
    public class NumericTokenScanner : ITokenScanner {

        private const char DECIMAL_SEPARATOR = '.';
        
        public bool TryScan(Pointer pointer, out Token token) {
            token = null;
            
            if (!IsNumeric(pointer.Current)) { return false; }

            string numericString = ScanNumericString(pointer, out bool isFloat);

            if (isFloat) {
                token = new FloatToken(float.Parse(numericString));
            }
            else {
                token = new IntegerToken(int.Parse(numericString));
            }

            return true;
        }

        private bool IsNumeric(char symbol) {
            return char.IsDigit(symbol) || symbol == DECIMAL_SEPARATOR;
        }

        private string ScanNumericString(Pointer pointer, out bool isFloat) {
            
            isFloat = false;
            
            StringBuilder numberBuilder = new StringBuilder();
            do {
                isFloat = isFloat || pointer.Current == DECIMAL_SEPARATOR;
                numberBuilder.Append(pointer.Current);
            } while (pointer.Move() && IsNumeric(pointer.Current));

            return numberBuilder.ToString();
        }
    }
}