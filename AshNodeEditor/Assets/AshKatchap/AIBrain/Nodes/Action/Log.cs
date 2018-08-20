using Ashkatchap.AIBrain.GeneratedNodes;
using Ashkatchap.AIBrain.Nodes;
using System;
using UnityEngine;

namespace Ashkatchap.AIBrain {
	[Serializable]
	[CreateNode("Actions/Log")]
	public class Log : Node {
		public Input_System_String message;

		public override void Calculate() { }

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			Debug.Log(message.GetValue());
			executionResult = ExecutionResult.Success;
			return null;
		}

#if UNITY_EDITOR
		public override void Init() {
			message = CreateIO<Input_System_String>();
		}

		protected override void Draw() {
			message.DisplayLayout("Message");
		}
#endif
	}
}
