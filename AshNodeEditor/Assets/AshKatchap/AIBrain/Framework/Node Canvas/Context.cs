using Ashkatchap.AIBrain.Nodes;
using Ashkatchap.Shared;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ashkatchap.AIBrain {
	public partial class Context : MonoBehaviour {
		[SerializeField] private Node[] nodes = new Node[0];
		[SerializeField] private Group[] groups = new Group[0];
		[SerializeField] private Transform _contextContainer;

		void Reset() {
			if (!string.IsNullOrEmpty(gameObjectContainerName)) {
				var go = transform.Find(gameObjectContainerName);
				if (null != go) UndoWrapper.DestroyObject(go);
			}
			gameObjectContainerName = "Context container " + Guid.NewGuid().ToString();
		}

		public void AddNode(Node node) {
			this.Add("nodes", node);
		}
		public void RemoveNode(Node node) {
			this.Remove("nodes", node);
		}

		public void AddGroup(Group group) {
			this.Add("groups", group);
		}
		public void RemoveGroup(Group group) {
			this.Remove("groups", group);
		}

		public Transform contextContainer {
			get {
				if (_contextContainer == null) {
					_contextContainer = transform.Find(gameObjectContainerName);
					if (_contextContainer == null) {
						_contextContainer = new GameObject(gameObjectContainerName).transform;
						_contextContainer.parent = transform;
					}
					_contextContainer.gameObject.hideFlags = HideFlags.HideInHierarchy;
				}
				return _contextContainer;
			}
		}

		private void OnDestroy() {
			if (null != _contextContainer)
				Destroy(_contextContainer.gameObject);
		}

#if UNITY_EDITOR
		[SerializeField] public Vector2 scrollOffset;
		[SerializeField] public Zoom zoom = new Zoom();
		[SerializeField] public string gameObjectContainerName;

		public void ForeachNode(Action<Node> action) {
			foreach (var node in nodes) action(node);
		}
		public void ForeachGroup(Action<Group> action) {
			foreach (var group in groups) action(group);
		}
		public Node GetNode(int i) { return nodes[i]; }
		public Group GetGroup(int i) { return groups[i]; }
		public int GetNodesLength() { return nodes.Length; }
		public int GetGroupsLength() { return groups.Length; }

		[NonSerialized] public readonly List<HistoryElement> calledElementsInOrder = new List<HistoryElement>();
		[NonSerialized] public bool debug = false;
		[NonSerialized] public bool stepByStep = false;
		[NonSerialized] private int lastFrame = -1;

		public void AddCalledElementInOrder(HistoryElement element) {
			if (debug) {
				if (lastFrame != Time.frameCount) {
					calledElementsInOrder.Clear();
					lastFrame = Time.frameCount;
				}
				calledElementsInOrder.Add(element);
				if (stepByStep)
					Debug.Break();
			}
			else {
				calledElementsInOrder.Clear();
			}
		}

		public class Zoom {
			[Range(0, 1)]
			public float zoomEffectDuration = 0.2f;
			private float _zoom = 1;
			public bool IsZooming { get; private set; }
			public float GetZoom() {
				return _zoom;
			}

			private float targetZoom = 1;
			private float previousZoom = 1;
			private float timeZoomRequested = 0;

			public void GUIUpdate(UnityEditor.EditorWindow window) {
				_zoom = Mathf.Lerp(previousZoom, targetZoom, (Time.realtimeSinceStartup - timeZoomRequested) / zoomEffectDuration);
				if (Event.current.type == EventType.Repaint) {
					if (_zoom == targetZoom) {
						IsZooming = false;
					}
					else {
						window.Repaint();
					}
				}
			}
			public void DoZoom(float multiplier) {
				float previousTargetZoom = targetZoom;
				targetZoom *= multiplier;
				if (targetZoom > 1) targetZoom = 1;
				if (previousTargetZoom != targetZoom) {
					previousZoom = _zoom;
					IsZooming = true;
					timeZoomRequested = Time.realtimeSinceStartup;
				}
			}
		}

		private void OnDrawGizmosSelected() {
			foreach (var node in nodes) {
				node.OnDrawGizmosContextSelected();
			}
		}
#endif
	}
}
