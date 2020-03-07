using System.Text;

namespace ProtoTranslator.Debug {
    public abstract class Logger {
        
        private readonly StringBuilder builder = new StringBuilder();

        public void Log(object value) {
            builder.Append(value);
        }

        public void LogSpace(object value) {
            builder.Append(value);
            builder.Append(' ');
        }

        public void LogLine(object value) {
            builder.AppendLine(value.ToString());
        }

        public void Flush() {
            DoFlush(builder.ToString());
            builder.Clear();
        }

        protected abstract void DoFlush(string contents);
    }
}