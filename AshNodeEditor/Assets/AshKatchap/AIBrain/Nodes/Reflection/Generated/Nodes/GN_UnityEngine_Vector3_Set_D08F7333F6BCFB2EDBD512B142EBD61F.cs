//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Vector3/Set (Single newX, Single newY, Single newZ) : Void")]
	public class GN_UnityEngine_Vector3_Set_D08F7333F6BCFB2EDBD512B142EBD61F : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Vector3 refObject;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_UnityEngine_Vector3 newRefObject;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single newX;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single newY;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single newZ;


#if UNITY_EDITOR
		public override void Init() {
			SetName("Set");
			refObject = CreateIO<Input_UnityEngine_Vector3>();
			newRefObject = CreateIO<Output_UnityEngine_Vector3>();
			newX = CreateIO<Input_System_Single>();
			newY = CreateIO<Input_System_Single>();
			newZ = CreateIO<Input_System_Single>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			UnityEngine.Vector3 tmp = refObject.GetValue();
			tmp.Set(newX.GetValue(), newY.GetValue(), newZ.GetValue());
			newRefObject.SetValue(tmp);
		}

#if UNITY_EDITOR
		protected override void Draw() {
			refObject.DisplayLayout("Reference");
			newRefObject.DisplayLayout("Reference");
			newX.DisplayLayout("newX");
			newY.DisplayLayout("newY");
			newZ.DisplayLayout("newZ");
		}
#endif
	}
}
