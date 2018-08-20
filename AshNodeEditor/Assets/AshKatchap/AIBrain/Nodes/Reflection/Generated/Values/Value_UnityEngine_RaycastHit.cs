//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Values/UnityEngine/RaycastHit")]
	public class Value_UnityEngine_RaycastHit : ValueBase<UnityEngine.RaycastHit> {
		public Input_UnityEngine_RaycastHit valueInput;
		public Output_UnityEngine_RaycastHit valueOutput;

#if UNITY_EDITOR
		public override void Init() {
			SetName("RaycastHit");
			valueOutput = CreateIO<Output_UnityEngine_RaycastHit>();
			valueInput = CreateIO<Input_UnityEngine_RaycastHit>();
		}
#endif

		public override void Calculate() {
			valueOutput.SetValue(value);
		}

		public override NodeInput GetInput() { return valueInput; }
		public override NodeOutput GetOutput() { return valueOutput; }
	}
}
