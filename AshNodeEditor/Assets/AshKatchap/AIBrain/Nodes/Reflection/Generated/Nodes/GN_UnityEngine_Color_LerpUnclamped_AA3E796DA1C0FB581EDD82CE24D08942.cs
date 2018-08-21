//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Color/LerpUnclamped (Color a, Color b, Single t) : Color")]
	public class GN_UnityEngine_Color_LerpUnclamped_AA3E796DA1C0FB581EDD82CE24D08942 : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Color a;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Color b;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single t;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_UnityEngine_Color returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("LerpUnclamped");
			a = CreateIO<Input_UnityEngine_Color>();
			b = CreateIO<Input_UnityEngine_Color>();
			t = CreateIO<Input_System_Single>();
			returnVar = CreateIO<Output_UnityEngine_Color>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.SetValue(UnityEngine.Color.LerpUnclamped(a.GetValue(), b.GetValue(), t.GetValue()));
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
