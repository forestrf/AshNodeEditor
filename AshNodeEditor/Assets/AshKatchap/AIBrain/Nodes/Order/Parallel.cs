using Ashkatchap.AIBrain.Nodes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ashkatchap.AIBrain {
	[Serializable]
	[CreateNode("Order/Parallel", "...")]
	public class Parallel : Node {
		private Dictionary<NodeTreeOutput, Context.PC> contexts;
		private bool inProgress;

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			if (null == contexts) {
				contexts = new Dictionary<NodeTreeOutput, Context.PC>();
			}
			if (contexts.Count != treeOutputs.Length) {
				for (int i = 0; i < treeOutputs.Length; i++) {
					if (!contexts.ContainsKey(treeOutputs[i])) {
						contexts[treeOutputs[i]] = new Context.PC();
					}
				}
			}

			if (!inProgress) {
				for (int i = 0; i < treeOutputs.Length; i++) {
					contexts[treeOutputs[i]].Push(treeOutputs[i].outputNode);
				}
				inProgress = true;
			}

			bool anyStackNoZero = false;
			for (int i = 0; i <treeOutputs.Length; i++) {
				var pc = contexts[treeOutputs[i]];
				if (pc.StackHasNodes()) {
					anyStackNoZero |= pc.Tick(this.nodeCanvas);
				}
			}

			if (anyStackNoZero) {
				executionResult = ExecutionResult.StopExecutionAtMe;
			}
			else {
				executionResult = ExecutionResult.Success;
				inProgress = false;
			}
			return null;
		}

		public override void InterruptExecution() {
			foreach (var pc in contexts) {
				if (pc.Value.StackHasNodes()) {
					pc.Value.Clear();
				}
			}
			inProgress = false;
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
				} else {
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
