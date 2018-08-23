//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using Ashkatchap.AIBrain.Nodes;
using System;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Time/timeScale")]
	public class GN_UnityEngine_Time_timeScale : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_Single getter;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_Single setter;


#if UNITY_EDITOR
		public override void Init() {
			SetName("timeScale");
			setter = CreateIO<Input_System_Single>();
			getter = CreateIO<Output_System_Single>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			UnityEngine.Time.timeScale = setter.GetValue();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			getter.value = (UnityEngine.Time.timeScale);
		}

#if UNITY_EDITOR
		protected override void Draw() {
			UnityEngine.GUILayout.BeginHorizontal();
			setter.DisplayLayout("set timeScale");
			getter.DisplayLayout("get timeScale");
			UnityEngine.GUILayout.EndHorizontal();
		}
#endif
	}
}
