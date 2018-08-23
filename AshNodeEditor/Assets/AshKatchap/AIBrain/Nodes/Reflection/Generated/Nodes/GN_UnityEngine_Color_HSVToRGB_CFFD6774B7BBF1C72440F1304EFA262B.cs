//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using Ashkatchap.AIBrain.Nodes;
using System;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Color/HSVToRGB (Single H, Single S, Single V, Boolean hdr) : Color")]
	public class GN_UnityEngine_Color_HSVToRGB_CFFD6774B7BBF1C72440F1304EFA262B : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single H;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single S;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single V;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Boolean hdr;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_UnityEngine_Color returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("HSVToRGB");
			H = CreateIO<Input_System_Single>();
			S = CreateIO<Input_System_Single>();
			V = CreateIO<Input_System_Single>();
			hdr = CreateIO<Input_System_Boolean>();
			returnVar = CreateIO<Output_UnityEngine_Color>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.value = (UnityEngine.Color.HSVToRGB(H.GetValue(), S.GetValue(), V.GetValue(), hdr.GetValue()));
		}

#if UNITY_EDITOR
		protected override void Draw() {
			H.DisplayLayout("H");
			S.DisplayLayout("S");
			V.DisplayLayout("V");
			hdr.DisplayLayout("hdr");
			returnVar.DisplayLayout("Return");
		}
#endif
	}
}
