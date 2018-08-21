//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/System/Object/Equals (Object obj) : Boolean")]
	public class GN_System_Object_Equals_E3750EC35FF403F74E981CBC37A4498D : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Object refObject;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Object obj;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Boolean returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("Equals");
			refObject = CreateInput<Input_System_Object, System.Object>(new System.Object());
			obj = CreateInput<Input_System_Object, System.Object>(new System.Object());
			returnVar = CreateIO<Output_System_Boolean>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.SetValue((refObject.GetValue() as System.Object).Equals(obj.GetValue()));
		}

#if UNITY_EDITOR
		protected override void Draw() {
			refObject.DisplayLayout("Reference");
			obj.DisplayLayout("obj");
			returnVar.DisplayLayout("Return");
		}
#endif
	}
}
