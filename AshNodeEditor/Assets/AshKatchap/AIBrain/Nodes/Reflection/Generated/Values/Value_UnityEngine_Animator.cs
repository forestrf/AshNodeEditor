//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using Ashkatchap.AIBrain.Nodes;
using System;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Values/UnityEngine/Animator")]
	public class Value_UnityEngine_Animator : ValueBase<UnityEngine.Animator> {
		public Input_UnityEngine_Animator valueInput;
		public Output_UnityEngine_Animator valueOutput;

#if UNITY_EDITOR
		public override void Init() {
			SetName("Animator");
			valueOutput = CreateIO<Output_UnityEngine_Animator>();
			valueInput = CreateIO<Input_UnityEngine_Animator>();
		}
#endif

		public override void Calculate() {
			valueOutput.value = value;
		}

		public override NodeInput GetInput() { return valueInput; }
		public override NodeOutput GetOutput() { return valueOutput; }
	}
}
