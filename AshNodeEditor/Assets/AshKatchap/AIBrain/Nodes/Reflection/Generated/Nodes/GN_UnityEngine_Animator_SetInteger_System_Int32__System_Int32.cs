//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Animator/SetInteger (Int32 id, Int32 value) : Void")]
	public class GN_UnityEngine_Animator_SetInteger_System_Int32__System_Int32 : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Animator refObject;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Int32 id;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Int32 value;


#if UNITY_EDITOR
		public override void Init() {
			SetName("SetInteger");
			refObject = CreateInput<Input_UnityEngine_Animator, UnityEngine.Animator>(new UnityEngine.Animator());
			id = CreateIO<Input_System_Int32>();
			value = CreateIO<Input_System_Int32>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			(refObject.GetValue() as UnityEngine.Animator).SetInteger(id.GetValue(), value.GetValue());
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
