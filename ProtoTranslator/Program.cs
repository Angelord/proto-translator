using ProtoTranslator.Debug;
using ProtoTranslator.Lexer;
using ProtoTranslator.Parsing;

namespace ProtoTranslator {
    internal class Program {
        public static void Main(string[] args) {
            
            Logger lexicalLogger = new ConsoleLogger();
            
            LexicalAnalyser lexer = new LexicalAnalyser("Resources/Program.c", lexicalLogger);
            
            using (lexer) {
                
                Parser parser = new Parser();

                parser.Parse(lexer);
            }
            
            lexicalLogger.Flush();
        }
    }
}