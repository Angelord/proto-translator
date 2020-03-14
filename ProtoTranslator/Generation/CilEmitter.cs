﻿using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;

namespace ProtoTranslator.Generation {

    public enum IncDecOperators {
        None, 
        PreInc,
        PreDec, 
        PostInc,
        PostDec
    }

    public class CilEmitter {

        private static readonly MethodInfo ConcatMethodInfo =
            typeof(String).GetMethod("Concat", new Type[] {typeof(object), typeof(object)});
        
        private static readonly MethodInfo ReadLineMethodInfo = typeof(Console).GetMethod("ReadLine", new Type[0]);

        private readonly string exeExePath;
        private readonly AssemblyBuilder assembly;
        private readonly TypeBuilder program;
        private readonly ILGenerator ilGeneratorConstructor;
        private MethodBuilder method;
        private ILGenerator ilGenerator;
        private bool currentMethodIsMain;
        private bool hasMainMethod;

        public CilEmitter(string programName) {

            string exePath = String.Format("{0}/{1}.exe", Program.ExecutionDir, programName);
            
            exeExePath = exePath;

            AssemblyName assemblyName = new AssemblyName {
                Name = Path.GetFileNameWithoutExtension(exePath)
            };

            string dir = Path.GetDirectoryName(exePath);
            string moduleName = Path.GetFileName(exePath);

            assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Save, dir);

            ModuleBuilder moduleBuilder = assembly.DefineDynamicModule(assemblyName + "Module", moduleName, true);

            program = moduleBuilder.DefineType(moduleName, TypeAttributes.Class | TypeAttributes.Public);

            ConstructorBuilder cctor = program.DefineConstructor(MethodAttributes.Static | MethodAttributes.Public,
                CallingConventions.Standard, Type.EmptyTypes);
            ilGeneratorConstructor = cctor.GetILGenerator();
            ilGeneratorConstructor.BeginScope();
        }

        public void WriteExecutable() {
            EndMethod();

            // TODO : Create custom exception
            if (!hasMainMethod) {
                throw new InvalidOperationException("No main method defined for program!");
            }

            ilGeneratorConstructor.Emit(OpCodes.Ret);
            ilGeneratorConstructor.EndScope();

            program.CreateType();

            assembly.Save(Path.GetFileName(exeExePath));
        }

        public MethodInfo BeginMain() {
            return BeginMethod("main", typeof(Int32), new Type[0]);
        }

        public MethodInfo BeginMethod(string methodName, Type returnType, Type[] parameterTypes) {
            EndMethod();

            method = program.DefineMethod(methodName, MethodAttributes.Public | MethodAttributes.Static, returnType,
                parameterTypes);
            method.InitLocals = true;
            ilGenerator = method.GetILGenerator();

            BeginScope();

            currentMethodIsMain = false;
            if (methodName.ToLower() == "main" && returnType == typeof(Int32) && parameterTypes.Length == 0) {
                currentMethodIsMain = true;
                hasMainMethod = true;
                assembly.SetEntryPoint(method);
            }

            return method;
        }

        public void EndMethod() {
            if (method == null) return;

            EmitReturn();

            EndScope();
        }

        public void EmitReturn() {
            if(currentMethodIsMain) ilGenerator.Emit(OpCodes.Ldc_I4_0);
            ilGenerator.Emit(OpCodes.Ret);
        }

        public void EmitWrite(Type type) {
            MethodInfo writeLineMethod = typeof(Console).GetMethod("WriteLine", new Type[] {type});
            ilGenerator.Emit(OpCodes.Call, writeLineMethod);
        }

        public void EmitEmptyRead() {
            ilGenerator.Emit(OpCodes.Call, ReadLineMethodInfo);
            EmitPop();
        }

        public void EmitRead() {
            ilGenerator.Emit(OpCodes.Call, ReadLineMethodInfo);
        }

        public void EmitPop() {
            ilGenerator.Emit(OpCodes.Pop);
        }

        public void EmitBox() {
            ilGenerator.Emit(OpCodes.Box);
        }

        public void EmitUnbox() {
            ilGenerator.Emit(OpCodes.Unbox);
        }

        public void BeginScope() {
            ilGenerator.BeginScope();
        }

        public void EndScope() {
            ilGenerator.EndScope();
        }

        public void EmitComparison(string op) {
            switch (op) {
                case "<":
                    ilGenerator.Emit(OpCodes.Clt);
                    break;
                case "<=":
                    ilGenerator.Emit(OpCodes.Cgt);
                    ilGenerator.Emit(OpCodes.Ldc_I4_0);
                    ilGenerator.Emit(OpCodes.Ceq);
                    break;
                case ">":
                    ilGenerator.Emit(OpCodes.Cgt);
                    break;
                case ">=":
                    ilGenerator.Emit(OpCodes.Clt);
                    ilGenerator.Emit(OpCodes.Ldc_I4_0);
                    ilGenerator.Emit(OpCodes.Ceq);
                    break;
                case "==":
                    ilGenerator.Emit(OpCodes.Ceq);
                    break;
                case "!=":
                    ilGenerator.Emit(OpCodes.Ceq);
                    ilGenerator.Emit(OpCodes.Ldc_I4_0);
                    ilGenerator.Emit(OpCodes.Ceq);
                    break;
            }
        }

        public void EmitParse(Type toType) {
            MethodInfo parseMethod = toType.GetMethod("Parse", new Type[] {typeof(string)});
            ilGenerator.Emit(OpCodes.Call, parseMethod);
        }

        public void EmitCast(Type castType, Type exprType) {
            if (!castType.IsValueType && !exprType.IsValueType)
                ilGenerator.Emit(OpCodes.Castclass, castType);
            else if (!castType.IsValueType && exprType.IsValueType)
                ilGenerator.Emit(OpCodes.Box, exprType);
            else if (castType.IsValueType && !exprType.IsValueType)
                ilGenerator.Emit(OpCodes.Unbox_Any, castType);
            else {
                if (castType == typeof(Single) && exprType == typeof(UInt32))
                    ilGenerator.Emit(OpCodes.Conv_R_Un);
                else if (castType == typeof(SByte))
                    ilGenerator.Emit(OpCodes.Conv_I1);
                else if (castType == typeof(Int16))
                    ilGenerator.Emit(OpCodes.Conv_I2);
                else if (castType == typeof(Int16))
                    ilGenerator.Emit(OpCodes.Conv_I2);
                else if (castType == typeof(Int32))
                    ilGenerator.Emit(OpCodes.Conv_I4);
                else if (castType == typeof(Int64))
                    ilGenerator.Emit(OpCodes.Conv_I8);
                else if (castType == typeof(Single))
                    ilGenerator.Emit(OpCodes.Conv_R4);
                else if (castType == typeof(Double))
                    ilGenerator.Emit(OpCodes.Conv_R8);
                else if (castType == typeof(Byte))
                    ilGenerator.Emit(OpCodes.Conv_U1);
                else if (castType == typeof(UInt16))
                    ilGenerator.Emit(OpCodes.Conv_U2);
                else if (castType == typeof(UInt32))
                    ilGenerator.Emit(OpCodes.Conv_U4);
                else if (castType == typeof(UInt64))
                    ilGenerator.Emit(OpCodes.Conv_U8);
            }

            if (!castType.IsValueType && exprType.IsValueType) {
                ilGenerator.Emit(OpCodes.Unbox_Any, castType);
            }
        }
        
        public void EmitIncLocalVar(LocalVariableInfo localVariableInfo, IncDecOperators @operator) {
            switch (@operator) {
                case IncDecOperators.PostInc:
                    ilGenerator.Emit(OpCodes.Dup);
                    ilGenerator.Emit(OpCodes.Ldc_I4_1);
                    ilGenerator.Emit(OpCodes.Add);
                    ilGenerator.Emit(OpCodes.Stloc, (LocalBuilder) localVariableInfo);
                    break;
                case IncDecOperators.PostDec:
                    ilGenerator.Emit(OpCodes.Dup);
                    ilGenerator.Emit(OpCodes.Ldc_I4_1);
                    ilGenerator.Emit(OpCodes.Sub);
                    ilGenerator.Emit(OpCodes.Stloc, (LocalBuilder) localVariableInfo);
                    break;
                case IncDecOperators.PreInc:
                    ilGenerator.Emit(OpCodes.Ldc_I4_1);
                    ilGenerator.Emit(OpCodes.Add);
                    ilGenerator.Emit(OpCodes.Dup);
                    ilGenerator.Emit(OpCodes.Stloc, (LocalBuilder) localVariableInfo);
                    break;
                case IncDecOperators.PreDec:
                    ilGenerator.Emit(OpCodes.Ldc_I4_1);
                    ilGenerator.Emit(OpCodes.Sub);
                    ilGenerator.Emit(OpCodes.Dup);
                    ilGenerator.Emit(OpCodes.Stloc, (LocalBuilder) localVariableInfo);
                    break;
            }
        }
        
        public void EmitBinaryOperator(string op) {
            switch (op) {
                case "+":
                    ilGenerator.Emit(OpCodes.Add);
                    break;
                case "-":
                    ilGenerator.Emit(OpCodes.Sub);
                    break;
                case "^":
                    ilGenerator.Emit(OpCodes.Xor);
                    break;
                case "*":
                    ilGenerator.Emit(OpCodes.Mul);
                    break;
                case "/":
                    ilGenerator.Emit(OpCodes.Div);
                    break;
                case "%":
                    ilGenerator.Emit(OpCodes.Rem);
                    break;
                case "|":
                    ilGenerator.Emit(OpCodes.Or);
                    break;
                case "&":
                    ilGenerator.Emit(OpCodes.And);
                    break;
                case "||":
                    ilGenerator.Emit(OpCodes.Or);
                    break;
                case "&&":
                    ilGenerator.Emit(OpCodes.And);
                    break;
            }
        }
        
        public void EmitUnaryOp(string op) {
            switch (op) {
                case "-":
                    ilGenerator.Emit(OpCodes.Neg);
                    break;
                case "!":
                    ilGenerator.Emit(OpCodes.Not);
                    break;
                case "~":
                    ilGenerator.Emit(OpCodes.Not);
                    break;
            }
        }

        public void EmitAssignCast(Type targetType, Type exprType) {
            if (!targetType.IsValueType && exprType.IsValueType)
                ilGenerator.Emit(OpCodes.Box, exprType);
            else if (targetType.IsValueType && !exprType.IsValueType)
                ilGenerator.Emit(OpCodes.Unbox_Any, targetType);
            //else ;
        }

        public void EmitConcatenationOp() {
            ilGenerator.Emit(OpCodes.Call, ConcatMethodInfo);
        }

        public LocalBuilder EmitLocalVarDeclaration(string localVarName, Type localVarType) {
            LocalBuilder localBuilder = ilGenerator.DeclareLocal(localVarType);
//            if (CanInitializeLocation(localVarType)) {
//                // Store the already prepared initializer
//                ilGenerator.Emit(OpCodes.Stloc, localBuilder);
//            }
            localBuilder.SetLocalSymInfo(localVarName);

            return localBuilder;
        }

        private bool CanInitializeLocation(Type type) {
            if (!type.IsValueType) {
                if (type == typeof(String)) {
                    ilGenerator.Emit(OpCodes.Ldstr, "");
                }
                else {
                    ConstructorInfo cons = type.GetConstructor(Type.EmptyTypes);
                    if (cons != null && !type.IsAbstract) {
                        ilGenerator.Emit(OpCodes.Newobj, type);
                    }
                    else {
                        ilGenerator.Emit(OpCodes.Ldnull);
                    }
                }

                return true;
            }

            return false;
        }
        
        public void EmitLocalVarAssignment(LocalVariableInfo localVariableInfo) {
            ilGenerator.Emit(OpCodes.Stloc, (LocalBuilder) localVariableInfo);
        }
        
        public Label GenerateLabel() {
            return ilGenerator.DefineLabel();
        }

        public void EmitLabel(Label label) {
            ilGenerator.MarkLabel(label);
        }

        public void EmitIfFalse(Label label) {
            ilGenerator.Emit(OpCodes.Brfalse, label);
        }

        public void EmitJump(Label label) {
            ilGenerator.Emit(OpCodes.Br, label);
        }
        
        public void EmitLocalVar(LocalVariableInfo localVariableInfo) {
            ilGenerator.Emit(OpCodes.Ldloc, (LocalBuilder) localVariableInfo);
        }

        public void EmitInt32(int value) {
            ilGenerator.Emit(OpCodes.Ldc_I4, value);
        }

        public void EmitDouble(double value) {
            ilGenerator.Emit(OpCodes.Ldc_R8, value);
        }

        public void EmitBool(bool value) {
            ilGenerator.Emit(OpCodes.Ldc_I4, value ? 1 : 0);
        }

        public void EmitChar(char value) {
            ilGenerator.Emit(OpCodes.Ldc_I4_S, value);
        }

        public void EmitString(string value) {
            ilGenerator.Emit(OpCodes.Ldstr, value);
        }

        public void EmitNull() {
            ilGenerator.Emit(OpCodes.Ldnull);
        }
    }
}