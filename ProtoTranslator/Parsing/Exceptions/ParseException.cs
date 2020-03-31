using System;

namespace ProtoTranslator.Parsing.Exceptions {
    public class ParseException : Exception {

        public ParseException(string msg, int line) 
            : base($"Parse exception : '{msg}' at line {line}.") { }
    }
}