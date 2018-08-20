using UnityEngine;
using Ashkatchap.Shared;
using System;
using Ashkatchap.AIBrain.Nodes;
using System.Collections.Generic;
using System.Text.RegularExpressions;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Ashkatchap.AIBrain {
	public abstract partial class Node : MonoBehaviour, IChangeableName
#if UNITY_EDITOR
		, INodeCanvasGUIGraphForOrder
#endif
		{
		[SerializeField] private string nodeName;

		[HideInNormalInspector] [SerializeField] public Context nodeCanvas;
		[HideInNormalInspector] [SerializeField] protected NodeInput[] inputs;
		[HideInNormalInspector] [SerializeField] protected NodeOutput[] outputs;
		[HideInNormalInspector] [SerializeField] protected NodeTreeOutput[] treeOutputs;

		public abstract void Calculate();

#if UNITY_EDITOR
		public abstract void Init();

		[HideInNormalInspector] [SerializeField] public Rect positionSize;

		protected T CreateIO<T>() where T : NodeIO {
			var io = Undo.AddComponent<T>(nodeCanvas.IoGO.gameObject);
			Type genericType = typeof(T).BaseType.GetGenericArguments()[0];
			io.Configure(this, genericType);
			return io;
		}
		protected NodeIO CreateIO(Type type) {
			var io = Undo.AddComponent(nodeCanvas.IoGO.gameObject, type) as NodeIO;
			Type genericType = type.BaseType.GetGenericArguments()[0];
			io.Configure(this, genericType);
			return io;
		}
		protected T CreateInput<T, T2>(T2 defaultValue) where T : NodeInput<T2> {
			var io = CreateIO<T>();
			io.SetDefaultValue(defaultValue);
			return io;
		}
		protected T CreateOutput<T, T2>(T2 defaultValue) where T : NodeOutput<T2> {
			var io = CreateIO<T>();
			io.SetDefaultValue(defaultValue);
			return io;
		}
		protected NodeTreeOutput CreateTreeOutput() {
			var io = Undo.AddComponent<NodeTreeOutput>(nodeCanvas.IoGO.gameObject);
			io.Configure(this);
			return io;
		}

		/// <summary>
		/// Applies a connection between output and input
		/// </summary>
		public virtual bool TryApplyConnection(NodeOutput output, NodeInput input) {
			if (CanApplyConnection(output, input)) {
				input.AddNodeOutput(output);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Init this node. Has to be called when creating a child node of this
		/// </summary>
		public void Init(Context nodeCanvas, Vector2 position) {
			nodeName = Regex.Replace(this.GetType().Name, "(\\B[A-Z])", " $1");

			positionSize.position = position;
			this.nodeCanvas = nodeCanvas;
			nodeCanvas.AddNode(this);

			inputs = new NodeInput[0];
			outputs = new NodeOutput[0];
			treeOutputs = new NodeTreeOutput[0];

			Init();
		}

		/// <summary>
		/// Check if an output and an input can be connected (same type, ...)
		/// </summary>
		public static bool CanApplyConnection(NodeOutput output, NodeInput input) {
			if (input == null || output == null)
				return false;

			if (input.body == output.body || input.ContainsNodeOutput(output))
				return false;

			if (!input.CanConnectWith(output))
				return false;

			return true;
		}

		/// <summary>
		/// Callback when the node is deleted. Extendable by the child node, but always call base.OnDelete when overriding !!
		/// </summary>
		public void OnDelete() {
			foreach (var elem in inputs.GetCopy()) elem.OnDelete();
			foreach (var elem in outputs.GetCopy()) elem.OnDelete();
			foreach (var elem in treeOutputs.GetCopy()) elem.OnDelete();

			nodeCanvas.RemoveNode(this);
			UndoWrapper.DestroyObject(this);
		}

		public void AddInput(NodeInput elem) {
			this.Add("inputs", elem);
		}
		public void AddOutput(NodeOutput elem) {
			this.Add("outputs", elem);
		}
		public void AddTreeOutput(NodeTreeOutput elem) {
			this.Add("treeOutputs", elem);
		}
		public void RemoveInput(NodeInput elem) {
			this.Remove("inputs", elem);
		}
		public void RemoveOutput(NodeOutput elem) {
			this.Remove("outputs", elem);
		}
		public void RemoveTreeOutput(NodeTreeOutput elem) {
			this.Remove("treeOutputs", elem);
		}

		public void SetName(string newName) {
			this.SetValue("nodeName", newName);
		}
		public string GetName() { return nodeName; }

		[NonSerialized] public float lastExecutedTime = -9999;

		public Rect rect {
			get { return new Rect(canvasPosition, Vector2.zero); }
		}
		public void SetCanvasPosition(Vector2 value, GUI_Info info) {
			if (!info.scrollWindow) {
				this.SetValue("positionSize", new Rect(value - nodeCanvas.scrollOffset, positionSize.size));
			}
		}
		public Vector2 canvasPosition {
			get { return positionSize.position + nodeCanvas.scrollOffset; }
		}

		public Rect rectPixelCorrected {
			get { return new Rect(canvasPositionPixelCorrected, Vector2.zero); }
		}
		public Vector2 canvasPositionPixelCorrected {
			get { return new Vector2((int) canvasPosition.x, (int) canvasPosition.y); }
		}

		[NonSerialized] public bool isVisible;

		/// <summary>
		/// Function implemented by the children to draw the node
		/// </summary>
		public void DrawNode() {
			GUILayoutUtility.GetRect(GUI.skin.label.CalcSize(new GUIContent(nodeName)).x, 0);
			EditorGUIUtility.labelWidth = 70;
			EditorGUIUtility.fieldWidth = 40;
			EditorGUIUtility.wideMode = false;
			try {
				Draw();
			} catch (Exception e) {
				Debug.LogError(e);
			}
		}

		protected abstract void Draw();

		[NonSerialized] public bool isNodeSelected = false;

		public virtual void OnDrawGizmosContextSelected() { }

		public void DrawInternalInspectorNode() {
			EditorGUIUtility.labelWidth = 70;
			EditorGUIUtility.fieldWidth = 40;
			EditorGUIUtility.wideMode = false;

			foreach (var input in inputs) {
				input.DisplayLayout(false);
				// Do variables here to hide them from wiring
			}

			DrawInternalInspector();
		}

		protected virtual void DrawInternalInspector() { }
		public virtual void OnSelectedDrawCanvas(GUI_Info info) { }

		private bool InBounds(Vector2 windowSize) {
			Vector2 pos = canvasPositionPixelCorrected;
			return pos.x + positionSize.width > 0 && pos.y + positionSize.height > 0
				&& pos.x < windowSize.x && pos.y < windowSize.y;
		}
		public bool CalculateVisibility(Vector2 windowSize) {
			return isVisible = InBounds(windowSize);
		}

		/// <summary>
		/// Returns the input knob that is at the position on this node or null
		/// </summary>
		public NodeInput GetInputAtPos(Vector2 pos, GUI_Info info) {
			foreach (var input in inputs) // Search for an input at the position
				if (input.GetKnobRect(info).Contains(pos))
					return input;
			return null;
		}

		/// <summary>
		/// Returns the output knob that is at the position on this node or null
		/// </summary>
		public NodeOutput GetOutputAtPos(Vector2 pos, GUI_Info info) {
			foreach (var output in outputs) // Search for an output at the position
				if (output.GetKnobRect(info).Contains(pos))
					return output;
			return null;
		}

		/// <summary>
		/// Returns the tree output knob that is at the position on this node or null
		/// </summary>
		public NodeTreeOutput GetTreeOutputAtPos(Vector2 pos, GUI_Info info) {
			foreach (var treeOutput in treeOutputs) // Search for an output at the position
				if (treeOutput.GetKnobRect(info).Contains(pos))
					return treeOutput;
			return null;
		}

		public Vector2 GetGUIPosForOrderGraph(Vector2 size, GUI_Info info) {
			var r = positionSize;
			r.position = canvasPosition;
			r.x += (r.width - size.x) * 0.5f;
			r.y -= size.y;
			return r.position;
		}

		public IEnumerable<NodeInput> GetInputs() { return inputs; }
		public IEnumerable<NodeOutput> GetOutputs() { return outputs; }
		public IEnumerable<NodeTreeOutput> GetTreeOutputs() { return treeOutputs; }
#endif
	}
}
