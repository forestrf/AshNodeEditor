//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Random/get_insideUnitCircle () : Vector2")]
	public class GN_UnityEngine_Random_get_insideUnitCircle_ : Node {

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_UnityEngine_Vector2 returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("insideUnitCircle");
			returnVar = CreateIO<Output_UnityEngine_Vector2>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.SetValue(UnityEngine.Random.insideUnitCircle);
		}

#if UNITY_EDITOR
		protected override void Draw() {
			returnVar.DisplayLayout("Return");
		}
#endif
	}
}
