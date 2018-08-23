//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using Ashkatchap.AIBrain.Nodes;
using System;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Quaternion/Euler (Single x, Single y, Single z) : Quaternion")]
	public class GN_UnityEngine_Quaternion_Euler_D08F7333F6BCFB2EDBD512B142EBD61F : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single x;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single y;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single z;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_UnityEngine_Quaternion returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("Euler");
			x = CreateIO<Input_System_Single>();
			y = CreateIO<Input_System_Single>();
			z = CreateIO<Input_System_Single>();
			returnVar = CreateIO<Output_UnityEngine_Quaternion>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.value = (UnityEngine.Quaternion.Euler(x.GetValue(), y.GetValue(), z.GetValue()));
		}

#if UNITY_EDITOR
		protected override void Draw() {
			x.DisplayLayout("x");
			y.DisplayLayout("y");
			z.DisplayLayout("z");
			returnVar.DisplayLayout("Return");
		}
#endif
	}
}
