//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Ray/direction")]
	public class GN_UnityEngine_Ray_direction : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Ray refObject;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_UnityEngine_Vector3 getter;


#if UNITY_EDITOR
		public override void Init() {
			SetName("direction");
			refObject = CreateIO<Input_UnityEngine_Ray>();
			getter = CreateIO<Output_UnityEngine_Vector3>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
				getter.SetValue(((UnityEngine.Ray) refObject.GetValue()).direction);
		}

#if UNITY_EDITOR
		protected override void Draw() {
			refObject.DisplayLayout("Reference");
			UnityEngine.GUILayout.BeginHorizontal();
			getter.DisplayLayout("get direction");
			UnityEngine.GUILayout.EndHorizontal();
		}
#endif
	}
}
