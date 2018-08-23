using System;
using UnityEngine;

namespace Ashkatchap.AIBrain {
	public partial class Context {
		/// <summary>
		/// Program Counter
		/// </summary>
		[Serializable]
		public class PC {
			static readonly int stackMaxSize = 128;

			Node[] stack = new Node[stackMaxSize];
			int stackSize = 0;
			int lastFrameTicked = -1;

			/// <returns>false if the stack is clean</returns>
			public bool Tick(Context context) {
				if (lastFrameTicked != Time.frameCount) {
					for (int i = 0; i < stackSize; i++) {
						if (stack[i] == null) continue;

						for (int j = 0; j < stack[i].interruptors.Count; j++) {
							if (stack[i].interruptors.elements[j].InterruptNeeded()) {
								while (stackSize >= i) {
									Pop().InterruptExecution();
								}
								break;
							}
						}
					}
					lastFrameTicked = Time.frameCount;
				}

				if (stackSize == 0) {
					Push(context.firstNode);
				}

				ExecutionResult result = ExecutionResult.Success;

				int iterations = 0;

				while (stackSize > 0) {
					if (iterations > 100000) {
						Debug.LogWarning("More than 100000 nodes executed in a frame");
						Debug.Break();
						break;
					}
					iterations++;

					var node = Pop();
					if (node == null) continue;
#if UNITY_EDITOR
					node.lastExecutedTime = Time.time;
					if (node.nodeCanvas.debug) node.nodeCanvas.AddCalledElementInOrder(new Nodes.HistoryElement(node, "Executing"));
#endif

					var newNodeTree = node.Tick(out result, result);
#if UNITY_EDITOR
					if (node.nodeCanvas.debug) node.nodeCanvas.AddCalledElementInOrder(new Nodes.HistoryElement(node, "Execution finished. Result: " + result.ToString()));
					if (node.nodeCanvas.debug && newNodeTree != null && newNodeTree.outputNode != null) node.nodeCanvas.AddCalledElementInOrder(new Nodes.HistoryElement(node, "Returned node tree: " + newNodeTree.outputNode.GetName()));
#endif
					if (result == ExecutionResult.Exit) {
						while (stackSize > 0) Pop();
						break;
					}
					else if (result == ExecutionResult.Failure) {
					}
					else if (result == ExecutionResult.Success) {
						if (newNodeTree != null && newNodeTree.outputNode != null) {
#if UNITY_EDITOR
							newNodeTree.lastExecutedTime = Time.time;
#endif
							Push(newNodeTree.outputNode);
						}
					}
					else {
						Push(node);
						if (newNodeTree != null && newNodeTree.outputNode != null) {
#if UNITY_EDITOR
							newNodeTree.lastExecutedTime = Time.time;
#endif
							Push(newNodeTree.outputNode);
						}
						if (result == ExecutionResult.StopExecutionAtMe) {
							break;
						}
						else {
							result = ExecutionResult.Success;
						}
					}
				}

				// Clean stack from nulls
				for (int i = 0; i < stackSize; i++) {
					if (stack[i] == null) {
						for (int j = i + 1; j < stackSize; j++) {
							if (stack[j] != null) {
								stack[i] = stack[j];
								stack[j] = null;
								stackSize--;
								break;
							}
						}
					}
				}

				return stackSize != 0;
			}

			private Node Peek() {
				return stack[stackSize - 1];
			}
			public void Push(Node node) {
				stack[stackSize++] = node;
			}
			private Node Pop() {
				var node = stack[stackSize - 1];
				stack[--stackSize] = null;
				return node;
			}

			public bool StackContainsNode(Node node) {
				return Array.IndexOf(stack, node) != -1;
			}

			public void TryInterruptNode(Node node) {
				for (int i = 0; i < stackSize; i++) {
					if (stack[i] == node) {
						stack[i].InterruptExecution();
						stack[i] = null;
					}
				}
			}

			public bool StackHasNodes() {
				return stackSize > 0;
			}

			public void Clear() {
				while (stackSize > 0) {
					Pop().InterruptExecution();
				}
			}
		}
	}

	public enum ExecutionResult {
		/// <summary>
		/// This node has not finished doing all it needs to do, but a child node needs to run before continuing
		/// </summary>
		Running,
		/// <summary>
		/// This node has finished doing all it has to do OK
		/// </summary>
		Success,
		/// <summary>
		/// This node has finished doing all it has to do FAIL
		/// </summary>
		Failure,
		/// <summary>
		/// This node wants to stop the execution of the tree for now and wants to be executed again in the next tick
		/// </summary>
		StopExecutionAtMe,
		/// <summary>
		/// Finish/Break Execution and clear Stack
		/// </summary>
		Exit
	}
}
