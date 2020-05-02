using System;
using System.Collections.Generic;
using System.Text;
using ProtoTranslator.Lexer;
using ProtoTranslator.Parsing.Nodes;
using ProtoTranslator.Parsing.Nodes.Expressions;

namespace ProtoTranslator {
    public class SymbolTable {

        private readonly Dictionary<string, LocalVariableSymbol> variableSymbols = new Dictionary<string, LocalVariableSymbol>();
        private readonly Dictionary<string, FunctionSymbol> functionSymbols = new Dictionary<string, FunctionSymbol>();
        private readonly SymbolTable prev;

        public SymbolTable Prev => prev;

        public SymbolTable() { }

        public SymbolTable(SymbolTable prev) {
            this.prev = prev;
        }

        public LocalVariableSymbol PutVar(string name, Type type) {
            LocalVariableSymbol symbol = new LocalVariableSymbol(name, type);
            variableSymbols.Add(name, symbol);
            return symbol;
        }

        public FunctionSymbol PutFunc(string name, Type returnType, Type[] paramTypes) {
            string signature = BuildFunctionSignature(name, paramTypes);
            FunctionSymbol symbol = new FunctionSymbol(name, returnType);
            functionSymbols[signature] = symbol;
            return symbol;
        }

        public LocalVariableSymbol GetVar(string name) {
            for (SymbolTable env = this; env != null; env = env.prev) {
                if (env.variableSymbols.TryGetValue(name, out LocalVariableSymbol found)) {
                    return found;
                }
            }

            return null;
        }

        public FunctionSymbol GetFunc(string name, Type[] paramTypes) {
            string signature = BuildFunctionSignature(name, paramTypes);
            
            for (SymbolTable env = this; env != null; env = env.prev) {
                if (env.functionSymbols.TryGetValue(signature, out FunctionSymbol found)) {
                    return found;
                }
            }
            return null;
        }

        private string BuildFunctionSignature(string name, Type[] paramTypes) {
            StringBuilder signatureBuilder = new StringBuilder();

            signatureBuilder.Append(name);
            signatureBuilder.Append("_");

            foreach (Type paramType in paramTypes) {
                signatureBuilder.Append(paramType.ToString());
            }

            return signatureBuilder.ToString();
        }
    }
}