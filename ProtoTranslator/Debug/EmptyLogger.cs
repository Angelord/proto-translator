namespace ProtoTranslator.Debug {
    
    public class EmptyLogger : ILogger {
        
        public void IncreaseIndent() { }

        public void DecreaseIndent() { }

        public void Log(object value) { }

        public void LogSpace(object value) { }

        public void LogLine(object value) { }

        public void Flush() { }
    }
}