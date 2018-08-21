//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Mathf/Max (Single a, Single b) : Single")]
	public class GN_UnityEngine_Mathf_Max_7C0F941A330AB25EDC6128992A85A777 : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single a;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single b;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Single returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("Max");
			a = CreateIO<Input_System_Single>();
			b = CreateIO<Input_System_Single>();
			returnVar = CreateIO<Output_System_Single>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.SetValue(UnityEngine.Mathf.Max(a.GetValue(), b.GetValue()));
		}

#if UNITY_EDITOR
		protected override void Draw() {
			a.DisplayLayout("a");
			b.DisplayLayout("b");
			returnVar.DisplayLayout("Return");
		}
#endif
	}
}
