//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/LayerMask/NameToLayer (String layerName) : Int32")]
	public class GN_UnityEngine_LayerMask_NameToLayer_2CC1E47139B92B2D50469EC279A36107 : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_String layerName;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Int32 returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("NameToLayer");
			layerName = CreateIO<Input_System_String>();
			returnVar = CreateIO<Output_System_Int32>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.SetValue(UnityEngine.LayerMask.NameToLayer(layerName.GetValue()));
		}

#if UNITY_EDITOR
		protected override void Draw() {
			layerName.DisplayLayout("layerName");
			returnVar.DisplayLayout("Return");
		}
#endif
	}
}
