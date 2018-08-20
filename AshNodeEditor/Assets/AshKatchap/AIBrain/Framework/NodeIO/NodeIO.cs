using System;
using UnityEngine;

namespace Ashkatchap.AIBrain.Nodes {
	[Serializable]
	public abstract class NodeIO : MonoBehaviour {
		public Node body;

		public abstract object GetValueAsObject();

#if UNITY_EDITOR
		[NonSerialized] public float lastExecutedTime = -9999;
		[NonSerialized] protected Rect rect = new Rect();

		public virtual void Configure(Node body, Type connectionType) {
			this.body = body;
		}

		public abstract void OnDelete();
		public abstract void SetDefaultValue(object value);

		public abstract Color GetColor();
		public abstract void TrySetLastRect();
		public abstract void SetRect(Rect labelRect);
		public abstract Rect GetKnobRect(GUI_Info info);
		public abstract void DrawKnob(GUI_Info info);
		public abstract Vector2 GetGUIPosForOrderGraph(Vector2 size, GUI_Info info);
#endif
	}
}
