//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Transform/position")]
	public class GN_UnityEngine_Transform_position : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Transform refObject;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_UnityEngine_Vector3 getter;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Vector3 setter;


#if UNITY_EDITOR
		public override void Init() {
			SetName("position");
			refObject = CreateIO<Input_UnityEngine_Transform>();
			setter = CreateIO<Input_UnityEngine_Vector3>();
			getter = CreateIO<Output_UnityEngine_Vector3>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			((UnityEngine.Transform) refObject.GetValue()).position = setter.GetValue();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
				getter.SetValue(((UnityEngine.Transform) refObject.GetValue()).position);
		}

#if UNITY_EDITOR
		protected override void Draw() {
			refObject.DisplayLayout("Reference");
			UnityEngine.GUILayout.BeginHorizontal();
			setter.DisplayLayout("set position");
			getter.DisplayLayout("get position");
			UnityEngine.GUILayout.EndHorizontal();
		}
#endif
	}
}
