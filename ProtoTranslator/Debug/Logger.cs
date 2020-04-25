using System.Text;

namespace ProtoTranslator.Debug {
    public abstract class Logger {

        private int indent = 0;
        private readonly StringBuilder builder = new StringBuilder();

        // Increase the indentation at the start of new lines
        public void IncreaseIndent() {
            indent++;
        }

        // Decrease the indentation at the start of new lines
        public void DecreaseIndent() {
            indent--;
            if (indent < 0) indent = 0;
        }

        public void Log(object value) {
            builder.Append(value);
        }

        public void LogSpace(object value) {
            builder.Append(value);
            builder.Append(' ');
        }

        public void LogLine(object value) {
            builder.AppendLine();
            for (int i = 0; i < indent; i++) {
                builder.Append(' ');
            }
            
            builder.Append(value.ToString());
        }

        public void Flush() {
            DoFlush(builder.ToString());
            builder.Clear();
        }

        protected abstract void DoFlush(string contents);
    }
}