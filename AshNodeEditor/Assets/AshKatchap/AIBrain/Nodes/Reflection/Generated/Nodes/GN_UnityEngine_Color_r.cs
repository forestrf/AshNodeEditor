//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using Ashkatchap.AIBrain.Nodes;
using System;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Color/r")]
	public class GN_UnityEngine_Color_r : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Color refObject;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Single getter;


#if UNITY_EDITOR
		public override void Init() {
			SetName("r");
			refObject = CreateIO<Input_UnityEngine_Color>();
			getter = CreateIO<Output_System_Single>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			getter.value = (((UnityEngine.Color) refObject.GetValue()).r);
		}

#if UNITY_EDITOR
		protected override void Draw() {
			refObject.DisplayLayout("Reference");
			UnityEngine.GUILayout.BeginHorizontal();
			getter.DisplayLayout("get r");
			UnityEngine.GUILayout.EndHorizontal();
		}
#endif
	}
}
