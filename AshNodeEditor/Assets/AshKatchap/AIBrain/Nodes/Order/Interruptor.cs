using Ashkatchap.AIBrain.Nodes;
using Ashkatchap.Shared;
using System;
using UnityEngine;

namespace Ashkatchap.AIBrain {
	[Serializable]
	[CreateNode("Order/Interruptor")]
	public class Interruptor : Node {
		public NodeTreeOutput child;
		public Group[] groups;
		public Node[] nodes;

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			for (int i = 0; i < nodes.Length; i++) {
				nodeCanvas.TryInterruptNode(nodes[i]);
			}
			for (int j = 0; j < groups.Length; j++) {
				var nodesGroup = (Node[]) groups[j].GetNodes();
				for (int i = 0; i < nodesGroup.Length; i++) {
					nodeCanvas.TryInterruptNode(nodesGroup[i]);
				}
			}

			executionResult = ExecutionResult.Success;
			return child;
		}

		public override void Calculate() { }

#if UNITY_EDITOR
		public override void Init() {
			child = CreateTreeOutput();
		}

		protected override void Draw() {
			GUILayout.BeginHorizontal();
			child.DisplayLayout("After Interrupting");
			GUILayout.EndHorizontal();
		}

		protected override void DrawInternalInspector() {
			this.LayoutPropertyField("groups");
			foreach (var g in groups) {
				if (g != null) GUILayout.Label(g.GetName());
			}
			this.LayoutPropertyField("nodes");
			foreach (var n in nodes) {
				if (n != null) GUILayout.Label(n.GetName());
			}
		}

		public override void OnSelectedDrawCanvas(GUI_Info info) {
			foreach (var g in groups) {
				if (g != null) UnityEditor.Handles.DrawLine(canvasPositionPixelCorrected * info.zoom, g.CanvasPositionPixelCorrected * info.zoom);
			}
			foreach (var n in nodes) {
				if (n != null) UnityEditor.Handles.DrawLine(canvasPositionPixelCorrected * info.zoom, n.canvasPositionPixelCorrected * info.zoom);
			}
		}
#endif
	}
}
