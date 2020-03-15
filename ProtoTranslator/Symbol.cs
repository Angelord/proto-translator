using System;
using System.Reflection;

namespace ProtoTranslator {
    public class Symbol {
        
        public readonly Type Type;

        public readonly LocalVariableInfo LocalVariableInfo;

        public Symbol(Type type, LocalVariableInfo localVariableInfo) {
            Type = type;
            LocalVariableInfo = localVariableInfo;
        }
    }
}