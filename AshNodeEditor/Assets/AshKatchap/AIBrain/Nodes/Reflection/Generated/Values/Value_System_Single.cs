//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using Ashkatchap.AIBrain.Nodes;
using System;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Values/System/Single")]
	public class Value_System_Single : ValueBase<System.Single> {
		public Input_System_Single valueInput;
		public Output_System_Single valueOutput;

#if UNITY_EDITOR
		public override void Init() {
			SetName("Single");
			valueOutput = CreateIO<Output_System_Single>();
			valueInput = CreateIO<Input_System_Single>();
		}
#endif

		public override void Calculate() {
			valueOutput.value = value;
		}

		public override NodeInput GetInput() { return valueInput; }
		public override NodeOutput GetOutput() { return valueOutput; }
	}
}
