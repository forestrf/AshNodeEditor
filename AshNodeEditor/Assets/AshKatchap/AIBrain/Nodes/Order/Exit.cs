using Ashkatchap.AIBrain.Nodes;
using System;

namespace Ashkatchap.AIBrain {
	[Serializable]
	[CreateNode("Order/Exit")]
	public class Exit : Node {
		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			executionResult = ExecutionResult.Exit;
			return null;
		}

		public override void Calculate() { }

#if UNITY_EDITOR
		public override void Init() { }
		protected override void Draw() { }
#endif
	}
}
