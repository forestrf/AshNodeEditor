//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Physics/Raycast (Vector3 origin, Vector3 direction, Single maxDistance) : Boolean")]
	public class GN_UnityEngine_Physics_Raycast_UnityEngine_Vector3__UnityEngine_Vector3__System_Single : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Vector3 origin;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Vector3 direction;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single maxDistance;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Boolean returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("Raycast");
			origin = CreateIO<Input_UnityEngine_Vector3>();
			direction = CreateIO<Input_UnityEngine_Vector3>();
			maxDistance = CreateIO<Input_System_Single>();
			returnVar = CreateIO<Output_System_Boolean>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.SetValue(UnityEngine.Physics.Raycast(origin.GetValue(), direction.GetValue(), maxDistance.GetValue()));
		}

#if UNITY_EDITOR
		protected override void Draw() {
			origin.DisplayLayout("origin");
			direction.DisplayLayout("direction");
			maxDistance.DisplayLayout("maxDistance");
			returnVar.DisplayLayout("Return");
		}
#endif
	}
}
