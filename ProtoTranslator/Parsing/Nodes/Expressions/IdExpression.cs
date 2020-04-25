using ProtoTranslator.Debug;
using ProtoTranslator.Generation;
using ProtoTranslator.Lexer.Tokens;

namespace ProtoTranslator.Parsing.Nodes.Expressions {
    public class IdExpression : Expression {
        
        // Offset ?

        private readonly WordToken identifier;

        private ILocalVariable localVariable;
        
        public string Name => identifier.Lexeme;

        public IdExpression(WordToken identifier, TypeToken type, int offset) : base(type.DotNetType) {
            this.identifier = identifier;
        }

        public void Declare(CilEmitter emitter) {
            if (localVariable == null) {
                localVariable = emitter.EmitLocalVarDeclaration(Name, Type);
            }
        }

        public void EmitAssignment(CilEmitter emitter) {
            Declare(emitter);
            localVariable.EmitAssignment();
        }

        public override void EmitRValue(CilEmitter emitter) {
            Declare(emitter);
            localVariable.EmitValue();
        }

        public override void Log(Logger logger) {
            logger.LogLine("Id " + identifier.Lexeme);
        }
    }
}