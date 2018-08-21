//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Vector3/op_Equality (Vector3 lhs, Vector3 rhs) : Boolean")]
	public class GN_UnityEngine_Vector3_op_Equality_E41A19B227B3CED9EDF4E662C59B73ED : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Vector3 lhs;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Vector3 rhs;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Boolean returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("==");
			lhs = CreateIO<Input_UnityEngine_Vector3>();
			rhs = CreateIO<Input_UnityEngine_Vector3>();
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
