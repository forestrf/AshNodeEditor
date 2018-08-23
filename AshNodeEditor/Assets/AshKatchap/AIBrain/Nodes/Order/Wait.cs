using Ashkatchap.AIBrain.GeneratedNodes;
using Ashkatchap.AIBrain.Nodes;
using System;
using UnityEngine;

namespace Ashkatchap.AIBrain {
	[Serializable]
	[CreateNode("Order/Wait")]
	public class Wait : Node {
		public NodeTreeOutput child;
		public Input_System_Single seconds;
		public Input_System_Boolean skipMinimumAFrame;

		private float startTime = 0;

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			if (startTime == 0) {
				startTime = Time.time;
			}
#if !UNITY_EDITOR
			float
#endif
			cachedSeconds = seconds.GetValue();
			if (Time.time > startTime + cachedSeconds || (Time.time == startTime && cachedSeconds <= 0 && !skipMinimumAFrame.GetValue())) {
				executionResult = ExecutionResult.Success;
				InterruptExecution();
				return child;
			}
			else {
				executionResult = ExecutionResult.StopExecutionAtMe;
				return null;
			}
		}

		public override void InterruptExecution() {
			startTime = 0;
		}

		public override void Calculate() { }

#if UNITY_EDITOR
		[NonSerialized] float cachedSeconds;

		public override void Init() {
			seconds = CreateInput<Input_System_Single, float>(1f);
			skipMinimumAFrame = CreateInput<Input_System_Boolean, bool>(true);
			child = CreateTreeOutput();
		}

		protected override void Draw() {
			UnityEditor.EditorGUIUtility.labelWidth = 80;
			UnityEditor.EditorGUIUtility.fieldWidth = 20;

			seconds.DisplayLayout("Seconds");
			skipMinimumAFrame.DisplayLayout(new GUIContent("Skip frame", "if as soon as this node is called its wait time is over, this will still skip one frame."));
			UnityEditor.EditorGUI.ProgressBar(GUILayoutUtility.GetRect(130, 10), (Time.time - startTime) / cachedSeconds, "");

			GUILayout.BeginHorizontal();
			child.DisplayLayout("After waiting");
			GUILayout.EndHorizontal();
		}
#endif
	}
}
