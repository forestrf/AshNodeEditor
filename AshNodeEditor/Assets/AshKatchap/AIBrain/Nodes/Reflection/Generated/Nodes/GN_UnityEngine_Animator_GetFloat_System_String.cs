//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Animator/GetFloat (String name) : Single")]
	public class GN_UnityEngine_Animator_GetFloat_System_String : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Animator refObject;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_String name;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Single returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("GetFloat");
			refObject = CreateInput<Input_UnityEngine_Animator, UnityEngine.Animator>(new UnityEngine.Animator());
			name = CreateIO<Input_System_String>();
			returnVar = CreateIO<Output_System_Single>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.SetValue((refObject.GetValue() as UnityEngine.Animator).GetFloat(name.GetValue()));
		}

#if UNITY_EDITOR
		protected override void Draw() {
			refObject.DisplayLayout("Reference");
			name.DisplayLayout("name");
			returnVar.DisplayLayout("Return");
		}
#endif
	}
}
