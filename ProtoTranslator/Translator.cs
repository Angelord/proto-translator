using System;
using ProtoTranslator.Debug;
using ProtoTranslator.Generation;
using ProtoTranslator.Lexer;
using ProtoTranslator.Parsing;

namespace ProtoTranslator {
    /// <summary>
    /// A facade class that provides an easy interface for compiling a file.
    /// </summary>
    public class Translator {

        public bool LogLexer;
        public bool LogSyntaxTree; 
        
        private readonly string sourceFilepath;
        private readonly string programName;
        
        public Translator(string sourceFilepath, string programName) {
            this.sourceFilepath = sourceFilepath;
            this.programName = programName;
        }

        public void Translate() {

            ILogger lexicalLogger = CreateLogger(LogLexer);
            ILogger treeLogger = CreateLogger(LogSyntaxTree);

            LexicalAnalyser lexer = new LexicalAnalyser(sourceFilepath, lexicalLogger);
            CilEmitter emitter = new CilEmitter(programName);
            
            using (lexer) {
                
                Parser parser = new Parser(lexer);

                SyntaxTree syntaxTree = parser.Parse();
                
                syntaxTree.Log(treeLogger);
                
                syntaxTree.Generate(emitter);
            }
            
            lexicalLogger.Flush();
            treeLogger.Flush();
        }

        private ILogger CreateLogger(bool loggingEnabled) {
            if(loggingEnabled) return new ConsoleLogger();
            return new EmptyLogger();
        }
    }
}