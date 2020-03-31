using System.Collections.Generic;

namespace ProtoTranslator {
    public class SymbolTable {

        private readonly Dictionary<string, Symbol> symTable = new Dictionary<string, Symbol>();
        private readonly SymbolTable prev;

        public SymbolTable Prev => prev;

        public SymbolTable() { }

        public SymbolTable(SymbolTable prev) {
            this.prev = prev;
        }

        public void Put(string key, Symbol sym) {
            symTable.Add(key, sym);
        }

        public T Get<T>(string key) where T : Symbol {
            return Get(key) as T;
        }

        public Symbol Get(string key) {
            for (SymbolTable env = this; env != null; env = env.prev) {
                if (env.symTable.TryGetValue(key, out Symbol found)) {
                    return found;
                }
            }

            return null;
        }
    }
}