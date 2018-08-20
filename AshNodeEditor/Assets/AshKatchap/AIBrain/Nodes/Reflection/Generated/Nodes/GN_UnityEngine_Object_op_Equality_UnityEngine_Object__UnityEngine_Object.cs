//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Object/op_Equality (Object x, Object y) : Boolean")]
	public class GN_UnityEngine_Object_op_Equality_UnityEngine_Object__UnityEngine_Object : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Object x;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Object y;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Boolean returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("==");
			x = CreateInput<Input_UnityEngine_Object, UnityEngine.Object>(new UnityEngine.Object());
			y = CreateInput<Input_UnityEngine_Object, UnityEngine.Object>(new UnityEngine.Object());
			returnVar = CreateIO<Output_System_Boolean>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.SetValue(x.GetValue()==y.GetValue());
		}

#if UNITY_EDITOR
		protected override void Draw() {
			x.DisplayLayout("x");
			y.DisplayLayout("y");
			returnVar.DisplayLayout("Return");
		}
#endif
	}
}
