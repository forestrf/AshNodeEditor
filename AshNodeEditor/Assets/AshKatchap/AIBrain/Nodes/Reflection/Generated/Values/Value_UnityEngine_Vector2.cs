//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using Ashkatchap.AIBrain.Nodes;
using System;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Values/UnityEngine/Vector2")]
	public class Value_UnityEngine_Vector2 : ValueBase<UnityEngine.Vector2> {
		public Input_UnityEngine_Vector2 valueInput;
		public Output_UnityEngine_Vector2 valueOutput;

#if UNITY_EDITOR
		public override void Init() {
			SetName("Vector2");
			valueOutput = CreateIO<Output_UnityEngine_Vector2>();
			valueInput = CreateIO<Input_UnityEngine_Vector2>();
		}
#endif

		public override void Calculate() {
			valueOutput.value = value;
		}

		public override NodeInput GetInput() { return valueInput; }
		public override NodeOutput GetOutput() { return valueOutput; }
	}
}
