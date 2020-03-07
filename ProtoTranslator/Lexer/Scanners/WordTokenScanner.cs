
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
            ReserveWord(new WordToken(Tag.Keyword, "if"));
            ReserveWord(new WordToken(Tag.Keyword, "else"));
            ReserveWord(new WordToken(Tag.Keyword, "while"));
            ReserveWord(new WordToken(Tag.Keyword, "break"));
            ReserveWord(new WordToken(Tag.Keyword, "continue"));
            ReserveWord(new WordToken(Tag.Keyword, "return"));
            ReserveWord(new WordToken(Tag.Type, "int"));
            ReserveWord(new WordToken(Tag.Type, "bool"));
            ReserveWord(new WordToken(Tag.Type, "char"));
            // Missing * and pchar
            ReserveWord(new WordToken(Tag.Id, "abs"));
            ReserveWord(new WordToken(Tag.Id, "sqr"));
            ReserveWord(new WordToken(Tag.Id, "odd"));
            ReserveWord(new WordToken(Tag.Id, "ord"));
            ReserveWord(new WordToken(Tag.Id, "scanf"));
            ReserveWord(new WordToken(Tag.Id, "printf"));
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