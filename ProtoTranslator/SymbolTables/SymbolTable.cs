using System.Collections.Generic;
using ProtoTranslator.Lexer;
using ProtoTranslator.Parsing.Nodes;
using ProtoTranslator.Parsing.Nodes.Expressions;

namespace ProtoTranslator {
    public class SymbolTable {

        private readonly Dictionary<Token, Symbol> symTable = new Dictionary<Token, Symbol>();
        private readonly SymbolTable prev;

        public SymbolTable Prev => prev;

        public SymbolTable() { }

        public SymbolTable(SymbolTable prev) {
            this.prev = prev;
        }

        public void Put(Token key, Symbol variable) {
            symTable.Add(key, variable);
        }

        public Symbol Get(Token key) {
            for (SymbolTable env = this; env != null; env = env.prev) {
                if (env.symTable.TryGetValue(key, out Symbol found)) {
                    return found;
                }
            }

            return null;
        }
    }
}