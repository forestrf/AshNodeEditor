//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Mathf/Abs (Single f) : Single")]
	public class GN_UnityEngine_Mathf_Abs_389529E4F9AC198761450D1A29747F79 : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single f;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Single returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("Abs");
			f = CreateIO<Input_System_Single>();
			returnVar = CreateIO<Output_System_Single>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.SetValue(UnityEngine.Mathf.Abs(f.GetValue()));
		}

#if UNITY_EDITOR
		protected override void Draw() {
			f.DisplayLayout("f");
			returnVar.DisplayLayout("Return");
		}
#endif
	}
}
