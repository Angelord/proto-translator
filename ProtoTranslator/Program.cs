using System;
using System.Collections.Generic;
using System.Text;
using ProtoTranslator.Lexer;

namespace ProtoTranslator {
    internal class Program {
        public static void Main(string[] args) {
            
            Console.WriteLine("Analyzing...");
            
            LexicalAnalyser lexer = new LexicalAnalyser();

            List<Token> result = lexer.Scan("Resources/Program.c");

            StringBuilder outputBuilder = new StringBuilder();
            foreach (Token token in result) {
                outputBuilder.Append(token);
                outputBuilder.Append(" ");
            }
            
            Console.WriteLine("Result :");
            Console.WriteLine(outputBuilder.ToString());
        }
    }
}