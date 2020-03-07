using System;

namespace ProtoTranslator.Debug {
    public class ConsoleLogger : Logger {
        
        protected override void DoFlush(string contents) {
            Console.Write(contents);
        }
    }
}