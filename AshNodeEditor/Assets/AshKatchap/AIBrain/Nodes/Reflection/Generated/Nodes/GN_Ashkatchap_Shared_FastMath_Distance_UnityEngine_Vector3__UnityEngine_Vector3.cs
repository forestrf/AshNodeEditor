//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/Ashkatchap/Shared/FastMath/Distance (Vector3 a, Vector3 b) : Single")]
	public class GN_Ashkatchap_Shared_FastMath_Distance_UnityEngine_Vector3__UnityEngine_Vector3 : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Vector3 a;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Vector3 b;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Single returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("Distance");
			a = CreateIO<Input_UnityEngine_Vector3>();
			b = CreateIO<Input_UnityEngine_Vector3>();
			returnVar = CreateIO<Output_System_Single>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.SetValue(Ashkatchap.Shared.FastMath.Distance(a.GetValue(), b.GetValue()));
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
