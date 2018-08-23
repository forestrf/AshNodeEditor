//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using Ashkatchap.AIBrain.Nodes;
using System;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Quaternion/op_Multiply (Quaternion rotation, Vector3 point) : Vector3")]
	public class GN_UnityEngine_Quaternion_op_Multiply_A2607B0A0DD3F611836C9E58203CE036 : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Quaternion rotation;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Vector3 point;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_UnityEngine_Vector3 returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("*");
			rotation = CreateIO<Input_UnityEngine_Quaternion>();
			point = CreateIO<Input_UnityEngine_Vector3>();
			returnVar = CreateIO<Output_UnityEngine_Vector3>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.value = (rotation.GetValue() * point.GetValue());
		}

#if UNITY_EDITOR
		protected override void Draw() {
			rotation.DisplayLayout("rotation");
			point.DisplayLayout("point");
			returnVar.DisplayLayout("Return");
		}
#endif
	}
}
