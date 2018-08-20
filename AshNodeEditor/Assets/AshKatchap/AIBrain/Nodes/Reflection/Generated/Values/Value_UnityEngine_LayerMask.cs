//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Values/UnityEngine/LayerMask")]
	public class Value_UnityEngine_LayerMask : ValueBase<UnityEngine.LayerMask> {
		public Input_UnityEngine_LayerMask valueInput;
		public Output_UnityEngine_LayerMask valueOutput;

#if UNITY_EDITOR
		public override void Init() {
			SetName("LayerMask");
			valueOutput = CreateIO<Output_UnityEngine_LayerMask>();
			valueInput = CreateIO<Input_UnityEngine_LayerMask>();
		}
#endif

		public override void Calculate() {
			valueOutput.SetValue(value);
		}

		public override NodeInput GetInput() { return valueInput; }
		public override NodeOutput GetOutput() { return valueOutput; }
	}
}
