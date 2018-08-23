//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using Ashkatchap.AIBrain.Nodes;
using System;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Mathf/Log (Single f, Single p) : Single")]
	public class GN_UnityEngine_Mathf_Log_7C0F941A330AB25EDC6128992A85A777 : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single f;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single p;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Single returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("Log");
			f = CreateIO<Input_System_Single>();
			p = CreateIO<Input_System_Single>();
			returnVar = CreateIO<Output_System_Single>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.value = (UnityEngine.Mathf.Log(f.GetValue(), p.GetValue()));
		}

#if UNITY_EDITOR
		protected override void Draw() {
			f.DisplayLayout("f");
			p.DisplayLayout("p");
			returnVar.DisplayLayout("Return");
		}
#endif
	}
}
