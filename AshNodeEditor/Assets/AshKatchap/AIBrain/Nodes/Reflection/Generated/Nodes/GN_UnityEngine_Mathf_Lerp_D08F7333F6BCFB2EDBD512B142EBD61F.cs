//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Mathf/Lerp (Single a, Single b, Single t) : Single")]
	public class GN_UnityEngine_Mathf_Lerp_D08F7333F6BCFB2EDBD512B142EBD61F : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single a;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single b;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single t;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Single returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("Lerp");
			a = CreateIO<Input_System_Single>();
			b = CreateIO<Input_System_Single>();
			t = CreateIO<Input_System_Single>();
			returnVar = CreateIO<Output_System_Single>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.SetValue(UnityEngine.Mathf.Lerp(a.GetValue(), b.GetValue(), t.GetValue()));
		}

#if UNITY_EDITOR
		protected override void Draw() {
			a.DisplayLayout("a");
			b.DisplayLayout("b");
			t.DisplayLayout("t");
			returnVar.DisplayLayout("Return");
		}
#endif
	}
}
