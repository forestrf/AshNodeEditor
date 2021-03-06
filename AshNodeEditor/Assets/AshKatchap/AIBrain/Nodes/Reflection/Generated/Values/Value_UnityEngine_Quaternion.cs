//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using Ashkatchap.AIBrain.Nodes;
using System;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Values/UnityEngine/Quaternion")]
	public class Value_UnityEngine_Quaternion : ValueBase<UnityEngine.Quaternion> {
		public Input_UnityEngine_Quaternion valueInput;
		public Output_UnityEngine_Quaternion valueOutput;

#if UNITY_EDITOR
		public override void Init() {
			SetName("Quaternion");
			valueOutput = CreateIO<Output_UnityEngine_Quaternion>();
			valueInput = CreateIO<Input_UnityEngine_Quaternion>();
		}
#endif

		public override void Calculate() {
			valueOutput.value = value;
		}

		public override NodeInput GetInput() { return valueInput; }
		public override NodeOutput GetOutput() { return valueOutput; }
	}
}
