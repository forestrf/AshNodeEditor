//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Values/UnityEngine/Transform")]
	public class Value_UnityEngine_Transform : ValueBase<UnityEngine.Transform> {
		public Input_UnityEngine_Transform valueInput;
		public Output_UnityEngine_Transform valueOutput;

#if UNITY_EDITOR
		public override void Init() {
			SetName("Transform");
			valueOutput = CreateIO<Output_UnityEngine_Transform>();
			valueInput = CreateIO<Input_UnityEngine_Transform>();
		}
#endif

		public override void Calculate() {
			valueOutput.SetValue(value);
		}

		public override NodeInput GetInput() { return valueInput; }
		public override NodeOutput GetOutput() { return valueOutput; }
	}
}
