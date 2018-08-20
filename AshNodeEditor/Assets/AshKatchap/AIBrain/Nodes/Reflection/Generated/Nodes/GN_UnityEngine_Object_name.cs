//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Actuator/UnityEngine/Object/name")]
	public class GN_UnityEngine_Object_name : Node {
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_UnityEngine_Object refObject;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Output_System_String getter;
		[HideInNormalInspector] [UnityEngine.SerializeField] public Input_System_String setter;


#if UNITY_EDITOR
		public override void Init() {
			SetName("name");
			refObject = CreateInput<Input_UnityEngine_Object, UnityEngine.Object>(new UnityEngine.Object());
			setter = CreateIO<Input_System_String>();
			getter = CreateIO<Output_System_String>();
		}
#endif

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			((UnityEngine.Object) refObject.GetValue()).name = setter.GetValue();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
				getter.SetValue(((UnityEngine.Object) refObject.GetValue()).name);
		}

#if UNITY_EDITOR
		protected override void Draw() {
			refObject.DisplayLayout("Reference");
			UnityEngine.GUILayout.BeginHorizontal();
			setter.DisplayLayout("set name");
			getter.DisplayLayout("get name");
			UnityEngine.GUILayout.EndHorizontal();
		}
#endif
	}
}
