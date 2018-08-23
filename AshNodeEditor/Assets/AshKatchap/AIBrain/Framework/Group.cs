using Ashkatchap.AIBrain.Nodes;
using Ashkatchap.Shared;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ashkatchap.AIBrain {
	public class Group : MonoBehaviour, IChangeableName {
		static Vector2 border = new Vector2(24, 24);

		[SerializeField] private string groupName = "New Group";

		[HideInNormalInspector] [SerializeField] private Context nodeCanvas;
		[HideInNormalInspector] [SerializeField] private Node[] nodes;

		public bool ContainsNode(Node node) {
			return nodes.IndexOf(node) != -1;
		}
		public IEnumerable<Node> GetNodes() {
			return nodes;
		}

#if UNITY_EDITOR
		public string GetName() { return groupName; }

		[NonSerialized] private Rect oldRect;
		[NonSerialized] public bool needsUpdate = true;
		Rect GetBoundingRect() {
			if (needsUpdate) {
				CheckArray();
				if (nodes.Length > 0) {
					Bounds b = new Bounds(nodes[0].positionSize.position, Vector3.zero);
					foreach (var node in nodes) {
						b.Encapsulate(node.positionSize.position);
						b.Encapsulate(node.positionSize.position + node.positionSize.size);
					}
					oldRect = new Rect(b.min.XY() - border, b.max.XY() - b.min.XY() + border * 2);
				}
				else {
					oldRect = new Rect();
				}
				needsUpdate = false;
			}
			return oldRect;
		}

		/// <summary>
		/// Init this node. Has to be called when creating a child node of this
		/// </summary>
		public void Init(Context nodeCanvas) {
			SetName(this.GetType().Name);
			this.nodeCanvas = nodeCanvas;
			nodeCanvas.AddGroup(this);

			nodes = new Node[0];
		}

		public void OnDelete() {
			nodeCanvas.RemoveGroup(this);
			UndoWrapper.DestroyObject(this);
		}

		public void SetName(string newName) {
			this.SetSerializedValue("groupName", newName);
		}

		public void AddNode(Node node) {
			this.Add("nodes", node);
		}
		public void RemoveNode(Node node) {
			this.Remove("nodes", node);
		}
		public void RemoveAllNodes() {
			this.ClearArray("nodes");
		}
		void CheckArray() {
			this.Remove("nodes", null);
		}

		public Rect PositionSize {
			get { return GetBoundingRect(); }
		}

		public Rect Rect {
			get { return new Rect(CanvasPosition, Vector2.zero); }
		}
		public Vector2 CanvasPosition {
			get { return PositionSize.position + nodeCanvas.scrollOffset; }
		}

		public Rect RectPixelCorrected {
			get { return new Rect(CanvasPositionPixelCorrected, Vector2.zero); }
		}
		public Vector2 CanvasPositionPixelCorrected {
			get { return new Vector2((int) CanvasPosition.x, (int) CanvasPosition.y); }
		}

		[SerializeField] private Color color = new Color(128 / 255f, 0, 195 / 255f);

		public void SetColor(Color color) {
			this.SetSerializedValue("color", color);
		}

		public void AddDelta(Vector2 delta, GUI_Info info) {
			foreach (var node in nodes) {
				node.SetCanvasPosition(node.canvasPosition + delta, info);
			}
		}

		private static GUIStyle boxStyle;

		/// <summary>
		/// Function implemented by the children to draw the node
		/// </summary>
		public void Draw(GUI_Info guiInfo) {
			CheckArray();
			if (nodes.Length == 0) {
				OnDelete();
			}
			else {
				if (boxStyle == null || boxStyle.normal.background == null) {
					boxStyle = new GUIStyle(GUI.skin.box);
					boxStyle.normal.background = ColorToTex(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0.3f));
					boxStyle.padding = boxStyle.margin = new RectOffset();
					boxStyle.border = new RectOffset(1, 1, 1, 1);
					boxStyle.normal.textColor = GetTextColor(color);
				}

				Color originalColor = GUI.backgroundColor;
				GUI.backgroundColor = color;
				Rect r = RectPixelCorrected;
				r.size = PositionSize.size;
				GUI.Box(r, groupName, boxStyle);
				GUI.backgroundColor = originalColor;

				Rect rColor = RectPixelCorrected;
				rColor.size = new Vector2(40, 20);
				rColor.x += PositionSize.size.x - 40;
				try {
					var oldColor = color;
					SetColor(UnityEditor.EditorGUI.ColorField(rColor, color));
					if (oldColor != color) {
						boxStyle.normal.textColor = GetTextColor(color);
					}
				}
				catch (ExitGUIException) { }
			}
		}

		/// <summary>
		/// Create a 1x1 tex with color col
		/// </summary>
		private static Texture2D ColorToTex(Color border, Color inside) {
			Texture2D tex = new Texture2D(3, 3, TextureFormat.ARGB32, false, true);
			tex.filterMode = FilterMode.Point;
			tex.SetPixel(0, 0, border); tex.SetPixel(1, 0, border); tex.SetPixel(2, 0, border);
			tex.SetPixel(0, 1, border); tex.SetPixel(1, 1, inside); tex.SetPixel(2, 1, border);
			tex.SetPixel(0, 2, border); tex.SetPixel(1, 2, border); tex.SetPixel(2, 2, border);
			tex.Apply();
			return tex;
		}

		/// <summary>
		/// White/Black Color given another color and looking at its contrast
		/// </summary>
		static Color GetTextColor(Color backgroundColor) {
			return ((backgroundColor.r * 299) + (backgroundColor.g * 587) + (backgroundColor.b * 114)) / 1000 > 0.5f ? Color.black : Color.white;
		}
#endif
	}
}
