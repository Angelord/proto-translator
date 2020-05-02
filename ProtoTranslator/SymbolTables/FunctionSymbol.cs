using System;
using System.Collections.Generic;
using System.Text;
using ProtoTranslator.Generation;

namespace ProtoTranslator {
    public class FunctionSymbol : Symbol {
        
//        private Dictionary<string, IFunction> overrides = new Dictionary<string, IFunction>();

        public IFunction Function;
        
        public readonly Type ReturnType;

        public FunctionSymbol(string name, Type returnType) : base(name) {
            ReturnType = returnType;
        }
//
//        public void DeclareOverride(Type[] parameters, IFunction functionOverride) {
//            string signature = TypeArrayToString(parameters);
//            overrides.Add(signature, null);
//        }
//
//        public void SetOverride(Type[] parameters, IFunction functionOverride) {
//            string signature = TypeArrayToString(parameters);
//            overrides[signature] = functionOverride;
//        }
//
//        public bool ContainsOverride() {
//            
//        }
//
//        public IFunction GetOverride(Type[] parameters) {
//            string signature = TypeArrayToString(parameters);
//            if (overrides.TryGetValue(signature, out IFunction funcOverride)) {
//                return funcOverride;
//            }
//
//            return null;
//        }
//
//        private string TypeArrayToString(Type[] parameters) {
//            StringBuilder stringBuilder = new StringBuilder();
//            foreach (Type parameter in parameters) {
//                stringBuilder.Append(parameter.ToString());
//                stringBuilder.Append("_");
//            }
//            return stringBuilder.ToString();
//        }
    }
}