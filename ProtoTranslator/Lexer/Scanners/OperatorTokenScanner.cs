using System.Collections.Generic;
using ProtoTranslator.Lexer.Tokens;

namespace ProtoTranslator.Lexer.Scanners {
    
    public class OperatorTokenScanner : ITokenScanner {

        private static readonly Dictionary<string, WordToken> MultiCharOperators = new Dictionary<string, WordToken>() {
            { "<", WordToken.Lt },
            { ">", WordToken.Gt },
            { "<=", WordToken.Le },
            { "!=", WordToken.Ne },
            { ">=", WordToken.Ge },
            { "==", WordToken.Eq },
            { "||", WordToken.Or },
            { "&&", WordToken.And },
            { "++", WordToken.Increment },
            { "--", WordToken.Decrement }
        };
        
        public bool TryScan(Pointer pointer, out Token token) {
            
            string nextTwo = pointer.Select(2);
            if(MultiCharOperators.TryGetValue(nextTwo, out WordToken opWordToken)) {
                token = opWordToken;
                pointer.Move();
                pointer.Move();
                return true;
            }

            token = null;
            return false;
        }
    }
}