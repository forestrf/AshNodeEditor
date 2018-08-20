//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Physics/Linecast (Vector3 start, Vector3 end, Int32 layerMask) : Boolean")]
	public class GN_UnityEngine_Physics_Linecast_UnityEngine_Vector3__UnityEngine_Vector3__System_Int32 : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Vector3 start;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Vector3 end;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Int32 layerMask;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Boolean returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("Linecast");
			start = CreateIO<Input_UnityEngine_Vector3>();
			end = CreateIO<Input_UnityEngine_Vector3>();
			layerMask = CreateIO<Input_System_Int32>();
			returnVar = CreateIO<Output_System_Boolean>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.SetValue(UnityEngine.Physics.Linecast(start.GetValue(), end.GetValue(), layerMask.GetValue()));
		}

#if UNITY_EDITOR
		protected override void Draw() {
			start.DisplayLayout("start");
			end.DisplayLayout("end");
			layerMask.DisplayLayout("layerMask");
			returnVar.DisplayLayout("Return");
		}
#endif
	}
}
