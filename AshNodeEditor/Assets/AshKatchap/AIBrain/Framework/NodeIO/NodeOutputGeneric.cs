using System;
using UnityEngine;

namespace Ashkatchap.AIBrain.Nodes {
	[Serializable]
	public abstract class NodeOutput<T> : NodeOutput {
		public T value;

		public override void SetFromInput(NodeInput input) {
			SetValue((input as NodeInput<T>).GetValue());
		}

		public void SetValue(T value) {
#if UNITY_EDITOR
			lastExecutedTime = Time.time;
			if (body.nodeCanvas.debug) body.nodeCanvas.AddCalledElementInOrder(new HistoryElement(this, "Value Setted: " + (value == null ? "Null" : value.ToString())));
#endif
			this.value = value;
		}

		public override object GetValueAsObject() {
			return value;
		}

#if UNITY_EDITOR
		public override void DrawKnob(GUI_Info info) {
			GUI.color = GetColor();
			GUI.DrawTexture(GetKnobRect(info), GUI_Info.ConnectorKnob);
		}

		public override Color GetColor() {
			return GUI_Info.TypeToColor(typeof(T));
		}
#endif
	}
}
