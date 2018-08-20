//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Vector3/op_Multiply (Vector3 a, Single d) : Vector3")]
	public class GN_UnityEngine_Vector3_op_Multiply_UnityEngine_Vector3__System_Single : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Vector3 a;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single d;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_UnityEngine_Vector3 returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("*");
			a = CreateIO<Input_UnityEngine_Vector3>();
			d = CreateIO<Input_System_Single>();
			returnVar = CreateIO<Output_UnityEngine_Vector3>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.SetValue(a.GetValue()*d.GetValue());
		}

#if UNITY_EDITOR
		protected override void Draw() {
			a.DisplayLayout("a");
			d.DisplayLayout("d");
			returnVar.DisplayLayout("Return");
		}
#endif
	}
}
