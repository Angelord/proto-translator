using System.Linq;
using ProtoTranslator.Lexer.Tokens;

namespace ProtoTranslator.Lexer.Scanners {
    
    public class OperatorTokenScanner : ITokenScanner {

        private static readonly char[] SingleCharOperators = new[] {
            '=',
            '+',
            '-',
            '*',
            '|',
            '/',
            '%',
            '&',
            '~',
            '<',
            '>',
            '!'
        };
        
        private static readonly string[] MultiCharOperators = new[] {
            "+=",
            "-=",
            "*=",
            "/=",
            "%=",
            "++",
            "--",
            "<=",
            "!=",
            ">=",
            "==",
            "||",
            "&&",
        };
        
        public bool TryScan(Pointer pointer, out Token token) {

            string nextTwo = pointer.Select(2);
            if(MultiCharOperators.Contains(nextTwo)) {
                token = new OperatorToken(nextTwo);
                pointer.Move();
                pointer.Move();
                return true;
            }
            
            if (SingleCharOperators.Contains(pointer.Current)) {
                token = new OperatorToken(pointer.Current.ToString());
                pointer.Move();
                return true;
            }

            token = null;
            return false;
        }
    }
}