//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Component/tag")]
	public class GN_UnityEngine_Component_tag : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Component refObject;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_String getter;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_String setter;


#if UNITY_EDITOR
		public override void Init() {
			SetName("tag");
			refObject = CreateInput<Input_UnityEngine_Component, UnityEngine.Component>(new UnityEngine.Component());
			setter = CreateIO<Input_System_String>();
			getter = CreateIO<Output_System_String>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			((UnityEngine.Component) refObject.GetValue()).tag = setter.GetValue();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
				getter.SetValue(((UnityEngine.Component) refObject.GetValue()).tag);
		}

#if UNITY_EDITOR
		protected override void Draw() {
			refObject.DisplayLayout("Reference");
			UnityEngine.GUILayout.BeginHorizontal();
			setter.DisplayLayout("set tag");
			getter.DisplayLayout("get tag");
			UnityEngine.GUILayout.EndHorizontal();
		}
#endif
	}
}
