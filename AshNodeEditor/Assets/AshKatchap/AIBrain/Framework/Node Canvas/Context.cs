using UnityEngine;
using System.Collections.Generic;
using System;
using Ashkatchap.AIBrain.Nodes;
using Ashkatchap.Shared;

namespace Ashkatchap.AIBrain {
	public partial class Context : MonoBehaviour {
		[HideInNormalInspector] [SerializeField] private Node[] nodes = new Node[0];
		[HideInNormalInspector] [SerializeField] private Group[] groups = new Group[0];
		[HideInNormalInspector] [SerializeField] private Transform _nodesGO;
		[HideInNormalInspector] [SerializeField] private Transform _ioGO;
		[HideInNormalInspector] [SerializeField] private Transform _groupsGO;

#if UNITY_EDITOR
		[NonSerialized] public Vector2 scrollOffset;
		[NonSerialized] public Zoom zoom = new Zoom();
		
		public const string NAME_nodesGO = "Nodes", NAME_ioGO = "Connections", NAME_groupsGO = "Groups";
		
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

		public Transform NodesGO {
			get {
				ResolveSubObjects();
				return _nodesGO;
			}
		}
		public Transform IoGO {
			get {
				ResolveSubObjects();
				return _ioGO;
			}
		}
		public Transform GroupsGO {
			get {
				ResolveSubObjects();
				return _groupsGO;
			}
		}

		void ResolveSubObjects() {
			FixSubObject(ref _nodesGO, NAME_nodesGO);
			FixSubObject(ref _ioGO, NAME_ioGO);
			FixSubObject(ref _groupsGO, NAME_groupsGO);
		}

		void FixSubObject(ref Transform tr, string name) {
			if (tr == null) {
				tr = transform.Find(name);
				if (tr == null) {
					tr = new GameObject(name).transform;
					tr.parent = transform;
				}
			}
		}

		public IEnumerable<Node> GetNodes() { return nodes; }
		public IEnumerable<Group> GetGroups() { return groups; }
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
			} else {
				calledElementsInOrder.Clear();
			}
		}
		
		public class Zoom {
			[Range(0, 1)]
			public float zoomEffectDuration = 0.2f;
			public float zoom { get; private set; }
			public bool IsZooming { get; private set; }

			private float targetZoom = 1;
			private float previousZoom = 1;
			private float timeZoomRequested = 0;

			public void GUIUpdate(UnityEditor.EditorWindow window) {
				zoom = Mathf.Lerp(previousZoom, targetZoom, (Time.realtimeSinceStartup - timeZoomRequested) / zoomEffectDuration);
				if (Event.current.type == EventType.Repaint) {
					if (zoom == targetZoom) {
						IsZooming = false;
					} else {
						window.Repaint();
					}
				}
			}
			public void DoZoom(float multiplier) {
				float previousTargetZoom = targetZoom;
				targetZoom *= multiplier;
				if (targetZoom > 1) targetZoom = 1;
				if (previousTargetZoom != targetZoom) {
					previousZoom = zoom;
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
