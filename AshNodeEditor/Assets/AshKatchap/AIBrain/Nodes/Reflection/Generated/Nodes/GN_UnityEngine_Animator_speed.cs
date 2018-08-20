//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Animator/speed")]
	public class GN_UnityEngine_Animator_speed : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Animator refObject;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Single getter;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single setter;


#if UNITY_EDITOR
		public override void Init() {
			SetName("speed");
			refObject = CreateInput<Input_UnityEngine_Animator, UnityEngine.Animator>(new UnityEngine.Animator());
			setter = CreateIO<Input_System_Single>();
			getter = CreateIO<Output_System_Single>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			((UnityEngine.Animator) refObject.GetValue()).speed = setter.GetValue();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
				getter.SetValue(((UnityEngine.Animator) refObject.GetValue()).speed);
		}

#if UNITY_EDITOR
		protected override void Draw() {
			refObject.DisplayLayout("Reference");
			UnityEngine.GUILayout.BeginHorizontal();
			setter.DisplayLayout("set speed");
			getter.DisplayLayout("get speed");
			UnityEngine.GUILayout.EndHorizontal();
		}
#endif
	}
}
