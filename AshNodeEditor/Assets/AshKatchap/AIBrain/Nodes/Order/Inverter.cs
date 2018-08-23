using Ashkatchap.AIBrain.Nodes;
using System;
using UnityEngine;

namespace Ashkatchap.AIBrain {
	[Serializable]
	[CreateNode("Order/Inverter (NOT)")]
	public class Inverter : Node {
		public NodeTreeOutput child;

		private bool waiting = true;

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			if (waiting) {
				waiting = false;
				executionResult = ExecutionResult.Running;
				return child;
			}
			else {
				InterruptExecution();
				executionResult = childResult == ExecutionResult.Success ? ExecutionResult.Failure : ExecutionResult.Success;
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
			child.DisplayLayout("Invert");
			GUILayout.EndHorizontal();
		}
#endif
	}
}
