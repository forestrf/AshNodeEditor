//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/AnimationCurve/Evaluate (Single time) : Single")]
	public class GN_UnityEngine_AnimationCurve_Evaluate_System_Single : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_AnimationCurve refObject;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single time;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Single returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("Evaluate");
			refObject = CreateInput<Input_UnityEngine_AnimationCurve, UnityEngine.AnimationCurve>(new UnityEngine.AnimationCurve());
			time = CreateIO<Input_System_Single>();
			returnVar = CreateIO<Output_System_Single>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.SetValue((refObject.GetValue() as UnityEngine.AnimationCurve).Evaluate(time.GetValue()));
		}

#if UNITY_EDITOR
		protected override void Draw() {
			refObject.DisplayLayout("Reference");
			time.DisplayLayout("time");
			returnVar.DisplayLayout("Return");
		}
#endif
	}
}
