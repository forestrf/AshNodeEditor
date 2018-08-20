using Ashkatchap.AIBrain.Nodes;
using System;
using UnityEngine;

namespace Ashkatchap.AIBrain {
	[Serializable]
	[CreateNode("Order/Succeeder (TRUE)")]
	public class Succeeder : Node {
		public NodeTreeOutput child;

		private bool waiting = true;

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			if (waiting) {
				waiting = false;
				executionResult = ExecutionResult.Running;
				return child;
			} else {
				InterruptExecution();
				executionResult = ExecutionResult.Success;
				return null;
			}
		}

		public override void InterruptExecution() { waiting = true; }

		public override void Calculate() { }

#if UNITY_EDITOR
		public override void Init() {
			child = CreateTreeOutput();
		}

		protected override void Draw() {
			GUILayout.BeginHorizontal();
			child.DisplayLayout("Success");
			GUILayout.EndHorizontal();
		}
#endif
	}
}
