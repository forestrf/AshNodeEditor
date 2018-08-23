using Ashkatchap.AIBrain.Nodes;
using System;
using UnityEngine;

namespace Ashkatchap.AIBrain {
	[Serializable]
	[CreateNode("Order/Selector (OR)", "OR. Executes in order until one returns Success. Returns Success if one was successful, Fail otherwise")]
	public class Selector : Node {
		private int nextChild = 0;

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			if (treeOutputs.Length == 0) {
				executionResult = ExecutionResult.Success;
				InterruptExecution();
				return null;
			}
			else {
				if (childResult == ExecutionResult.Success && nextChild > 0) {
					executionResult = ExecutionResult.Success;
					InterruptExecution();
					return null;
				}
				else {
					if (nextChild < treeOutputs.Length) {
						executionResult = ExecutionResult.Running;
						return treeOutputs[nextChild++];
					}
					else {
						executionResult = ExecutionResult.Failure;
						InterruptExecution();
						return null;
					}
				}
			}
		}

		public override void InterruptExecution() {
			nextChild = 0;
		}

		public override void Calculate() { }

#if UNITY_EDITOR
		public override void Init() { }

		protected override void Draw() {
			GUILayout.BeginHorizontal();
			for (int i = 0; i < treeOutputs.Length; i++) {
				GUILayout.BeginVertical();
				if (GUILayout.Button("-")) {
					treeOutputs[i].OnDelete();
				}
				else {
					treeOutputs[i].DisplayLayout((i + 1).ToString());
				}
				GUILayout.EndVertical();
			}
			if (GUILayout.Button("+")) {
				CreateTreeOutput();
			}
			GUILayout.EndHorizontal();
		}
#endif
	}
}
