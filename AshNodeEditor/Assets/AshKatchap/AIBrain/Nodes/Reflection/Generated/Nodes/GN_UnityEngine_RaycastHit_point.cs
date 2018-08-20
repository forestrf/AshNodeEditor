//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/RaycastHit/point")]
	public class GN_UnityEngine_RaycastHit_point : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_RaycastHit refObject;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_UnityEngine_Vector3 getter;


#if UNITY_EDITOR
		public override void Init() {
			SetName("point");
			refObject = CreateIO<Input_UnityEngine_RaycastHit>();
			getter = CreateIO<Output_UnityEngine_Vector3>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
				getter.SetValue(((UnityEngine.RaycastHit) refObject.GetValue()).point);
		}

#if UNITY_EDITOR
		protected override void Draw() {
			refObject.DisplayLayout("Reference");
			UnityEngine.GUILayout.BeginHorizontal();
			getter.DisplayLayout("get point");
			UnityEngine.GUILayout.EndHorizontal();
		}
#endif
	}
}
