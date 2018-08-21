//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Quaternion/Euler (Vector3 euler) : Quaternion")]
	public class GN_UnityEngine_Quaternion_Euler_EDB24573A1EDBE7F04D42229A9A60AF2 : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Vector3 euler;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_UnityEngine_Quaternion returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("Euler");
			euler = CreateIO<Input_UnityEngine_Vector3>();
			returnVar = CreateIO<Output_UnityEngine_Quaternion>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.SetValue(UnityEngine.Quaternion.Euler(euler.GetValue()));
		}

#if UNITY_EDITOR
		protected override void Draw() {
			euler.DisplayLayout("euler");
			returnVar.DisplayLayout("Return");
		}
#endif
	}
}
