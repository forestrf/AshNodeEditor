//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using Ashkatchap.AIBrain.Nodes;
using System;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Quaternion/Inverse (Quaternion rotation) : Quaternion")]
	public class GN_UnityEngine_Quaternion_Inverse_3B7C4D0AACE47618C42BDA6993CB1CD8 : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Quaternion rotation;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_UnityEngine_Quaternion returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("Inverse");
			rotation = CreateIO<Input_UnityEngine_Quaternion>();
			returnVar = CreateIO<Output_UnityEngine_Quaternion>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.value = (UnityEngine.Quaternion.Inverse(rotation.GetValue()));
		}

#if UNITY_EDITOR
		protected override void Draw() {
			rotation.DisplayLayout("rotation");
			returnVar.DisplayLayout("Return");
		}
#endif
	}
}
