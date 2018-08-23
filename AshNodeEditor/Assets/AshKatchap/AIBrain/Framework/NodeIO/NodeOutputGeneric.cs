using System;
using UnityEngine;

namespace Ashkatchap.AIBrain.Nodes {
	[Serializable]
	public abstract class NodeOutput<T> : NodeOutput {
#if UNITY_EDITOR
		private T _value;
		public T value {
			set {
				lastExecutedTime = Time.time;
				if (body.nodeCanvas.debug) body.nodeCanvas.AddCalledElementInOrder(new HistoryElement(this, "Value Setted: " + (value == null ? "Null" : value.ToString())));
				_value = value;
			}
			get {
				return _value;
			}
		}
#else
		public T value;
#endif

		public override void SetFromInput(NodeInput input) {
			value = (input as NodeInput<T>).GetValue();
		}

		public override object GetValueAsObject() {
			return _value;
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
