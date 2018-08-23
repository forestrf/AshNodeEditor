using Ashkatchap.AIBrain.Nodes;
using Ashkatchap.Shared;
using System;
using UnityEngine;

namespace Ashkatchap.AIBrain {
	[Serializable]
	public abstract class ValueBase : Node {
		public override void InterruptExecution() { }

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			if (GetInput().HasOutput()) {
				SetValueFromInput(GetInput());
			}
			Calculate();
			executionResult = ExecutionResult.Success;
			return null;
		}

		public abstract NodeInput GetInput();
		public abstract NodeOutput GetOutput();
		public abstract void SetValueFromInput(NodeInput input);
		public abstract object GetValueAsObject();
		public abstract void SetValueAsObject(object value);

#if UNITY_EDITOR
		protected override void Draw() {
			UnityEditor.EditorGUIUtility.labelWidth = 50;
			UnityEditor.EditorGUIUtility.fieldWidth = 50;
			GUILayout.BeginHorizontal();
			this.LayoutPropertyField("value", GUIContent.none);
			GetInput().TrySetLastRect();
			GetOutput().DisplayLayout(GUIContent.none);
			GUILayout.EndHorizontal();
		}
#endif
	}
}
