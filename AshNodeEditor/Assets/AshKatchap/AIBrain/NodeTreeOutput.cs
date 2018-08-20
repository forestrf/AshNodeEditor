using Ashkatchap.Shared;
using System;
using UnityEngine;

namespace Ashkatchap.AIBrain.Nodes {
	[Serializable]
	public class NodeTreeOutput : MonoBehaviour {
		[HideInNormalInspector] [SerializeField] private Node body;
		public Node outputNode;

		public void OnDelete() {
#if UNITY_EDITOR
			Set(null);
			body.RemoveTreeOutput(this);
#endif
			UndoWrapper.DestroyObject(this);
		}

#if UNITY_EDITOR
		public void Configure(Node body) {
			this.body = body;
			body.AddTreeOutput(this);
		}

		public void Set(Node newOutputNode) {
			if (outputNode != null) outputNode.SetValue("timesLinkedAsTree", outputNode.timesLinkedAsTree - 1);
			if (newOutputNode != null) newOutputNode.SetValue("timesLinkedAsTree", newOutputNode.timesLinkedAsTree + 1);
			this.SetValue("outputNode", newOutputNode);
		}

		[NonSerialized] public float lastExecutedTime = -9999;

		[NonSerialized] public Rect rect = new Rect();
		public static Color GetColor() {
			return Color.red;
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
			style.alignment = TextAnchor.MiddleCenter;
			GUILayout.Label(content, style);
			TrySetLastRect();
		}

		/// <summary>
		/// Call SetRect if repainting with GUILayoutUtility.GetLastRect()
		/// </summary>
		public void TrySetLastRect() {
			if (Event.current.type == EventType.Repaint)
				SetRect(GUILayoutUtility.GetLastRect());
		}

		/// <summary>
		/// Set the output rect as labelrect in global canvas space and extend it to the right node edge
		/// </summary>
		public void SetRect(Rect labelRect) {
			if (Event.current.type == EventType.Repaint)
				rect = new Rect(body.canvasPosition.x + labelRect.x,
							 body.canvasPosition.y + labelRect.y,
							 labelRect.width,
							 labelRect.height);
		}

		/// <summary>
		/// Get the rect of the knob left to the input
		/// </summary>
		public Rect GetKnobRect(GUI_Info info) {
			int knobSize = info.knobSize;
			return new Rect(rect.x + (rect.width - knobSize) / 2,
							 body.rectPixelCorrected.y + body.positionSize.height,
							 knobSize, knobSize);
		}
		
		public Vector2 GetGUIPosForOrderGraph(Vector2 size, GUI_Info info) {
			var r = GetKnobRect(info);
			r.x += info.knobSize;
			r.y += (r.height - size.y) * 0.5f;
			return r.position;
		}
		
		public void DrawKnob(GUI_Info info) {
			GUI.color = GetColor();
			GUI.DrawTexture(GetKnobRect(info), GUI_Info.TreeKnob);
		}

		public Node GetBody() {
			return body;
		}
#endif
	}
}
