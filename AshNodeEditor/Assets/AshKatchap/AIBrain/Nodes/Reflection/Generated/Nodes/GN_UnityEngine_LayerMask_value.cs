//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using Ashkatchap.AIBrain.Nodes;
using System;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/LayerMask/value")]
	public class GN_UnityEngine_LayerMask_value : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_LayerMask refObject;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Int32 getter;


#if UNITY_EDITOR
		public override void Init() {
			SetName("value");
			refObject = CreateIO<Input_UnityEngine_LayerMask>();
			getter = CreateIO<Output_System_Int32>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			getter.value = (((UnityEngine.LayerMask) refObject.GetValue()).value);
		}

#if UNITY_EDITOR
		protected override void Draw() {
			refObject.DisplayLayout("Reference");
			UnityEngine.GUILayout.BeginHorizontal();
			getter.DisplayLayout("get value");
			UnityEngine.GUILayout.EndHorizontal();
		}
#endif
	}
}
