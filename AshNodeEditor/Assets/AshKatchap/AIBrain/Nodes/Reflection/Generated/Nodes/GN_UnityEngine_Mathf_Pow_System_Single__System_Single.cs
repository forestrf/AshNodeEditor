//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Mathf/Pow (Single f, Single p) : Single")]
	public class GN_UnityEngine_Mathf_Pow_System_Single__System_Single : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single f;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single p;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Single returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("Pow");
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
			returnVar.SetValue(UnityEngine.Mathf.Pow(f.GetValue(), p.GetValue()));
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
