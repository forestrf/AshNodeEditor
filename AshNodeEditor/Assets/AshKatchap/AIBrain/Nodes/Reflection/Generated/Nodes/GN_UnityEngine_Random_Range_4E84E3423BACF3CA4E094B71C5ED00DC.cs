//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Random/Range (Int32 min, Int32 max) : Int32")]
	public class GN_UnityEngine_Random_Range_4E84E3423BACF3CA4E094B71C5ED00DC : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Int32 min;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Int32 max;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Int32 returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("Range");
			min = CreateIO<Input_System_Int32>();
			max = CreateIO<Input_System_Int32>();
			returnVar = CreateIO<Output_System_Int32>();
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
