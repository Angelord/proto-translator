using System.Collections.Generic;
using ProtoTranslator.Lexer;
using ProtoTranslator.Parsing.Nodes;
using ProtoTranslator.Parsing.Nodes.Expressions;

namespace ProtoTranslator {
    public class SymbolTable {

        private readonly Dictionary<Token, IdExpression> symTable = new Dictionary<Token, IdExpression>();
        private readonly SymbolTable prev;

        public SymbolTable Prev => prev;

        public SymbolTable() { }

        public SymbolTable(SymbolTable prev) {
            this.prev = prev;
        }

        public void Put(Token key, IdExpression id) {
            symTable.Add(key, id);
        }

        public IdExpression Get(Token key) {
            for (SymbolTable env = this; env != null; env = env.prev) {
                if (env.symTable.TryGetValue(key, out IdExpression found)) {
                    return found;
                }
            }

            return null;
        }
    }
}