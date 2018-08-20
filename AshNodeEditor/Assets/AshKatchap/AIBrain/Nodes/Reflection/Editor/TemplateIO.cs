using System;
using System.Text;

namespace Ashkatchap.AIBrain.GeneratedNodes {

	public static class TemplateIO {
		public static string GetIOName(Type type, char separator = '_') {
			return type.FullName.Replace('.', separator).Replace("&", "");
		}

		public static string GenerateFileInput(Type type, out string filename) {
			return GenerateFileOutput(type, out filename, "Input");
		}

		public static string GenerateFileOutput(Type type, out string filename) {
			return GenerateFileOutput(type, out filename, "Output");
		}

		private static string GenerateFileOutput(Type type, out string filename, string IOType) {
			StringBuilder builder = new StringBuilder();

			filename = IOType + "_" + GetIOName(type);

			builder.Append("//////////////////////////////////////\n");
			builder.Append("//// FILE GENERATED AUTOMATICALLY ////\n");
			builder.Append("//////////////////////////////////////\n");
			builder.Append("\n");
			builder.Append("using System;\n");
			builder.Append("using Ashkatchap.AIBrain.Nodes;\n");
			builder.Append("\n");
			builder.Append("namespace Ashkatchap.AIBrain.GeneratedNodes {\n");
			builder.Append("	[Serializable]\n");
			builder.Append("	public class " + filename + " : Node" + IOType + "<" + type.FullName + "> { }\n");
			builder.Append("}\n");

			filename += ".cs";

			return builder.ToString();
		}



		public static string GenerateValueFile(Type type, out string filename) {
			StringBuilder builder = new StringBuilder();

			filename = "Value_" + GetIOName(type);

			builder.Append("//////////////////////////////////////\n");
			builder.Append("//// FILE GENERATED AUTOMATICALLY ////\n");
			builder.Append("//////////////////////////////////////\n");
			builder.Append("\n");
			builder.Append("using System;\n");
			builder.Append("using Ashkatchap.AIBrain.Nodes;\n");
			builder.Append("\n");
			builder.Append("namespace Ashkatchap.AIBrain.GeneratedNodes {\n");
			builder.Append("	[Serializable]\n");
			builder.Append("	[CreateNode(\"Values/" + type.FullName.Replace('.', '/') + "\")]\n");
			builder.Append("	public class " + filename + " : ValueBase<" + type.FullName + "> {\n");
			builder.Append("		public Input_" + GetIOName(type) + " valueInput;\n");
			builder.Append("		public Output_" + GetIOName(type) + " valueOutput;\n");
			builder.Append("\n");
			builder.Append("#if UNITY_EDITOR\n");
			builder.Append("		public override void Init() {\n");
			builder.Append("			SetName(\"" + type.Name + "\");\n");
			builder.Append("			valueOutput = CreateIO<Output_"+ GetIOName(type) + ">();\n");
			builder.Append("			valueInput = CreateIO<Input_"+ GetIOName(type) + ">();\n");
			builder.Append("		}\n");
			builder.Append("#endif\n");
			builder.Append("\n");
			builder.Append("		public override void Calculate() {\n");
			builder.Append("			valueOutput.SetValue(value);\n");
			builder.Append("		}\n");
			builder.Append("\n");
			builder.Append("		public override NodeInput GetInput() { return valueInput; }\n");
			builder.Append("		public override NodeOutput GetOutput() { return valueOutput; }\n");
			builder.Append("	}\n");
			builder.Append("}\n");

			filename += ".cs";

			return builder.ToString();
		}
	}
}
