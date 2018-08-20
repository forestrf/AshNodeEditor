//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Values/UnityEngine/Color")]
	public class Value_UnityEngine_Color : ValueBase<UnityEngine.Color> {
		public Input_UnityEngine_Color valueInput;
		public Output_UnityEngine_Color valueOutput;

#if UNITY_EDITOR
		public override void Init() {
			SetName("Color");
			valueOutput = CreateIO<Output_UnityEngine_Color>();
			valueInput = CreateIO<Input_UnityEngine_Color>();
		}
#endif

		public override void Calculate() {
			valueOutput.SetValue(value);
		}

		public override NodeInput GetInput() { return valueInput; }
		public override NodeOutput GetOutput() { return valueOutput; }
	}
}
