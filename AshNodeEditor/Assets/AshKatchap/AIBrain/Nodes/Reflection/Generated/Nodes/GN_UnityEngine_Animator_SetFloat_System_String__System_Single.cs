//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Animator/SetFloat (String name, Single value) : Void")]
	public class GN_UnityEngine_Animator_SetFloat_System_String__System_Single : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Animator refObject;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_String name;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single value;


#if UNITY_EDITOR
		public override void Init() {
			SetName("SetFloat");
			refObject = CreateInput<Input_UnityEngine_Animator, UnityEngine.Animator>(new UnityEngine.Animator());
			name = CreateIO<Input_System_String>();
			value = CreateIO<Input_System_Single>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			(refObject.GetValue() as UnityEngine.Animator).SetFloat(name.GetValue(), value.GetValue());
		}

#if UNITY_EDITOR
		protected override void Draw() {
			refObject.DisplayLayout("Reference");
			name.DisplayLayout("name");
			value.DisplayLayout("value");
		}
#endif
	}
}
