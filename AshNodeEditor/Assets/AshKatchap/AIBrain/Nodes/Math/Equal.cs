using Ashkatchap.AIBrain.GeneratedNodes;
using Ashkatchap.AIBrain.Nodes;
using System;
using UnityEngine;

namespace Ashkatchap.AIBrain {
	// HACER ALGO SIMILAR A VARIABLE: Crear los inputs conforme se necesitan según el tipo del objeto puesto
	[Serializable]
	[CreateNode("Math/Equal")]
	public class Equal : Node {
		public Input_System_Single a;
		public Input_System_Single b;
		public Output_System_Boolean res;

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			res.value = a.GetValue() == b.GetValue();
		}

#if UNITY_EDITOR
		public override void Init() {
			a = CreateIO<Input_System_Single>();
			b = CreateIO<Input_System_Single>();
			res = CreateIO<Output_System_Boolean>();
			SetName("A == B");
		}

		protected override void Draw() {
			UnityEditor.EditorGUIUtility.labelWidth = 12;
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();
			a.DisplayLayout("A");
			b.DisplayLayout("B");
			GUILayout.EndVertical();

			res.DisplayLayout("Out");
			GUILayout.EndHorizontal();
		}
#endif
	}
}
