using Ashkatchap.AIBrain.GeneratedNodes;
using Ashkatchap.AIBrain.Nodes;
using System;
using UnityEngine;

namespace Ashkatchap.AIBrain {
	// HACER ALGO SIMILAR A VARIABLE: Crear los inputs conforme se necesitan según el tipo del objeto puesto
	[Serializable]
	[CreateNode("Math/Or")]
	public class Or : Node {
		public Output_System_Boolean res;
		
		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public override void Calculate() {
			foreach (var input in inputs) {
				if (((Input_System_Boolean) input).GetValue()) {
					res.SetValue(true);
					return;
				}
			}
			res.SetValue(false);
		}

#if UNITY_EDITOR
		public override void Init() {
			res = CreateIO<Output_System_Boolean>();
			SetName("Or");
		}

		protected override void Draw() {
			UnityEditor.EditorGUIUtility.labelWidth = 12;
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();
			for (int i = 0; i < inputs.Length; i++) {
				GUILayout.BeginHorizontal();
				if (GUILayout.Button("-")) {
					inputs[i].OnDelete();
				} else {
					inputs[i].DisplayLayout((i + 1).ToString());
				}
				GUILayout.EndHorizontal();
			}
			if (GUILayout.Button("+")) {
				CreateIO<Input_System_Boolean>();
			}
			GUILayout.EndVertical();
			res.DisplayLayout("Out");
			GUILayout.EndHorizontal();
		}
#endif
	}
}
