using Ashkatchap.AIBrain.GeneratedNodes;
using Ashkatchap.AIBrain.Nodes;
using System;
using UnityEngine;

namespace Ashkatchap.AIBrain {
	// HACER ALGO SIMILAR A VARIABLE: Crear los inputs conforme se necesitan según el tipo del objeto puesto
	[Serializable]
	[CreateNode("Math/Not")]
	public class Not : Node {
		public Input_System_Boolean a;
		public Output_System_Boolean res;
		
		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			res.SetValue(!a.GetValue());
		}

#if UNITY_EDITOR
		public override void Init() {
			a = CreateIO<Input_System_Boolean>();
			res = CreateIO<Output_System_Boolean>();
			SetName("not A");
		}

		protected override void Draw() {
			UnityEditor.EditorGUIUtility.labelWidth = 12;
			GUILayout.BeginHorizontal();
			a.DisplayLayout("A");
			res.DisplayLayout("Out");
			GUILayout.EndHorizontal();
		}
#endif
	}
}
