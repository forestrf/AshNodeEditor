//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Color/a")]
	public class GN_UnityEngine_Color_a : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Color refObject;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Single getter;


#if UNITY_EDITOR
		public override void Init() {
			SetName("a");
			refObject = CreateIO<Input_UnityEngine_Color>();
			getter = CreateIO<Output_System_Single>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
				getter.SetValue(((UnityEngine.Color) refObject.GetValue()).a);
		}

#if UNITY_EDITOR
		protected override void Draw() {
			refObject.DisplayLayout("Reference");
			UnityEngine.GUILayout.BeginHorizontal();
			getter.DisplayLayout("get a");
			UnityEngine.GUILayout.EndHorizontal();
		}
#endif
	}
}
