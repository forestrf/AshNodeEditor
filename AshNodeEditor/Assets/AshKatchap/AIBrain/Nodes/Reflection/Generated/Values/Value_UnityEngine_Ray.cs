//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Values/UnityEngine/Ray")]
	public class Value_UnityEngine_Ray : ValueBase<UnityEngine.Ray> {
		public Input_UnityEngine_Ray valueInput;
		public Output_UnityEngine_Ray valueOutput;

#if UNITY_EDITOR
		public override void Init() {
			SetName("Ray");
			valueOutput = CreateIO<Output_UnityEngine_Ray>();
			valueInput = CreateIO<Input_UnityEngine_Ray>();
		}
#endif

		public override void Calculate() {
			valueOutput.SetValue(value);
		}

		public override NodeInput GetInput() { return valueInput; }
		public override NodeOutput GetOutput() { return valueOutput; }
	}
}
