using System.IO;
using System.Diagnostics;

namespace ProtoTranslator {
    internal class Program {

        public static string ExecutionDir => Path.GetDirectoryName(Process.GetCurrentProcess().MainModule?.FileName);

        public static void Main(string[] args) {

            Translator translator = new Translator("Resources/Program.c", "Result/GenProgram_IncrTest");

            translator.LogLexer = true;
            translator.LogSyntaxTree = true;
            
            translator.Translate();
        }
    }
}