using Ashkatchap.Shared;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ashkatchap.AIBrain.Nodes {
	[Serializable]
	public abstract class NodeOutput : NodeIO
#if UNITY_EDITOR
		, INodeCanvasGUIGraphForOrder
#endif
		{
		[HideInNormalInspector] [SerializeField] private NodeInput[] nodeInputs = new NodeInput[0];
		
		public IEnumerable<NodeInput> GetInputs() {
			return nodeInputs;
		}

		public abstract void SetFromInput(NodeInput input);

#if UNITY_EDITOR
		public override void SetDefaultValue(object defaultValue) { }

		public override void Configure(Node nodeBody, Type connectionType) {
			base.Configure(nodeBody, connectionType);
			body.AddOutput(this);
		}

		public override void OnDelete() {
			body.RemoveOutput(this);
			while (nodeInputs.Length > 0)
				nodeInputs[0].RemoveNodeOutput(this);
			UndoWrapper.DestroyObject(this);
		}

		public void RemoveNodeInput(NodeInput input, bool completely = true) {
			this.Remove("nodeInputs", input);
			if (completely) input.RemoveNodeOutput(this, false);
		}
		public void AddNodeInput(NodeInput input, bool completely = true) {
			this.Add("nodeInputs", input);
			if (completely) input.AddNodeOutput(this, false);
		}

		/// <summary>
		/// Function to automatically draw and update the output with a label for it's name
		/// </summary>
		public void DisplayLayout(string name) {
			DisplayLayout(new GUIContent(name));
		}
		/// <summary>
		/// Function to automatically draw and update the output with a label for it's name
		/// </summary>
		public void DisplayLayout(GUIContent content) {
			GUIStyle style = new GUIStyle(UnityEditor.EditorStyles.label);
			style.alignment = TextAnchor.MiddleRight;
			GUILayout.Label(content, style);
			TrySetLastRect();
		}

		/// <summary>
		/// Call SetRect if repainting with GUILayoutUtility.GetLastRect()
		/// </summary>
		public override void TrySetLastRect() {
			if (Event.current.type == EventType.Repaint)
				SetRect(GUILayoutUtility.GetLastRect());
		}

		/// <summary>
		/// Set the output rect as labelrect in global canvas space and extend it to the right node edge
		/// </summary>
		public override void SetRect(Rect labelRect) {
			if (Event.current.type == EventType.Repaint)
				rect = new Rect(body.canvasPosition.x + labelRect.x,
							 body.canvasPosition.y + labelRect.y,
							 body.positionSize.width - labelRect.x,
							 labelRect.height);
		}

		/// <summary>
		/// Get the rect of the knob left to the input
		/// </summary>
		public override Rect GetKnobRect(GUI_Info info) {
			int knobSize = info.knobSize;
			return new Rect(rect.x + rect.width,
							 rect.y + (rect.height - knobSize) / 2,
							 knobSize, knobSize);
		}

		public override void DrawKnob(GUI_Info info) {
			GUI.DrawTexture(GetKnobRect(info), GUI_Info.ConnectorKnob);
		}

		public override Vector2 GetGUIPosForOrderGraph(Vector2 size, GUI_Info info) {
			var r = GetKnobRect(info);
			r.x += info.knobSize;
			r.y += (r.height - size.y) * 0.5f;
			return r.position;
		}
#endif
	}
}
