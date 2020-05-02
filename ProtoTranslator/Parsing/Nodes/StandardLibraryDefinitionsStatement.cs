using System;
using ProtoTranslator.Debug;
using ProtoTranslator.Generation;

namespace ProtoTranslator.Parsing.Nodes {
    public class StandardLibraryDefinitionsStatement : Statement {

        private readonly FunctionSymbol printInt;
        private readonly FunctionSymbol printFloat;
        private readonly FunctionSymbol printChar;
        private readonly FunctionSymbol printBool;
        
        public StandardLibraryDefinitionsStatement(SymbolTable table) {
            printInt = table.PutFunc("printf", typeof(void), new[] {typeof(int)});
            printFloat = table.PutFunc("printf", typeof(void), new[] {typeof(float)});
            printChar = table.PutFunc("printf", typeof(void), new[] {typeof(char)});
            printBool = table.PutFunc("printf", typeof(void), new[] {typeof(bool)});
        }

        public override void Generate(CilEmitter emitter, ILabel begin, ILabel after) {

            printInt.Function = emitter.GetWriteFunction(typeof(int));
            printFloat.Function = emitter.GetWriteFunction(typeof(float));
            printChar.Function = emitter.GetWriteFunction(typeof(char));
            printBool.Function = emitter.GetWriteFunction(typeof(bool));
        }

        public override void Log(Logger logger) {
            logger.LogLine("Standard library functions");
        }
    }
}