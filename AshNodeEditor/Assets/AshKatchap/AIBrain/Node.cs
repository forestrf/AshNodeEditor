using Ashkatchap.AIBrain.Nodes;
using Ashkatchap.Shared.Collections;
using UnityEngine;

namespace Ashkatchap.AIBrain {
	public abstract partial class Node : MonoBehaviour, INodeInterruptor {
		/// <summary>
		/// Update()
		/// </summary>
		/// <param name="executionResult"></param>
		/// <returns>Node to add to the stack</returns>
		public abstract NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult);

		public virtual void InterruptExecution() { }

		public UnorderedList<INodeInterruptor> interruptors = new UnorderedList<INodeInterruptor>(0, 4);

		protected void AddAsInterruptor() {
			interruptors.Add(this);
		}
		protected void RemoveAsInterruptor() {
			interruptors.Remove(this);
		}

		public virtual bool InterruptNeeded() {
			return false;
		}

		[HideInNormalInspector] public int timesLinkedAsTree = 0;
	}
}
