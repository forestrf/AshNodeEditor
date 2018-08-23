//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using Ashkatchap.AIBrain.Nodes;
using System;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Transform/right")]
	public class GN_UnityEngine_Transform_right : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Transform refObject;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_UnityEngine_Vector3 getter;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Vector3 setter;


#if UNITY_EDITOR
		public override void Init() {
			SetName("right");
			refObject = CreateIO<Input_UnityEngine_Transform>();
			setter = CreateIO<Input_UnityEngine_Vector3>();
			getter = CreateIO<Output_UnityEngine_Vector3>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			((UnityEngine.Transform) refObject.GetValue()).right = setter.GetValue();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			getter.value = (((UnityEngine.Transform) refObject.GetValue()).right);
		}

#if UNITY_EDITOR
		protected override void Draw() {
			refObject.DisplayLayout("Reference");
			UnityEngine.GUILayout.BeginHorizontal();
			setter.DisplayLayout("set right");
			getter.DisplayLayout("get right");
			UnityEngine.GUILayout.EndHorizontal();
		}
#endif
	}
}
