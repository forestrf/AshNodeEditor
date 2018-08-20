//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Quaternion/eulerAngles")]
	public class GN_UnityEngine_Quaternion_eulerAngles : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Quaternion refObject;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_UnityEngine_Vector3 getter;


#if UNITY_EDITOR
		public override void Init() {
			SetName("eulerAngles");
			refObject = CreateIO<Input_UnityEngine_Quaternion>();
			getter = CreateIO<Output_UnityEngine_Vector3>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
				getter.SetValue(((UnityEngine.Quaternion) refObject.GetValue()).eulerAngles);
		}

#if UNITY_EDITOR
		protected override void Draw() {
			refObject.DisplayLayout("Reference");
			UnityEngine.GUILayout.BeginHorizontal();
			getter.DisplayLayout("get eulerAngles");
			UnityEngine.GUILayout.EndHorizontal();
		}
#endif
	}
}
