using Ashkatchap.AIBrain.GeneratedNodes;
using Ashkatchap.AIBrain.Nodes;
using System;
using UnityEngine;

namespace Ashkatchap.AIBrain {
	[Serializable]
	[CreateNode("Order/Repeater")]
	public class Repeater : Node {
		public NodeTreeOutput child;
		public Input_System_Int32 repetitions;

		private int repDone = 0;

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			int reps = repetitions.GetValue();
			if (reps == -1 || repDone < reps) {
				repDone++;
				if (childResult == ExecutionResult.Success) {
					if (repDone == 0) {
						executionResult = ExecutionResult.StopExecutionAtMe;
					} else {
						executionResult = ExecutionResult.Running;
					}
					return child;
				} else {
					executionResult = ExecutionResult.Failure;
					return null;
				}
			} else {
				InterruptExecution();
				executionResult = ExecutionResult.Success;
				return null;
			}
		}

		public override void InterruptExecution() { repDone = 0; }

		public override void Calculate() { }

#if UNITY_EDITOR
		public override void Init() {
			child = CreateTreeOutput();
			repetitions = CreateIO<Input_System_Int32>();
		}

		protected override void Draw() {
			UnityEditor.EditorGUIUtility.labelWidth = 110;
			UnityEditor.EditorGUIUtility.fieldWidth = 30;

			repetitions.DisplayLayout(new GUIContent("Repetitions", "[rep = -1] Repeat until fail.\n[rep > 0] Repeat this number of times unless fail."));

			GUILayout.BeginHorizontal();
			child.DisplayLayout("Repeat");
			GUILayout.EndHorizontal();
		}
#endif
	}
}
