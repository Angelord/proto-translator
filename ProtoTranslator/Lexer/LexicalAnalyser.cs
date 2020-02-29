using System.IO;
using System.Collections.Generic;
using ProtoTranslator.Lexer.Scanners;

namespace ProtoTranslator.Lexer {
    public class LexicalAnalyser {
        
        private readonly List<ITokenScanner> scanners;
        private StreamReader fileStream;
        private Pointer pointer;
        
        public LexicalAnalyser() {
            
            scanners = new List<ITokenScanner>() {
                new NumericTokenScanner(),
                new WordTokenScanner(),
                new RelationalOperatorTokenScanner(),
                new DefaultTokenScanner()
            };
        }

        public List<Token> Scan(string filepath) {
            List<Token> result = new List<Token>();
            
            fileStream = new StreamReader(filepath);
            using (fileStream) {
                
                pointer = new Pointer(fileStream);

                Token token = ScanToken();

                while (token != null) {
                    result.Add(token);
                    token = ScanToken();
                }
            }

            return result;
        }

        private Token ScanToken() {
            
            SkipWhitespace();
            
            if (pointer.EndOfStream) { return null; }

            foreach (ITokenScanner tokenScanner in scanners) {

                if (tokenScanner.TryScan(pointer, out Token token)) {
                    return token;
                }
            }

            return null;
        }

        private void SkipWhitespace() {

            if (pointer.EndOfStream) { return; }

            do {

                string nextTwo = pointer.Select(2);
                if (nextTwo == "//") {
                    SkipSingleLineComment();
                }
                else if (nextTwo == "/*") {
                    SkipMultiLineComment();
                }
                else if (!char.IsWhiteSpace(pointer.Current)) {
                    return;
                }
            } while (pointer.Move());
        }

        private void SkipSingleLineComment() {
            
            while (pointer.Move()) {
                if(pointer.Current == '\n') return;
            }
        }

        private void SkipMultiLineComment() {

            while (pointer.Move()) {
                if (pointer.Select(2) == "*/") {
                    pointer.Move();
                    return;
                }
            }
        }
    }
}