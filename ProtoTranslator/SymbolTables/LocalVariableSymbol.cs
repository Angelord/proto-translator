using System;
using ProtoTranslator.Generation;

namespace ProtoTranslator {
    public class LocalVariableSymbol : Symbol {
        
        public ILocalVariable Variable;

        public readonly Type VariableType;
        
        public LocalVariableSymbol(string name, Type variableType) : base(name) {
            VariableType = variableType;
        }
    }
}