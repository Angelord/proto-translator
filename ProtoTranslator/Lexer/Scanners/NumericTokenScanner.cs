using System.Text;
using ProtoTranslator.Lexer.Tokens;

namespace ProtoTranslator.Lexer.Scanners {
    /// <summary>
    /// Recognizes integer and floating point numbers.
    /// </summary>
    public class NumericTokenScanner : ITokenScanner {

        private const char DECIMAL_SEPARATOR = '.';
        private const char PLUS = '+';
        private const char MINUS = '-';
        
        public bool TryScan(Pointer pointer, out Token token) {
            token = null;
            
            if (pointer.Current != PLUS && pointer.Current != MINUS && !IsNumeric(pointer.Current)) { return false; }

            if ((pointer.Current == PLUS || pointer.Current == MINUS) && !IsNumeric(pointer.Next)) { return false; }

            string numericString = ScanNumericString(pointer, out bool isFloat);

            if (isFloat) {
                token = new RealToken(float.Parse(numericString));
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