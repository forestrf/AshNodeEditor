//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using Ashkatchap.AIBrain.Nodes;
using System;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/RaycastHit/normal")]
	public class GN_UnityEngine_RaycastHit_normal : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_RaycastHit refObject;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_UnityEngine_Vector3 getter;


#if UNITY_EDITOR
		public override void Init() {
			SetName("normal");
			refObject = CreateIO<Input_UnityEngine_RaycastHit>();
			getter = CreateIO<Output_UnityEngine_Vector3>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			getter.value = (((UnityEngine.RaycastHit) refObject.GetValue()).normal);
		}

#if UNITY_EDITOR
		protected override void Draw() {
			refObject.DisplayLayout("Reference");
			UnityEngine.GUILayout.BeginHorizontal();
			getter.DisplayLayout("get normal");
			UnityEngine.GUILayout.EndHorizontal();
		}
#endif
	}
}
