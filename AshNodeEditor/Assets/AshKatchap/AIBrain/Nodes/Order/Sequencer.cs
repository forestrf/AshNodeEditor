using Ashkatchap.AIBrain.Nodes;
using System;
using UnityEngine;

namespace Ashkatchap.AIBrain {
	[Serializable]
	[CreateNode("Order/Sequencer", "AND. Execute the nodes in order and stop when on returns Fail")]
	public class Sequencer : Node {
		private int nextChild = 0;

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			if (nextChild < treeOutputs.Length) {
				if (childResult == ExecutionResult.Success) {
					executionResult = ExecutionResult.Running;
					return treeOutputs[nextChild++];
				}
				else {
					executionResult = ExecutionResult.Failure;
					InterruptExecution();
					return null;
				}
			}
			else {
				executionResult = childResult;
				InterruptExecution();
				return null;
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
