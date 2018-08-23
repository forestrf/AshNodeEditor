using Ashkatchap.Shared.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ashkatchap.AIBrain {
	public class GraphDrawer<T> {
#if UNITY_EDITOR
		private Dictionary<T, CircularBuffer<float>> history = new Dictionary<T, CircularBuffer<float>>();
		public Dictionary<T, Color> colors = new Dictionary<T, Color>();
		private static Material material;

		private int histotyLength;

		private GraphDrawer() { }
		public GraphDrawer(int historyLength) {
			this.histotyLength = historyLength;
		}

		public void UpdateHistory(List<KeyValuePair<T, float>> newValues) {
			foreach (var elem in newValues) {
				CircularBuffer<float> buffer;
				if (!history.TryGetValue(elem.Key, out buffer)) {
					buffer = new CircularBuffer<float>(histotyLength, 0);
					history.Add(elem.Key, buffer);
				}
				buffer.Enqueue(elem.Value);
			}
			foreach (var elem in history) {
				bool exists = false;
				foreach (var value in newValues) {
					if (value.Key.Equals(elem.Key)) {
						exists = true;
						break;
					}
				}
				if (!exists) {
					elem.Value.Enqueue(elem.Value.PeekLastQueued());
				}
			}
		}

		public void ResetHistory() {
			history.Clear();
		}

		public void Draw(Rect graphRect) {
			if (Event.current.type != EventType.Repaint) return;
			if (material == null) CreateMaterial();

			material.SetPass(0);
			GL.PushMatrix();
			GL.Begin(GL.QUADS);
			GL.Color(new Color(0.1f, 0.1f, 0.1f, 1));
			GL.Vertex(new Vector2(graphRect.x, graphRect.y));
			GL.Vertex(new Vector2(graphRect.x + graphRect.width, graphRect.y));
			GL.Vertex(new Vector2(graphRect.x + graphRect.width, graphRect.y + graphRect.height));
			GL.Vertex(new Vector2(graphRect.x, graphRect.y + graphRect.height));
			GL.End();
			GL.Begin(GL.LINES);
			GL.Color(new Color(0.3f, 0.3f, 0.3f, 1));
			DrawLine(new Vector2(graphRect.x, graphRect.y), new Vector2(graphRect.x + graphRect.width, graphRect.y));
			DrawLine(new Vector2(graphRect.x + graphRect.width, graphRect.y), new Vector2(graphRect.x + graphRect.width, graphRect.y + graphRect.height));
			DrawLine(new Vector2(graphRect.x + graphRect.width, graphRect.y + graphRect.height), new Vector2(graphRect.x, graphRect.y + graphRect.height));
			DrawLine(new Vector2(graphRect.x, graphRect.y + graphRect.height), new Vector2(graphRect.x, graphRect.y));

			if (history != null) {
				float max = GetMaxHistory();
				float mult = graphRect.height / max;

				foreach (var elem in history) {
					Color c = Color.green;
					if (!colors.TryGetValue(elem.Key, out c)) c = Color.green;
					GL.Color(c);
					for (int j = 0; j < elem.Value.Count - 1; j++) {
						float y0 = mult * elem.Value[j];
						float y1 = mult * elem.Value[j + 1];
						DrawLine(
							new Vector2(graphRect.x + elem.Value.Count - 1 - j, graphRect.y + graphRect.height - (y0 > 0 ? y0 : 0)),
							new Vector2(graphRect.x + elem.Value.Count - j, graphRect.y + graphRect.height - (y1 > 0 ? y1 : 0)));
					}
				}
			}

			GL.End();
			GL.PopMatrix();
		}
		private static void DrawLine(Vector2 p1, Vector2 p2) {
			GL.Vertex(p1);
			GL.Vertex(p2);
		}
		private float GetMaxHistory() {
			float max = 1;
			foreach (var elem in history) {
				foreach (var f in elem.Value.NormalEnumerator) {
					if (max < f) max = f;
				}
			}
			return max;
		}

		private static Material CreateMaterial() {
			material = new Material(Shader.Find("Lines/VertexColored"));
			material.hideFlags = HideFlags.HideAndDontSave;
			return material;
		}
#endif
	}
}
