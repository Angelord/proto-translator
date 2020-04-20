//using System.Text;
//using ProtoTranslator.Lexer.Tokens;
//
//namespace ProtoTranslator.Lexer.Scanners {
//    public class StringTokenScanner : ITokenScanner {
//
//        private const char QUOTE = '"';
//        
//        public bool TryScan(Pointer pointer, out Token token) {
//            token = null;
//
//            if (pointer.Current != QUOTE) { return false; }
//
//            StringBuilder stringBuilder = new StringBuilder();
//            while(pointer.Move() && pointer.Current != QUOTE) {
//                // TODO : Handle string not closed exc.
//                stringBuilder.Append(pointer.Current);
//            }
//
//            pointer.Move();
//            
//            token = new StringToken(stringBuilder.ToString());
//
//            return true;
//        }
//    }
//}