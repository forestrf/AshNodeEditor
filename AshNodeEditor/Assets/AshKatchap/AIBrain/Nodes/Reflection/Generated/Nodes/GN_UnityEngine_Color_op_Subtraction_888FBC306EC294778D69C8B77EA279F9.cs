//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using Ashkatchap.AIBrain.Nodes;
using System;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Color/op_Subtraction (Color a, Color b) : Color")]
	public class GN_UnityEngine_Color_op_Subtraction_888FBC306EC294778D69C8B77EA279F9 : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Color a;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Color b;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_UnityEngine_Color returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("-");
			a = CreateIO<Input_UnityEngine_Color>();
			b = CreateIO<Input_UnityEngine_Color>();
			returnVar = CreateIO<Output_UnityEngine_Color>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.value = (a.GetValue() - b.GetValue());
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
