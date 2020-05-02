
using System;
using System.Text;
using System.Collections.Generic;
using ProtoTranslator.Lexer.Tokens;

namespace ProtoTranslator.Lexer.Scanners {
    /// <summary>
    /// Recognizes Language keywords, types, built-in functions and Identifiers.
    /// </summary>
    public class WordTokenScanner : ITokenScanner {

        private readonly Dictionary<string, WordToken> words = new Dictionary<string, WordToken>();

        public WordTokenScanner() {
            // Keywords
            ReserveWord(new WordToken(Tags.IF, "if"));
            ReserveWord(new WordToken(Tags.ELSE, "else"));
            ReserveWord(new WordToken(Tags.DO, "do"));
            ReserveWord(new WordToken(Tags.WHILE, "while"));
            ReserveWord(new WordToken(Tags.BREAK, "break"));
            ReserveWord(WordToken.True);
            ReserveWord(WordToken.False);
            
            // Types
            ReserveWord(TypeToken.Int);
            ReserveWord(TypeToken.Float);
            ReserveWord(TypeToken.Char);
            ReserveWord(TypeToken.Bool);

            // Missing * and pchar
//            ReserveWord(new WordToken(Tags.ID, "abs"));
//            ReserveWord(new WordToken(Tags.ID, "sqr"));
//            ReserveWord(new WordToken(Tags.ID, "odd"));
//            ReserveWord(new WordToken(Tags.ID, "ord"));
//            ReserveWord(new WordToken(Tags.ID, "scanf"));
            ReserveWord(WordToken.PrintF);
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
            
            wordToken = new WordToken(Tags.ID, lexeme);
            ReserveWord(wordToken);

            return wordToken;
        }

        private void ReserveWord(WordToken wordToken) {
            words.Add(wordToken.Lexeme, wordToken);
        }
    }
}