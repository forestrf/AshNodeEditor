//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Animator/GetInteger (Int32 id) : Int32")]
	public class GN_UnityEngine_Animator_GetInteger_System_Int32 : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Animator refObject;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Int32 id;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Int32 returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("GetInteger");
			refObject = CreateInput<Input_UnityEngine_Animator, UnityEngine.Animator>(new UnityEngine.Animator());
			id = CreateIO<Input_System_Int32>();
			returnVar = CreateIO<Output_System_Int32>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.SetValue((refObject.GetValue() as UnityEngine.Animator).GetInteger(id.GetValue()));
		}

#if UNITY_EDITOR
		protected override void Draw() {
			refObject.DisplayLayout("Reference");
			id.DisplayLayout("id");
			returnVar.DisplayLayout("Return");
		}
#endif
	}
}
