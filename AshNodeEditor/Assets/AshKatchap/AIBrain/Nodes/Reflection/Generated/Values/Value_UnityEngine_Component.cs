//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using Ashkatchap.AIBrain.Nodes;
using System;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Values/UnityEngine/Component")]
	public class Value_UnityEngine_Component : ValueBase<UnityEngine.Component> {
		public Input_UnityEngine_Component valueInput;
		public Output_UnityEngine_Component valueOutput;

#if UNITY_EDITOR
		public override void Init() {
			SetName("Component");
			valueOutput = CreateIO<Output_UnityEngine_Component>();
			valueInput = CreateIO<Input_UnityEngine_Component>();
		}
#endif

		public override void Calculate() {
			valueOutput.value = value;
		}

		public override NodeInput GetInput() { return valueInput; }
		public override NodeOutput GetOutput() { return valueOutput; }
	}
}
