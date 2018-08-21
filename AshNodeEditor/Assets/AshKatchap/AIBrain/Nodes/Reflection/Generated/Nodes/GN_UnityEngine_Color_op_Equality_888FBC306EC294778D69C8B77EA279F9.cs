//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Color/op_Equality (Color lhs, Color rhs) : Boolean")]
	public class GN_UnityEngine_Color_op_Equality_888FBC306EC294778D69C8B77EA279F9 : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Color lhs;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Color rhs;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Boolean returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("==");
			lhs = CreateIO<Input_UnityEngine_Color>();
			rhs = CreateIO<Input_UnityEngine_Color>();
			returnVar = CreateIO<Output_System_Boolean>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.SetValue(lhs.GetValue()==rhs.GetValue());
		}

#if UNITY_EDITOR
		protected override void Draw() {
			lhs.DisplayLayout("lhs");
			rhs.DisplayLayout("rhs");
			returnVar.DisplayLayout("Return");
		}
#endif
	}
}
