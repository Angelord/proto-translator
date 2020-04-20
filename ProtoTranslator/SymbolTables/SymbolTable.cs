using System.Collections.Generic;
using ProtoTranslator.Lexer;
using ProtoTranslator.Parsing.Nodes;

namespace ProtoTranslator {
    public class SymbolTable {

        private readonly Dictionary<Token, IdExpr> symTable = new Dictionary<Token, IdExpr>();
        private readonly SymbolTable prev;

        public SymbolTable Prev => prev;

        public SymbolTable() { }

        public SymbolTable(SymbolTable prev) {
            this.prev = prev;
        }

        public void Put(Token key, IdExpr id) {
            symTable.Add(key, id);
        }

        public IdExpr Get(Token key) {
            for (SymbolTable env = this; env != null; env = env.prev) {
                if (env.symTable.TryGetValue(key, out IdExpr found)) {
                    return found;
                }
            }

            return null;
        }
    }
}