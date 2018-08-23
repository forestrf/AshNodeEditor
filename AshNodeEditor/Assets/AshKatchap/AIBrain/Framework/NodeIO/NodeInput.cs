using Ashkatchap.Shared;
using System;
using UnityEngine;

namespace Ashkatchap.AIBrain.Nodes {
	[Serializable]
	public abstract class NodeInput : NodeIO
#if UNITY_EDITOR
		, INodeCanvasGUIGraphForOrder
#endif
		{
		public NodeOutput nodeOutput { get { return _nodeOutput; } }
		[HideInNormalInspector] [SerializeField] protected NodeOutput _nodeOutput;

		public bool HasOutput() {
			return _nodeOutput != null;
		}

		public abstract void SetDefaultValueFromInput(NodeInput input);

#if UNITY_EDITOR
		[NonSerialized] public GUIContent guiContent = GUIContent.none;

		public abstract bool ContainsNodeOutput(NodeOutput nodeOutput);
		public abstract void AddNodeOutput(NodeOutput nodeOutput, bool completely = true);
		public abstract void RemoveNodeOutput(NodeOutput nodeOutput, bool completely = true);
		public abstract bool CanConnectWith(NodeOutput nodeOutput);

		public override void Configure(Node nodeBody, Type connectionType) {
			base.Configure(nodeBody, connectionType);
			body.AddInput(this);
		}

		public void SetOutput(NodeOutput nodeOutput) {
			this.SetSerializedValue("_nodeOutput", nodeOutput);
		}

		public override void OnDelete() {
			body.RemoveInput(this);
			if (nodeOutput != null) RemoveNodeOutput(nodeOutput);
			UndoWrapper.DestroyObject(this);
		}

		/// <summary>
		/// Function to automatically draw and update the input with a label for it's name
		/// </summary>
		public virtual void DisplayLayout(string name) {
			DisplayLayout(new GUIContent(name));
		}
		public void DisplayLayout(GUIContent content, bool setKnobRect = true) {
			guiContent = content;
			DisplayLayout(setKnobRect);
		}
		public void DisplayLayout(bool setKnobRect = true) {
			if (body.isVisible) {
				if (nodeOutput != null) {
					GUILayout.Label(guiContent, UnityEditor.EditorStyles.label);
				}
				else {
					this.LayoutPropertyField("defaultValue", guiContent);
				}
			}
			else {
				GUILayout.Label(guiContent, UnityEditor.EditorStyles.label);
			}
			if (setKnobRect) TrySetLastRect();
		}

		/// <summary>
		/// Call SetRect if repainting with GUILayoutUtility.GetLastRect()
		/// </summary>
		public override void TrySetLastRect() {
			if (Event.current.type == EventType.Repaint)
				SetRect(GUILayoutUtility.GetLastRect());
		}
		/// <summary>
		/// Set the input rect as labelrect in global canvas space and extend it to the left node edge
		/// </summary>
		public override void SetRect(Rect labelRect) {
			if (Event.current.type == EventType.Repaint)
				rect = new Rect(body.rect.x,
							 body.rect.y + labelRect.y,
							 labelRect.width + labelRect.x,
							 labelRect.height);
		}

		/// <summary>
		/// Get the rect of the knob left to the input
		/// </summary>
		public override Rect GetKnobRect(GUI_Info info) {
			int knobSize = info.knobSize;
			return new Rect(rect.x - knobSize,
							 rect.y + (rect.height - knobSize) / 2,
							 knobSize, knobSize);
		}

		public override void DrawKnob(GUI_Info info) {
			GUI.DrawTexture(GetKnobRect(info), GUI_Info.ConnectorKnob);
		}

		public override Vector2 GetGUIPosForOrderGraph(Vector2 size, GUI_Info info) {
			var r = GetKnobRect(info);
			r.x -= size.x;
			r.y += (r.height - size.y) * 0.5f;
			return r.position;
		}
#endif
	}
}
