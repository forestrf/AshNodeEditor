using Ashkatchap.Shared;
using UnityEngine;

namespace Ashkatchap.AIBrain {
	public partial class Context : MonoBehaviour {
		[SerializeField] protected Node firstNode;
		PC programCounter;

		void Awake() {
			programCounter = new PC();
		}

		public void Update() {
			if (!programCounter.Tick(this))
				enabled = false;
		}

		public bool StackContainsNode(Node node) {
			return programCounter != null && programCounter.StackContainsNode(node);
		}

		public Node GetFirstNode() {
			return firstNode;
		}

		public void TryInterruptNode(Node node) {
			programCounter.TryInterruptNode(node);
		}

#if UNITY_EDITOR
		public void SetFirstNode(Node node) {
			this.SetSerializedValue("firstNode", node);
		}
#endif
	}
}
