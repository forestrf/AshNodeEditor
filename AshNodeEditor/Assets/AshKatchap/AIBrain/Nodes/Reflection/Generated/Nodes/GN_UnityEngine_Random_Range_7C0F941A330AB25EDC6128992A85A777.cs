//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Random/Range (Single min, Single max) : Single")]
	public class GN_UnityEngine_Random_Range_7C0F941A330AB25EDC6128992A85A777 : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single min;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single max;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Single returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("Range");
			min = CreateIO<Input_System_Single>();
			max = CreateIO<Input_System_Single>();
			returnVar = CreateIO<Output_System_Single>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.SetValue(UnityEngine.Random.Range(min.GetValue(), max.GetValue()));
		}

#if UNITY_EDITOR
		protected override void Draw() {
			min.DisplayLayout("min");
			max.DisplayLayout("max");
			returnVar.DisplayLayout("Return");
		}
#endif
	}
}
