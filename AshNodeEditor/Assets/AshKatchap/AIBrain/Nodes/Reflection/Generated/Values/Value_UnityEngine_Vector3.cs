//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using Ashkatchap.AIBrain.Nodes;
using System;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Values/UnityEngine/Vector3")]
	public class Value_UnityEngine_Vector3 : ValueBase<UnityEngine.Vector3> {
		public Input_UnityEngine_Vector3 valueInput;
		public Output_UnityEngine_Vector3 valueOutput;

#if UNITY_EDITOR
		public override void Init() {
			SetName("Vector3");
			valueOutput = CreateIO<Output_UnityEngine_Vector3>();
			valueInput = CreateIO<Input_UnityEngine_Vector3>();
		}
#endif

		public override void Calculate() {
			valueOutput.value = value;
		}

		public override NodeInput GetInput() { return valueInput; }
		public override NodeOutput GetOutput() { return valueOutput; }
	}
}
