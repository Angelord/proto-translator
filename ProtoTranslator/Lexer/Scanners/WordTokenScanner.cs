
using System.Text;
using System.Collections.Generic;
using ProtoTranslator.Lexer.Tokens;

namespace ProtoTranslator.Lexer.Scanners {
    /// <summary>
    /// Recognizes Language keywords and Identifiers.
    /// </summary>
    public class WordTokenScanner : ITokenScanner {

        private readonly Dictionary<string, WordToken> words = new Dictionary<string, WordToken>();

        private static readonly string[] Keywords = {
            "if",
            "else",
            "while",
            "break",
            "continue",
            "return"
        };
        
        public WordTokenScanner() {
            foreach (string keyword in Keywords) {
                ReserveWord(new WordToken(Tag.Keyword, keyword));
            }
        }

        public bool TryScan(Pointer pointer, out Token token) {
            token = null;
            
            if (!char.IsLetter(pointer.Current)) { return false; }
            
            string lexeme = ScanLexeme(pointer);

            token = CreateFromLexeme(lexeme);

            return true;
        }

        private string ScanLexeme(Pointer pointer) {
            StringBuilder wordBuilder = new StringBuilder();
            do {
                wordBuilder.Append(pointer.Current);
            } while (pointer.Move() && char.IsLetterOrDigit(pointer.Current));

            return wordBuilder.ToString();
        }

        private WordToken CreateFromLexeme(string lexeme) {
            WordToken wordToken;
            if (words.TryGetValue(lexeme, out wordToken)) {
                return wordToken;
            }
            
            wordToken = new WordToken(Tag.Id, lexeme);
            ReserveWord(wordToken);

            return wordToken;
        }

        private void ReserveWord(WordToken wordToken) {
            words.Add(wordToken.Lexeme, wordToken);
        }
    }
}