using System;
using ProtoTranslator.Parsing.Nodes;

namespace ProtoTranslator.Parsing {
    public class ParseUtils {

        public static Type[] ExpressionArrayToTypes(Expression[] expressions) {
            Type[] types = new Type[expressions.Length];

            for (int i = 0; i < expressions.Length; i++) {
                types[i] = expressions[i].ReturnType;
            }

            return types;
        }
    }
}