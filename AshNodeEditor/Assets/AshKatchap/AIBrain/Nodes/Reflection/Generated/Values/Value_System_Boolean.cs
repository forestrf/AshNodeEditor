//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Values/System/Boolean")]
	public class Value_System_Boolean : ValueBase<System.Boolean> {
		public Input_System_Boolean valueInput;
		public Output_System_Boolean valueOutput;

#if UNITY_EDITOR
		public override void Init() {
			SetName("Boolean");
			valueOutput = CreateIO<Output_System_Boolean>();
			valueInput = CreateIO<Input_System_Boolean>();
		}
#endif

		public override void Calculate() {
			valueOutput.SetValue(value);
		}

		public override NodeInput GetInput() { return valueInput; }
		public override NodeOutput GetOutput() { return valueOutput; }
	}
}
