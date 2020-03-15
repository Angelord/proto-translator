using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using ProtoTranslator.Debug;
using ProtoTranslator.Generation;
using ProtoTranslator.Lexer;
using ProtoTranslator.Parsing;
using ProtoTranslator.Parsing.Nodes;

namespace ProtoTranslator {
    internal class Program {

        public static string ExecutionDir => Path.GetDirectoryName(Process.GetCurrentProcess().MainModule?.FileName);

        public static void Main(string[] args) {

            TestAbstractSyntaxTree();
            
//            GenerationTester tester = new GenerationTester();
//            tester.Test();

//            Logger lexicalLogger = new ConsoleLogger();
//            
//            LexicalAnalyser lexer = new LexicalAnalyser("Resources/Program.c", lexicalLogger);
//            
//            using (lexer) {
//                
//                Parser parser = new Parser();
//
//                parser.Parse(lexer);
//            }
//            
//            lexicalLogger.Flush();
        }

        private static void TestAbstractSyntaxTree() {
            
            CilEmitter emitter = new CilEmitter("GenProgram_ASTTest");

            emitter.BeginMain();
            
            Statement root = new Stmt_Seq(
                new Stmt_If(
                    new Expr_Comparison(">", new Literal_Integer(-10), new Literal_Integer(5)), 
                    new Stmt_Print(new Literal_String("Is True"))
                ),
                new Stmt_Seq(
                    new Stmt_Print(new Expr_Scan()), 
                    new Stmt_Print(new Literal_String("End"))
                )
            ); 
            
            root.Generate(emitter);
            
            emitter.EmitEmptyRead();
            
            emitter.WriteExecutable();
        }
    }
}