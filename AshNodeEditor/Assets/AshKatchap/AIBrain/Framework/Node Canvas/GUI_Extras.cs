using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ashkatchap.AIBrain.Nodes {
#if UNITY_EDITOR
	public interface INodeCanvasGUIGraphForOrder {
		Vector2 GetGUIPosForOrderGraph(Vector2 wantedSize, GUI_Info info);
	}

	public struct HistoryElement {
		public string text;
		public INodeCanvasGUIGraphForOrder sender;
		public HistoryElement(INodeCanvasGUIGraphForOrder sender, string text) {
			this.text = text;
			this.sender = sender;
		}
	}

	public struct GUI_Info {
		public static Texture2D ConnectorKnob;
		public static Texture2D TreeKnob;
		public static Texture2D Background;
		public static Texture2D window;
		public static GUIStyle nodeBase;
		public static GUIStyle nodeBox;
		public static GUIStyle nodeLabelBold;
		public static GUIStyle nodeButton;
		public static GUIStyle info;
		public static GUIStyle variablePreview;
		public static GUIStyle nodeStyle;

		static Dictionary<Type, Color> type_2_color = new Dictionary<Type, Color>() {
			{ typeof(float), UnityEngine.Random.ColorHSV(0, 1, 0, 1, 0.5f, 1) },
			{ typeof(int), UnityEngine.Random.ColorHSV(0, 1, 0, 1, 0.5f, 1) },
			{ typeof(bool), UnityEngine.Random.ColorHSV(0, 1, 0, 1, 0.5f, 1) },
			{ typeof(string), UnityEngine.Random.ColorHSV(0, 1, 0, 1, 0.5f, 1) },
			{ typeof(Vector2), UnityEngine.Random.ColorHSV(0, 1, 0, 1, 0.5f, 1) },
			{ typeof(Vector3), UnityEngine.Random.ColorHSV(0, 1, 0, 1, 0.5f, 1) },
			{ typeof(Vector4), UnityEngine.Random.ColorHSV(0, 1, 0, 1, 0.5f, 1) },
			{ typeof(Quaternion), UnityEngine.Random.ColorHSV(0, 1, 0, 1, 0.5f, 1) },
			{ typeof(Animator), UnityEngine.Random.ColorHSV(0, 1, 0, 1, 0.5f, 1) },
		};

		public bool scrollWindow;
		public int knobSize;
		public float zoom;
		public GUI_Info(bool scrollingWindow, int knobSize, float zoom) {
			this.scrollWindow = scrollingWindow;
			this.knobSize = knobSize;
			this.zoom = zoom;
		}

		public static Color TypeToColor(Type type) {
			Color c;
			if (type_2_color.TryGetValue(type, out c))
				return c;
			return new Color(0.7f, 0.7f, 0.7f);
		}
	}
#endif
}
