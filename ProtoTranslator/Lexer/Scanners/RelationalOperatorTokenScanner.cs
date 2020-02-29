using ProtoTranslator.Lexer.Tokens;

namespace ProtoTranslator.Lexer.Scanners {
    
    public class RelationalOperatorTokenScanner : ITokenScanner {
        
        public bool TryScan(Pointer pointer, out Token token) {
            
            if (pointer.Current == '<' || 
                pointer.Current == '>' || 
                pointer.Current == '!' || 
                pointer.Current == '=' && pointer.Next == '=') {
                
                string opString;

                if (pointer.Next == '=') {
                    opString = pointer.Select(2);
                    pointer.Move();
                }
                else {
                    opString = pointer.Current.ToString();
                }

                pointer.Move();

                token = new RelationalToken(opString);
                
                return true;
            }

            token = null;
            return false;
        }
    }
}