//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using Ashkatchap.AIBrain.Nodes;
using System;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Values/UnityEngine/Vector4")]
	public class Value_UnityEngine_Vector4 : ValueBase<UnityEngine.Vector4> {
		public Input_UnityEngine_Vector4 valueInput;
		public Output_UnityEngine_Vector4 valueOutput;

#if UNITY_EDITOR
		public override void Init() {
			SetName("Vector4");
			valueOutput = CreateIO<Output_UnityEngine_Vector4>();
			valueInput = CreateIO<Input_UnityEngine_Vector4>();
		}
#endif

		public override void Calculate() {
			valueOutput.value = value;
		}

		public override NodeInput GetInput() { return valueInput; }
		public override NodeOutput GetOutput() { return valueOutput; }
	}
}
