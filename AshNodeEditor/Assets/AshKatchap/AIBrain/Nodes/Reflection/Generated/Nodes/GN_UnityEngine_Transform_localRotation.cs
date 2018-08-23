//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using Ashkatchap.AIBrain.Nodes;
using System;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Transform/localRotation")]
	public class GN_UnityEngine_Transform_localRotation : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Transform refObject;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_UnityEngine_Quaternion getter;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Quaternion setter;


#if UNITY_EDITOR
		public override void Init() {
			SetName("localRotation");
			refObject = CreateIO<Input_UnityEngine_Transform>();
			setter = CreateIO<Input_UnityEngine_Quaternion>();
			getter = CreateIO<Output_UnityEngine_Quaternion>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			((UnityEngine.Transform) refObject.GetValue()).localRotation = setter.GetValue();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			getter.value = (((UnityEngine.Transform) refObject.GetValue()).localRotation);
		}

#if UNITY_EDITOR
		protected override void Draw() {
			refObject.DisplayLayout("Reference");
			UnityEngine.GUILayout.BeginHorizontal();
			setter.DisplayLayout("set localRotation");
			getter.DisplayLayout("get localRotation");
			UnityEngine.GUILayout.EndHorizontal();
		}
#endif
	}
}
