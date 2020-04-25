using System;

namespace ProtoTranslator {
    public static class TypeUtils {
        
        public static bool Numeric(Type type) {
            return (type == typeof(int) || type == typeof(float) || type == typeof(char));
        }

        public static Type Max(Type first, Type second) {
            if (first == typeof(float) || second == typeof(float)) return typeof(float);
            if (first == typeof(int) || second == typeof(int)) return typeof(int);
            return typeof(char);
        }
    }
}