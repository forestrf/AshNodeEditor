//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Animator/ResetTrigger (String name) : Void")]
	public class GN_UnityEngine_Animator_ResetTrigger_System_String : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Animator refObject;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_String name;


#if UNITY_EDITOR
		public override void Init() {
			SetName("ResetTrigger");
			refObject = CreateInput<Input_UnityEngine_Animator, UnityEngine.Animator>(new UnityEngine.Animator());
			name = CreateIO<Input_System_String>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			(refObject.GetValue() as UnityEngine.Animator).ResetTrigger(name.GetValue());
		}

#if UNITY_EDITOR
		protected override void Draw() {
			refObject.DisplayLayout("Reference");
			name.DisplayLayout("name");
		}
#endif
	}
}
