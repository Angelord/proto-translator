using ProtoTranslator.Debug;
using ProtoTranslator.Generation;
using ProtoTranslator.Lexer;
using ProtoTranslator.Parsing;

namespace ProtoTranslator {
    /// <summary>
    /// A facade class that provides an easy interface for compiling a file.
    /// </summary>
    public class Translator {

        private readonly string sourceFilepath;
        private readonly string programName;
        
        public Translator(string sourceFilepath, string programName) {
            this.sourceFilepath = sourceFilepath;
            this.programName = programName;
        }

        public void Translate() {
            
            Logger lexicalLogger = new ConsoleLogger();
            Logger treeLogger = new ConsoleLogger();

            LexicalAnalyser lexer = new LexicalAnalyser(sourceFilepath, lexicalLogger);

            CilEmitter emitter = new CilEmitter(programName);
            
            using (lexer) {
                
                Parser parser = new Parser(lexer);

                SyntaxTree syntaxTree = parser.Parse();
                
                syntaxTree.Log(treeLogger);
                
                syntaxTree.Generate(emitter);
            }
            
//            lexicalLogger.Flush();
            treeLogger.Flush();
        }
    }
}