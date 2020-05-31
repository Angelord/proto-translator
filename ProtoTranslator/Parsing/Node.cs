using ProtoTranslator.Debug;
using ProtoTranslator.Generation;
using ProtoTranslator.Parsing.Exceptions;

namespace ProtoTranslator.Parsing {
    public abstract class Node {

        public int lexline = 0;
        
        public void Error(string msg) {
            throw new ParseException(msg, lexline);
        }
        
        public virtual void Log(ILogger logger) { }
    }
}