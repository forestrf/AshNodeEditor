//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Vector3/z")]
	public class GN_UnityEngine_Vector3_z : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Vector3 refObject;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Single getter;


#if UNITY_EDITOR
		public override void Init() {
			SetName("z");
			refObject = CreateIO<Input_UnityEngine_Vector3>();
			getter = CreateIO<Output_System_Single>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
				getter.SetValue(((UnityEngine.Vector3) refObject.GetValue()).z);
		}

#if UNITY_EDITOR
		protected override void Draw() {
			refObject.DisplayLayout("Reference");
			UnityEngine.GUILayout.BeginHorizontal();
			getter.DisplayLayout("get z");
			UnityEngine.GUILayout.EndHorizontal();
		}
#endif
	}
}
