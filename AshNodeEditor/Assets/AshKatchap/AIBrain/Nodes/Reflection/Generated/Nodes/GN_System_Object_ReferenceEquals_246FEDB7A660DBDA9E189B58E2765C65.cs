//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using Ashkatchap.AIBrain.Nodes;
using System;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/System/Object/ReferenceEquals (Object objA, Object objB) : Boolean")]
	public class GN_System_Object_ReferenceEquals_246FEDB7A660DBDA9E189B58E2765C65 : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Object objA;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Object objB;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Boolean returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("ReferenceEquals");
			objA = CreateInput<Input_System_Object, System.Object>(new System.Object());
			objB = CreateInput<Input_System_Object, System.Object>(new System.Object());
			returnVar = CreateIO<Output_System_Boolean>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.value = (System.Object.ReferenceEquals(objA.GetValue(), objB.GetValue()));
		}

#if UNITY_EDITOR
		protected override void Draw() {
			objA.DisplayLayout("objA");
			objB.DisplayLayout("objB");
			returnVar.DisplayLayout("Return");
		}
#endif
	}
}
