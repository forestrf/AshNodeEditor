//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Animator/StringToHash (String name) : Int32")]
	public class GN_UnityEngine_Animator_StringToHash_System_String : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_String name;

		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Int32 returnVar;


#if UNITY_EDITOR
		public override void Init() {
			SetName("StringToHash");
			name = CreateIO<Input_System_String>();
			returnVar = CreateIO<Output_System_Int32>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			returnVar.SetValue(UnityEngine.Animator.StringToHash(name.GetValue()));
		}

#if UNITY_EDITOR
		protected override void Draw() {
			name.DisplayLayout("name");
			returnVar.DisplayLayout("Return");
		}
#endif
	}
}
