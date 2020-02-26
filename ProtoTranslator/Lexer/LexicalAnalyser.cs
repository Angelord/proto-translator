using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Text;

namespace ProtoTranslator.Lexer {
    public class LexicalAnalyser {
        
        private StreamReader fileStream;
        private Dictionary<string, WordToken> words = new Dictionary<string, WordToken>();
        private char prevChar = ' ';
        private char curChar = ' ';
        private int lines;

        private bool EndOfFile => fileStream.Peek() < 0;

        public LexicalAnalyser() {
            ReserveWord(new WordToken(Tag.True, "true"));
            ReserveWord(new WordToken(Tag.False, "false"));
        }

        public List<Token> Scan(string filepath) {

            List<Token> result = new List<Token>();
            
            fileStream = new StreamReader(filepath);
            using (fileStream) {

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

            if (EndOfFile) { return null; }

            if (char.IsDigit(curChar)) {
                return ScanNumber();
            }
            if (char.IsLetter(curChar)) {
                return ScanWord();
            }

            Token token = new MiscToken(curChar);
            curChar = ' ';

            return token;
        }

        private void SkipWhitespace() {

            while (MovePointer()) {

                if (prevChar == '/' && curChar == '/') {
                    SkipSingleLineComment();
                }
                else if (prevChar == '/' && curChar == '*') {
                    SkipMultiLineComment();
                }
                else if (curChar != '\n' && curChar != ' ' && curChar != '\t' && curChar != '\r') {
                    return;
                }
            }
        }
        
        private Token ScanNumber() {
            int value = 0;
            do {
                value = 10 * value + (int)char.GetNumericValue(curChar);
            } while (MovePointer() && char.IsDigit(curChar));
            
            return new NumberToken(value);
        }

        private Token ScanWord() {

            StringBuilder wordBuilder = new StringBuilder();
            do {
                wordBuilder.Append(curChar);
            } while (MovePointer() && char.IsLetterOrDigit(curChar));

            string lexeme = wordBuilder.ToString();

            WordToken wordToken;
            if (words.TryGetValue(lexeme, out wordToken)) {
                return wordToken;
            }
            
            wordToken = new WordToken(Tag.Id, lexeme);
            ReserveWord(wordToken);

            return wordToken;
        }

        private void SkipSingleLineComment() {
            
            while (MovePointer()) {
                if(curChar == '\n') return;
            }
        }

        private void SkipMultiLineComment() {

            while (MovePointer()) {
                if(prevChar == '*' && curChar == '/') return;
            }
        }

        private bool MovePointer() {
            if (EndOfFile) {
                return false;
            }

            prevChar = curChar;
            curChar = (char)fileStream.Read();

            if (curChar == '\n') {
                lines++;
            }

            return true;
        }

        private void ReserveWord(WordToken wordToken) {
            words.Add(wordToken.Lexeme, wordToken);
        }
    }
}