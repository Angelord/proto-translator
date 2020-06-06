using System;
using System.IO;
using System.Diagnostics;

namespace ProtoTranslator {
    internal class Program {

        private const string SOURCE_FOLDER = "Resources/";
        private const string BIN_FOLDER = "Builds/";

        public static string ExecutionDir => Path.GetDirectoryName(Process.GetCurrentProcess().MainModule?.FileName);

        public static void Main(string[] args) {

            Console.WriteLine("Select the input program : ");
            string inputProgramName = Console.ReadLine();
            string sourceProgramPath = $"{SOURCE_FOLDER}{inputProgramName}.c";
            string destProgramPath = $"{BIN_FOLDER}{inputProgramName}";
            
            if (!File.Exists(sourceProgramPath)) {
                Console.WriteLine("Program not found!");
                return;
            }

            Translator translator = new Translator(sourceProgramPath, destProgramPath);
            translator.LogLexer = true;
            translator.LogSyntaxTree = true;
            translator.Translate();
        }
    }
}