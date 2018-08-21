//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Quaternion/op_Equality (Quaternion lhs, Quaternion rhs) : Boolean")]
	public class GN_UnityEngine_Quaternion_op_Equality_029BE80266EBBDE9FA99DC354F0082C7 : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Quaternion lhs;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Quaternion rhs;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Boolean returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("==");
			lhs = CreateIO<Input_UnityEngine_Quaternion>();
			rhs = CreateIO<Input_UnityEngine_Quaternion>();
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
