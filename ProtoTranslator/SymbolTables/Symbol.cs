﻿using System;
 using ProtoTranslator.Generation;

 namespace ProtoTranslator {

    public abstract class Symbol {

        public readonly string Name;

        protected Symbol(string name) {
            Name = name;
        }
    }

    public class LocalVariableSymbol : Symbol {
        
        public ILocalVariable Variable;

        public readonly Type VariableType;
        
        public LocalVariableSymbol(string name, Type variableType) : base(name) {
            VariableType = variableType;
        }
    }

    public class FunctionSymbol : Symbol {

        public IFunction Function;
        
        public readonly Type ReturnType;

        public FunctionSymbol(string name, Type returnType) : base(name) {
            ReturnType = returnType;
        }
    }
 }