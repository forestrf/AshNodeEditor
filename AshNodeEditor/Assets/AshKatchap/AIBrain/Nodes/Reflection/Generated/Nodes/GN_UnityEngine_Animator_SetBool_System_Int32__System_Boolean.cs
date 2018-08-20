//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Animator/SetBool (Int32 id, Boolean value) : Void")]
	public class GN_UnityEngine_Animator_SetBool_System_Int32__System_Boolean : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Animator refObject;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Int32 id;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Boolean value;


#if UNITY_EDITOR
		public override void Init() {
			SetName("SetBool");
			refObject = CreateInput<Input_UnityEngine_Animator, UnityEngine.Animator>(new UnityEngine.Animator());
			id = CreateIO<Input_System_Int32>();
			value = CreateIO<Input_System_Boolean>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			(refObject.GetValue() as UnityEngine.Animator).SetBool(id.GetValue(), value.GetValue());
		}

#if UNITY_EDITOR
		protected override void Draw() {
			refObject.DisplayLayout("Reference");
			id.DisplayLayout("id");
			value.DisplayLayout("value");
		}
#endif
	}
}
