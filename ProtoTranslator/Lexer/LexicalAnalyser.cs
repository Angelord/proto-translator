using System;
using System.IO;
using System.Collections.Generic;
using ProtoTranslator.Debug;
using ProtoTranslator.Lexer.Scanners;

namespace ProtoTranslator.Lexer {
    public class LexicalAnalyser : IDisposable {
        
        private readonly List<ITokenScanner> scanners;
        private readonly StreamReader fileStream;
        private readonly Pointer pointer;
        private readonly Logger logger;
        
        public LexicalAnalyser(string filepath, Logger logger) {

            this.logger = logger;
            
            fileStream = new StreamReader(filepath);
            pointer = new Pointer(fileStream);

            scanners = new List<ITokenScanner>() {
                new NumericTokenScanner(),
                new StringTokenScanner(),
                new WordTokenScanner(),
                new RelationalOperatorTokenScanner(),
                new DefaultTokenScanner()
            };
        }

        public Token ScanToken() {
            
            SkipWhitespace();
            
            if (pointer.EndOfStream) { return null; }

            foreach (ITokenScanner tokenScanner in scanners) {

                if (tokenScanner.TryScan(pointer, out Token token)) {
                    logger.Log(token.ToString() + (pointer.Next == '\n' ? '\n' : ' '));
                    token.Line = pointer.Lines;
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

        public void Dispose() {
            fileStream?.Dispose();
        }
    }
}