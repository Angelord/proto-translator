using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using ProtoTranslator.Generation;

namespace ProtoTranslator {
    public class GenerationTester {

        private CilEmitter emitter;
        
        public GenerationTester() {
            emitter = new CilEmitter("GenProgram_EmitterTest");
        }

        public void Test() {
            
            emitter.BeginMethod("main", typeof(Int32), new Type[0]);

//            TestWriteInt();
            
//            TestWriteLine();
            
//            TestIf();
            
//            TestAddition();

//            TestComparison();

//            TestLocalVars();
            
//            TestInputParsing();

            emitter.EmitEmptyRead();
            
            emitter.WriteExecutable();
        }

        private void TestWriteInt() {
            
            emitter.EmitInt32(5);
            
            emitter.EmitWrite(typeof(int));
        }

        private void TestWriteString() {
            
            emitter.EmitString("Woop");

            emitter.EmitWrite(typeof(string));
        }

        private void TestIf() {

            Label label = emitter.GenerateLabel();
            
            emitter.EmitBool(true);
            emitter.EmitIfFalse(label);
            
            emitter.EmitString("Inside if");
            emitter.EmitWrite(typeof(string));
            
            emitter.EmitLabel(label);
            
            emitter.EmitString("Outside if");
            emitter.EmitWrite(typeof(string));
        }

        private void TestAddition() {
         
            emitter.EmitInt32(15);
            emitter.EmitInt32(215);
            
            emitter.EmitBinaryOperator("+");
            
            emitter.EmitWrite(typeof(int));
        }

        private void TestComparison() {
         
            emitter.EmitInt32(100);
            emitter.EmitInt32(100);
            
            emitter.EmitComparison("==");

            Label endLabel = emitter.GenerateLabel();
            emitter.EmitIfFalse(endLabel);
            
            emitter.EmitString("Are equal");
            emitter.EmitWrite(typeof(string));
            emitter.EmitEmptyRead();
            emitter.EmitReturn();
            
            emitter.EmitLabel(endLabel);

            emitter.EmitString("Not Equal");
            emitter.EmitWrite(typeof(string));
        }

        private void TestLocalVars() {

            // A = 5
            LocalVariableInfo aInfo = emitter.EmitLocalVarDeclaration("A", typeof(int));
            emitter.EmitInt32(5);
            emitter.EmitLocalVarAssignment(aInfo);

            // B = 15
            LocalVariableInfo bInfo = emitter.EmitLocalVarDeclaration("B", typeof(int));
            emitter.EmitInt32(15);
            emitter.EmitLocalVarAssignment(bInfo);
            
            // A = A - B
            emitter.EmitLocalVar(aInfo);
            emitter.EmitLocalVar(bInfo);
            emitter.EmitBinaryOperator("-");
            emitter.EmitLocalVarAssignment(aInfo);
            
            // WriteLine(A)
            emitter.EmitLocalVar(aInfo);
            emitter.EmitWrite(typeof(int));
        }
        
        private void TestInputParsing() {
            
            LocalVariableInfo inputVar = emitter.EmitLocalVarDeclaration("input", typeof(Int32));
            
            emitter.EmitRead();
            emitter.EmitParse(typeof(Int32));
            emitter.EmitLocalVarAssignment(inputVar);
            
            emitter.EmitLocalVar(inputVar);
            emitter.EmitWrite(typeof(int));
        }
    }
}