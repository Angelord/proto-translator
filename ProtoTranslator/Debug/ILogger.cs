namespace ProtoTranslator.Debug {

    public interface ILogger {

        void IncreaseIndent();

        void DecreaseIndent();
        
        void Log(object value);

        void LogSpace(object value);

        void LogLine(object value);

        void Flush();
    }
}