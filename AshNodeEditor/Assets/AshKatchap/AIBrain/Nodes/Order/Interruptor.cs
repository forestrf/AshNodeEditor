using Ashkatchap.AIBrain.Nodes;
using System;
using UnityEngine;

namespace Ashkatchap.AIBrain {
	[Serializable]
	[CreateNode("Order/Interruptor")]
	public class Interruptor : Node {
		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			for (int i = 0; i < treeOutputs.Length; i++) {
				if (null != treeOutputs[i].outputNode) {
					nodeCanvas.TryInterruptNode(treeOutputs[i].outputNode);
				}
			}

			executionResult = ExecutionResult.Success;
			return null;
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
