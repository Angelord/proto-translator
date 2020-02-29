using System.IO;
using System.Text;
using System.Collections.Generic;
using ProtoTranslator.Lexer.Tokens;

namespace ProtoTranslator.Lexer {
    public class LexicalAnalyser {
        
        private StreamReader fileStream;
        private Dictionary<string, WordToken> words = new Dictionary<string, WordToken>();
        private Pointer pointer;

        private bool EndOfFile => fileStream.Peek() < 0;

        public LexicalAnalyser() {
            ReserveWord(new WordToken(Tag.True, "true"));
            ReserveWord(new WordToken(Tag.False, "false"));
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
            
            if (EndOfFile) { return null; }

            string nextTwo = pointer.Select(2);

            if (char.IsDigit(pointer.Current)) {
                return ScanNumber();
            }
            if (char.IsLetter(pointer.Current)) {
                return ScanWord();
            }
            else if (pointer.Current == '<' || pointer.Current == '>' || pointer.Current == '!' || nextTwo == "==") {
                return ScanRelationalOperator();
            }

            Token token = new MiscToken(pointer.Current);
            pointer.Move();
            
            return token;
        }

        private void SkipWhitespace() {

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
        
        private Token ScanNumber() {
            int value = 0;
            do {
                value = 10 * value + (int)char.GetNumericValue(pointer.Current);
            } while (pointer.Move() && char.IsDigit(pointer.Current));
            
            return new NumberToken(value);
        }

        private Token ScanWord() {

            StringBuilder wordBuilder = new StringBuilder();
            do {
                wordBuilder.Append(pointer.Current);
            } while (pointer.Move() && char.IsLetterOrDigit(pointer.Current));

            string lexeme = wordBuilder.ToString();

            WordToken wordToken;
            if (words.TryGetValue(lexeme, out wordToken)) {
                return wordToken;
            }
            
            wordToken = new WordToken(Tag.Id, lexeme);
            ReserveWord(wordToken);

            return wordToken;
        }

        private Token ScanRelationalOperator() {

            string opString;

            if (pointer.Next == '=') {
                opString = pointer.Select(2);
                pointer.Move();
            }
            else {
                opString = pointer.Current.ToString();
            }

            pointer.Move();

            return new RelationalToken(opString);
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

        private void ReserveWord(WordToken wordToken) {
            words.Add(wordToken.Lexeme, wordToken);
        }
    }
}