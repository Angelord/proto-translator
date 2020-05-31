using System.Globalization;

namespace ProtoTranslator.Lexer.Tokens {
    // Token representing floating point numbers
    public class RealToken : Token {

        public readonly float Value;

        public RealToken(float value) : base(Tags.REAL) {
            Value = value;
        }

        public override string GetLexeme() {
            return Value.ToString(CultureInfo.InvariantCulture);
        }

        public override string ToString() {
            return $"<{Tag}, {Value}>";
        }
    }
}