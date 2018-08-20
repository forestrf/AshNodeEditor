//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Values/UnityEngine/AnimationCurve")]
	public class Value_UnityEngine_AnimationCurve : ValueBase<UnityEngine.AnimationCurve> {
		public Input_UnityEngine_AnimationCurve valueInput;
		public Output_UnityEngine_AnimationCurve valueOutput;

#if UNITY_EDITOR
		public override void Init() {
			SetName("AnimationCurve");
			valueOutput = CreateIO<Output_UnityEngine_AnimationCurve>();
			valueInput = CreateIO<Input_UnityEngine_AnimationCurve>();
		}
#endif

		public override void Calculate() {
			valueOutput.SetValue(value);
		}

		public override NodeInput GetInput() { return valueInput; }
		public override NodeOutput GetOutput() { return valueOutput; }
	}
}
