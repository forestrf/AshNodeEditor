//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Physics/Raycast (Vector3 origin, Vector3 direction, out RaycastHit hitInfo) : Boolean")]
	public class GN_UnityEngine_Physics_Raycast_UnityEngine_Vector3__UnityEngine_Vector3__UnityEngine_RaycastHit_Out_ : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Vector3 origin;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Vector3 direction;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_UnityEngine_RaycastHit hitInfo;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Boolean returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("Raycast");
			origin = CreateIO<Input_UnityEngine_Vector3>();
			direction = CreateIO<Input_UnityEngine_Vector3>();
			hitInfo = CreateIO<Output_UnityEngine_RaycastHit>();
			returnVar = CreateIO<Output_System_Boolean>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			UnityEngine.RaycastHit out_hitInfo;
returnVar.SetValue(UnityEngine.Physics.Raycast(origin.GetValue(), direction.GetValue(), out out_hitInfo));
hitInfo.SetValue(out_hitInfo);
		}

#if UNITY_EDITOR
		protected override void Draw() {
			origin.DisplayLayout("origin");
			direction.DisplayLayout("direction");
			hitInfo.DisplayLayout("hitInfo");
			returnVar.DisplayLayout("Return");
		}
#endif
	}
}
