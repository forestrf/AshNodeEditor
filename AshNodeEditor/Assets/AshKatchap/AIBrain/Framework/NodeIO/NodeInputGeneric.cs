using Ashkatchap.Shared;
using System;
using UnityEngine;

namespace Ashkatchap.AIBrain.Nodes {
	[Serializable]
	public abstract class NodeInput<T> : NodeInput {
		public T defaultValue;

		public T GetValue() {
#if UNITY_EDITOR
			lastExecutedTime = Time.time;
			if (body.nodeCanvas.debug) body.nodeCanvas.AddCalledElementInOrder(new HistoryElement(this, "Value?"));
#endif

			if (nodeOutput != null) {
				// Calculate only if it is not set as a tree node in runtime
				if (nodeOutput.body.timesLinkedAsTree == 0) {
#if UNITY_EDITOR
					if (body.nodeCanvas.debug) body.nodeCanvas.AddCalledElementInOrder(new HistoryElement(nodeOutput.body, "Launching calculator"));
					nodeOutput.body.lastExecutedTime = Time.time;
#endif
					nodeOutput.body.Calculate();
#if UNITY_EDITOR
					if (body.nodeCanvas.debug) body.nodeCanvas.AddCalledElementInOrder(new HistoryElement(this, "Value Calculated: " + nodeOutput.GetValueAsObject().ToString()));
#endif
				}
#if UNITY_EDITOR
				else {
					if (body.nodeCanvas.debug) body.nodeCanvas.AddCalledElementInOrder(new HistoryElement(this, "Value Cached: " + (nodeOutput.GetValueAsObject() == null ? "Null" : nodeOutput.GetValueAsObject().ToString())));
				}
#endif
				var vv = nodeOutput as NodeOutput<T>;
				if (vv == null) {
					return (T) nodeOutput.GetValueAsObject();
				}
				else {
					return vv.value;
				}
			}
			else {
#if UNITY_EDITOR
				if (body.nodeCanvas.debug) body.nodeCanvas.AddCalledElementInOrder(new HistoryElement(this, "Value Default: " + defaultValue.ToString()));
#endif
				return defaultValue;
			}
		}

		public T GetCachedValue() {
			var vv = nodeOutput as NodeOutput<T>;
			return nodeOutput != null ? vv.value : defaultValue;
		}

		public override object GetValueAsObject() {
			var vv = nodeOutput as NodeOutput<T>;
			return nodeOutput != null ? vv.value : defaultValue;
		}

		public override void SetDefaultValueFromInput(NodeInput input) {
			defaultValue = (input as NodeInput<T>).GetValue();
		}

#if UNITY_EDITOR
		public override void AddNodeOutput(NodeOutput nodeOutput, bool completely = true) {
			if (base.nodeOutput != null)
				base.nodeOutput.RemoveNodeInput(this);
			SetOutput(nodeOutput);
			if (completely) nodeOutput.AddNodeInput(this, false);
		}
		public override void RemoveNodeOutput(NodeOutput nodeOutput, bool completely = true) {
			if (completely) nodeOutput.RemoveNodeInput(this, false);
			SetOutput(null);
		}
		public override bool ContainsNodeOutput(NodeOutput nodeOutput) {
			return base.nodeOutput == nodeOutput;
		}
		public override bool CanConnectWith(NodeOutput nodeOutput) {
			var genericArguments = nodeOutput.GetType().BaseType.GetGenericArguments();
			if (genericArguments.Length == 1) {
				return typeof(T).IsAssignableFrom(genericArguments[0]);
			}
			return false;
		}

		public override void SetDefaultValue(object defaultValue) {
			UndoWrapper.RecordObject(this, "SetDefaultValue");
			if (defaultValue == null) {
				this.defaultValue = default(T);
			}
			else {
				this.defaultValue = (T) defaultValue;
			}
		}

		public override void OnDelete() {
			base.OnDelete();
			if (nodeOutput != null)
				nodeOutput.RemoveNodeInput(this);
		}

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
