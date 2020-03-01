using System.Collections.Generic;

namespace ProtoTranslator {
    public class Environment {

        private Dictionary<string, Symbol> symTable;
        private Environment prev;

        public Environment(Environment prev) {
            this.prev = prev;
        }

        public void Put(string key, Symbol sym) {
            symTable.Add(key, sym);
        }

        public Symbol Get(string key) {
            for (Environment env = this; env != null; env = env.prev) {
                if (env.symTable.TryGetValue(key, out Symbol found)) {
                    return found;
                }
            }

            return null;
        }
    }
}