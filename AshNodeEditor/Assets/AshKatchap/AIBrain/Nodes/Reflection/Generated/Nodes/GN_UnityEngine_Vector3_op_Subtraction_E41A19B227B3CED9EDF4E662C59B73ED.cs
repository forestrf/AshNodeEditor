//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Vector3/op_Subtraction (Vector3 a, Vector3 b) : Vector3")]
	public class GN_UnityEngine_Vector3_op_Subtraction_E41A19B227B3CED9EDF4E662C59B73ED : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Vector3 a;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Vector3 b;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_UnityEngine_Vector3 returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("-");
			a = CreateIO<Input_UnityEngine_Vector3>();
			b = CreateIO<Input_UnityEngine_Vector3>();
			returnVar = CreateIO<Output_UnityEngine_Vector3>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.SetValue(a.GetValue()-b.GetValue());
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
