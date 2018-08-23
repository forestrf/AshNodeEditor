//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using Ashkatchap.AIBrain.Nodes;
using System;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Values/UnityEngine/Object")]
	public class Value_UnityEngine_Object : ValueBase<UnityEngine.Object> {
		public Input_UnityEngine_Object valueInput;
		public Output_UnityEngine_Object valueOutput;

#if UNITY_EDITOR
		public override void Init() {
			SetName("Object");
			valueOutput = CreateIO<Output_UnityEngine_Object>();
			valueInput = CreateIO<Input_UnityEngine_Object>();
		}
#endif

		public override void Calculate() {
			valueOutput.value = value;
		}

		public override NodeInput GetInput() { return valueInput; }
		public override NodeOutput GetOutput() { return valueOutput; }
	}
}
