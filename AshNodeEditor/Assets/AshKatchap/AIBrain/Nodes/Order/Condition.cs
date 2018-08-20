using Ashkatchap.AIBrain.Nodes;
using System;
using UnityEngine;
using Ashkatchap.AIBrain.GeneratedNodes;

namespace Ashkatchap.AIBrain {
	[Serializable]
	[CreateNode("Order/Condition (IF)")]
	public class Condition : Node {
		public NodeTreeOutput childTrue;
		public NodeTreeOutput childFalse;

		public Input_System_Boolean condition;
		public Input_System_Boolean returnCondition;
		
		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			bool result = condition.GetValue();
			if (returnCondition.GetValue()) executionResult = result ? ExecutionResult.Success : ExecutionResult.Failure;
			else executionResult = ExecutionResult.Success;
			return result ? childTrue : childFalse;
		}

		public override void InterruptExecution() { }

		public override void Calculate() { }

#if UNITY_EDITOR
		public override void Init() {
			condition = CreateIO<Input_System_Boolean>();
			returnCondition = CreateIO<Input_System_Boolean>();
			childTrue = CreateTreeOutput();
			childFalse = CreateTreeOutput();
		}

		protected override void Draw() {
			condition.DisplayLayout("Condition");
			returnCondition.DisplayLayout(new GUIContent("Return", "If the condition is true, return Success\nIf the condition is false, return Failure"));

			GUILayout.BeginHorizontal();
			childTrue.DisplayLayout("<color=green><b>T</b></color>");
			childFalse.DisplayLayout("<color=red><b>F</b></color>");
			GUILayout.EndHorizontal();
		}
#endif
	}
}
