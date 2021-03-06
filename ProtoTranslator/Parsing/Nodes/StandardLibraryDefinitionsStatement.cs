﻿using System;
using ProtoTranslator.Debug;
using ProtoTranslator.Generation;
using ProtoTranslator.Lexer.Tokens;

namespace ProtoTranslator.Parsing.Nodes {
    // Makes sure the symbol table contains all the standard library functions such as printf and scanf
    public class StandardLibraryDefinitionsStatement : Statement {

        private readonly FunctionSymbol printInt;
        private readonly FunctionSymbol printFloat;
        private readonly FunctionSymbol printChar;
        private readonly FunctionSymbol printBool;

        private readonly FunctionSymbol scanF;
        
        public StandardLibraryDefinitionsStatement(SymbolTable table) {
            printInt = table.PutFunc(WordToken.PrintF.Lexeme, typeof(void), new[] {typeof(int)});
            printFloat = table.PutFunc(WordToken.PrintF.Lexeme, typeof(void), new[] {typeof(float)});
            printChar = table.PutFunc(WordToken.PrintF.Lexeme, typeof(void), new[] {typeof(char)});
            printBool = table.PutFunc(WordToken.PrintF.Lexeme, typeof(void), new[] {typeof(bool)});

            scanF = table.PutFunc(WordToken.ScanF.Lexeme, typeof(string), new Type[0]);
        }

        protected override void DoGenerate(CilEmitter emitter) {
            printInt.Function = emitter.GetWriteFunction(typeof(int));
            printFloat.Function = emitter.GetWriteFunction(typeof(float));
            printChar.Function = emitter.GetWriteFunction(typeof(char));
            printBool.Function = emitter.GetWriteFunction(typeof(bool));

            scanF.Function = emitter.GetReadLineFunction();
        }

        public override void Log(ILogger logger) {
            logger.LogLine("Standard library functions");
        }
    }
}