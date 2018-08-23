//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using Ashkatchap.AIBrain.Nodes;
using System;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/Ashkatchap/Shared/FastMath/Distance (Vector2 a, Vector2 b) : Single")]
	public class GN_Ashkatchap_Shared_FastMath_Distance_A0F79D789476B9E65E33E13A5597086E : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Vector2 a;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Vector2 b;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Single returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("Distance");
			a = CreateIO<Input_UnityEngine_Vector2>();
			b = CreateIO<Input_UnityEngine_Vector2>();
			returnVar = CreateIO<Output_System_Single>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.value = (Ashkatchap.Shared.FastMath.Distance(a.GetValue(), b.GetValue()));
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
