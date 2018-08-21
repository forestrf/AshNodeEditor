using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Ashkatchap.AIBrain.GeneratedNodes {

	public static class TemplateNode {

		// TO DO: CLASS NAME has to include the type of the input parameters if it is a method to allow overloading

		public static string GenerateFile(MemberInfo memberInfo, out string filename) {
			List<KeyValuePair<string, Type>> input = new List<KeyValuePair<string, Type>>();
			List<KeyValuePair<string, Type>> output = new List<KeyValuePair<string, Type>>();

			StringBuilder builder = new StringBuilder();
			builder.Append("//////////////////////////////////////\n");
			builder.Append("//// FILE GENERATED AUTOMATICALLY ////\n");
			builder.Append("//////////////////////////////////////\n");
			builder.Append("\n");
			builder.Append("using System;\n");
			builder.Append("using Ashkatchap.AIBrain.Nodes;\n");
			builder.Append("\n");
			builder.Append("namespace Ashkatchap.AIBrain.GeneratedNodes {\n");
			builder.Append("	[Serializable]\n");
			builder.Append("	[CreateNode(\"Actuator/" + memberInfo.DeclaringType.FullName.Replace('.', '/') + "/" + TypeFinder.GetDisplayName(memberInfo, false, false) + "\")]\n");
			filename = "GN_" + (memberInfo.DeclaringType.FullName + "_" + memberInfo.Name).Replace('.', '_');
			if (memberInfo.MemberType == MemberTypes.Method) {
				string methodSignature = string.Join("__", ((MethodInfo) memberInfo).GetParameters().Select(p => p.ParameterType.FullName.Replace('.', '_')).ToArray());
				filename += "_" + GetHashString(methodSignature);
			}
			filename = filename.Replace("&", "_Out_");
			builder.Append("	public class " + filename + " : Node {\n");
			filename += ".cs";

			bool useReference = memberInfo.MemberType == MemberTypes.Method && !((MethodInfo) memberInfo).IsStatic;
			useReference |= memberInfo.MemberType == MemberTypes.Field && !((FieldInfo) memberInfo).IsStatic;
			useReference |= memberInfo.MemberType == MemberTypes.Property && !(
				(((PropertyInfo) memberInfo).GetGetMethod(false) != null && ((PropertyInfo) memberInfo).GetGetMethod(false).IsStatic) ||
				(((PropertyInfo) memberInfo).GetSetMethod(false) != null && ((PropertyInfo) memberInfo).GetSetMethod(false).IsStatic)
				);

			if (useReference) {
				builder.Append("		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_" + TemplateIO.GetIOName(memberInfo.DeclaringType) + " refObject;\n");
				if (memberInfo.DeclaringType.IsValueType) {
					builder.Append("		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_" + TemplateIO.GetIOName(memberInfo.DeclaringType) + " newRefObject;\n");
				}
			}

			bool isSpecialSetMethod = memberInfo.MemberType == MemberTypes.Method && (memberInfo as MethodInfo).IsSpecialName && memberInfo.Name.StartsWith("set_");

			ParameterInfo[] parameters = null;
			bool methodHasReturn = false;
			bool canWrite = memberInfo.MemberType == MemberTypes.Field && !((FieldInfo) memberInfo).IsInitOnly && !((FieldInfo) memberInfo).IsLiteral;
			canWrite |= memberInfo.MemberType == MemberTypes.Property && (memberInfo as PropertyInfo).CanWrite;
			canWrite |= isSpecialSetMethod;
			canWrite &= !memberInfo.DeclaringType.IsValueType;
			bool canRead = memberInfo.MemberType == MemberTypes.Field || (memberInfo.MemberType == MemberTypes.Property && (memberInfo as PropertyInfo).CanRead);
			Type fieldPropertyType = memberInfo.MemberType == MemberTypes.Field ? (memberInfo as FieldInfo).FieldType : memberInfo.MemberType == MemberTypes.Property ? (memberInfo as PropertyInfo).PropertyType : null;
			if (memberInfo.MemberType == MemberTypes.Method) {
				var method = memberInfo as MethodInfo;
				parameters = method.GetParameters();
				for (int i = 0; i < parameters.Length; i++) {
					if (parameters[i].IsOut) {
						builder.Append("		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_" + TemplateIO.GetIOName(parameters[i].ParameterType) + " " + parameters[i].Name + ";\n");
						output.Add(new KeyValuePair<string, Type>(parameters[i].Name, parameters[i].ParameterType));
					} else {
						builder.Append("		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_" + TemplateIO.GetIOName(parameters[i].ParameterType) + " " + parameters[i].Name + ";\n");
						input.Add(new KeyValuePair<string, Type>(parameters[i].Name, parameters[i].ParameterType));
					}
				}
				methodHasReturn = method.ReturnType != typeof(void);
				if (methodHasReturn) {
					builder.Append("\n");
					builder.Append("		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_" + TemplateIO.GetIOName(method.ReturnType) + " returnVar;\n");
					output.Add(new KeyValuePair<string, Type>("returnVar", method.ReturnType));
				}

				if (isSpecialSetMethod) {
					fieldPropertyType = parameters[0].ParameterType;
				}
			} else {
				if (canRead) {
					builder.Append("		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_" + TemplateIO.GetIOName(fieldPropertyType) + " getter;\n");
					output.Add(new KeyValuePair<string, Type>("getter", fieldPropertyType));
				}
			}
			if (canWrite) {
				builder.Append("		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_" + TemplateIO.GetIOName(fieldPropertyType) + " setter;\n");
				input.Add(new KeyValuePair<string, Type>("setter", fieldPropertyType));
			}

			builder.Append("\n");
			builder.Append("\n");



			builder.Append("#if UNITY_EDITOR\n");
			builder.Append("		public override void Init() {\n");
			var nodeName = memberInfo.Name;
			if (memberInfo.MemberType == MemberTypes.Method && ((MethodInfo) memberInfo).IsSpecialName) {
				var name = memberInfo.Name;
				if (name.StartsWith("get_")) {
					nodeName = name.Substring("get_".Length);
				} else if (name.StartsWith("op_")) {
					nodeName = OperatorFromName[name];
				}
			}
			builder.Append("			SetName(\"" + nodeName + "\");\n");
			if (useReference) {
				builder.Append("			refObject = " + CreateIOConstructorParam(memberInfo.DeclaringType, "Input") + ";\n");
				if (memberInfo.DeclaringType.IsValueType) {
					builder.Append("			newRefObject = " + CreateIOConstructorParam(memberInfo.DeclaringType, "Output") + ";\n");
				}
			}
			foreach (var i in input) {
				builder.Append("			" + i.Key + " = " + CreateIOConstructorParam(i.Value, "Input") + ";\n");
			}
			foreach (var i in output) {
				builder.Append("			" + i.Key + " = " + CreateIOConstructorParam(i.Value, "Output") + ";\n");
			}
			builder.Append("		}\n");
			builder.Append("#endif\n");

			builder.Append("\n");

			builder.Append("		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {\n");
			if (memberInfo.MemberType == MemberTypes.Method && !isSpecialSetMethod) {
				builder.Append("			Calculate();\n");
			} else if (canWrite) {
				if (useReference) {
					builder.Append("			((" + memberInfo.DeclaringType.FullName + ") refObject.GetValue()).");
				} else {
					builder.Append(memberInfo.DeclaringType.FullName + ".");
				}
				if (isSpecialSetMethod) {
					builder.Append(memberInfo.Name.Substring("set_".Length));
				} else {
					builder.Append(memberInfo.Name);
				}
				builder.Append(" = setter.GetValue();\n");
			}
			
			builder.Append("			executionResult = ExecutionResult.Success;\n");
			builder.Append("			return null;\n");
			builder.Append("		}\n");

			builder.Append("\n");

			builder.Append("		public override void Calculate() {");
			if (canRead) {
				builder.Append("\n");
				builder.Append("				getter.SetValue(");
				if (useReference) {
					builder.Append("((" + memberInfo.DeclaringType.FullName + ") refObject.GetValue()).");
				} else {
					builder.Append(memberInfo.DeclaringType.FullName + ".");
				}
				builder.Append(memberInfo.Name + ");\n");
			} else if (!isSpecialSetMethod) {
				builder.Append("\n");
				builder.Append("			");
				WriteMethodAction(memberInfo, parameters, builder, methodHasReturn, useReference);
			}
			builder.Append("		}\n");

			builder.Append("\n");
			builder.Append("#if UNITY_EDITOR\n");
			builder.Append("		protected override void Draw() {\n");
			if (useReference) {
				builder.Append("			refObject.DisplayLayout(\"Reference\");\n");
				if (memberInfo.DeclaringType.IsValueType) {
					builder.Append("			newRefObject.DisplayLayout(\"Reference\");\n");
				}
			}

			if (memberInfo.MemberType == MemberTypes.Method) {
				for (int i = 0; i < parameters.Length; i++) {
					if (!parameters[i].IsOut) {
						builder.Append("			" + parameters[i].Name + ".DisplayLayout(\"" + parameters[i].Name + "\");\n");
					}
				}
				for (int i = 0; i < parameters.Length; i++) {
					if (parameters[i].IsOut) {
						builder.Append("			" + parameters[i].Name + ".DisplayLayout(\"" + parameters[i].Name + "\");\n");
					}
				}
				if (methodHasReturn) {
					builder.Append("			returnVar.DisplayLayout(\"Return\");\n");
				}
			} else {
				builder.Append("			UnityEngine.GUILayout.BeginHorizontal();\n");
				if (canWrite) {
					builder.Append("			setter.DisplayLayout(\"set " + memberInfo.Name + "\");\n");
				}
				if (canRead) {
					builder.Append("			getter.DisplayLayout(\"get " + memberInfo.Name + "\");\n");
				}
				builder.Append("			UnityEngine.GUILayout.EndHorizontal();\n");
			}
			
			builder.Append("		}\n");
			builder.Append("#endif\n");
			builder.Append("	}\n");
			builder.Append("}\n");

			return builder.ToString();
		}

		static void WriteMethodAction(MemberInfo memberInfo, ParameterInfo[] parameters, StringBuilder builder, bool methodHasReturn, bool useReference) {
			for (int i = 0; i < parameters.Length; i++) {
				if (parameters[i].IsOut) {
					builder.Append(parameters[i].ParameterType.FullName.Replace("&", "") + " out_" + parameters[i].Name + ";\n");
				}
			}


			if (methodHasReturn) {
				builder.Append("returnVar.SetValue(");
			}
			else if (useReference && memberInfo.DeclaringType.IsValueType) {
				builder.Append(memberInfo.DeclaringType.FullName + " tmp = refObject.GetValue();\n			");
			}
			if (memberInfo.MemberType != MemberTypes.Method || !((MethodInfo) memberInfo).IsSpecialName || !memberInfo.Name.StartsWith("op_")) {
				if (useReference) {
					if (memberInfo.DeclaringType.IsValueType) {
						if (!methodHasReturn) {
							builder.Append("tmp.");
						}
						else {
							builder.Append("refObject.GetValue().");
						}
					}
					else {
						builder.Append("(refObject.GetValue() as " + memberInfo.DeclaringType.FullName + ").");
					}
				} else {
					builder.Append(memberInfo.DeclaringType.FullName + ".");
				}
			}
			if (memberInfo.MemberType == MemberTypes.Method && ((MethodInfo) memberInfo).IsSpecialName) {
				var name = memberInfo.Name;
				if (name.StartsWith("get_")) {
					builder.Append(name.Substring("get_".Length));
				} else if (name.StartsWith("op_")) {
					if (parameters.Length == 2) {
						builder.Append(parameters[0].Name + ".GetValue()");
						builder.Append(OperatorFromName[name]);
						builder.Append(parameters[1].Name + ".GetValue()");
					}
					else if(parameters.Length == 1) {
						builder.Append(OperatorFromName[name]);
						builder.Append(parameters[0].Name + ".GetValue()");
					}
					else {
						throw new Exception("Unexpected number of parameters for operator");
					}
				}
			} else {
				builder.Append(memberInfo.Name + "(");
				for (int i = 0; i < parameters.Length; i++) {
					if (parameters[i].IsOut) {
						builder.Append("out out_" + parameters[i].Name);
					} else {
						builder.Append(parameters[i].Name + ".GetValue()");
					}
					if (i < parameters.Length - 1) builder.Append(", ");
				}
				builder.Append(")");
			}
			
			if (methodHasReturn) {
				builder.Append(")");
			}
			builder.Append(";\n");

			for (int i = 0; i < parameters.Length; i++) {
				if (parameters[i].IsOut) {
					builder.Append(parameters[i].Name + ".SetValue(out_" + parameters[i].Name + ");\n");
				}
			}

			if (useReference && memberInfo.DeclaringType.IsValueType && !methodHasReturn) {
				builder.Append("			newRefObject.SetValue(tmp);\n");
			}
		}

		/// <param name="ioType">Input or Output</param>
		static string CreateIOConstructorParam(Type type, string ioType) {
			return type.IsValueType || type.IsAbstract || type.GetConstructors().Count(c => c.GetParameters().Length == 0) == 0 ?
				"CreateIO<" + ioType + "_" + TemplateIO.GetIOName(type) + ">()" :
				"Create" + ioType + "<" + ioType + "_" + TemplateIO.GetIOName(type) + ", " + type.FullName + ">(new " + type.FullName + "())";
		}

		static Dictionary<string, string> OperatorFromName = new Dictionary<string, string>() {
			{ "op_Equality", "==" },
			{ "op_Inequality", "!=" },
			{ "op_Addition", "+" },
			{ "op_Subtraction", "-" },
			{ "op_Multiply", "*" },
			{ "op_Division", "/" },
			{ "op_LessThan", "<" },
			{ "op_GreaterThan", ">" },
			{ "op_LessThanOrEqual", "<=" },
			{ "op_GreaterThanOrEqual", ">=" },
			{ "op_UnaryNegation", "-" }
		};

		static byte[] GetHash(string inputString) {
			HashAlgorithm algorithm = MD5.Create();  //or use SHA256.Create();
			return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
		}

		static string GetHashString(string inputString) {
			StringBuilder sb = new StringBuilder();
			foreach (byte b in GetHash(inputString))
				sb.Append(b.ToString("X2"));

			return sb.ToString();
		}
	}
}
