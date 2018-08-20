using Ashkatchap.AIBrain.Nodes;
using System;
using Ashkatchap.AIBrain.GeneratedNodes;
using UnityEngine;
using System.Reflection;
using System.Linq;
using Ashkatchap.Shared;

namespace Ashkatchap.AIBrain {
	[Serializable]
	[CreateNode("Values/Variable (WRONG)")]
	public class Variable : Node {
		public ValueBase variable;
		public NodeInput input; // Use reflection to create
		public NodeOutput output; // Use reflection to create
		
		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			executionResult = ExecutionResult.Success;
			variable.SetValueFromInput(input);
			Calculate();
			return null;
		}

		public override void InterruptExecution() { }

		public override void Calculate() {
			variable.SetValueFromInput(input);
		}

#if UNITY_EDITOR
		public override void Init() {
			input = CreateIO<Input_System_Boolean>();
			output = CreateIO<Output_System_Boolean>();
		}

		protected override void Draw() {
			UnityEditor.EditorGUIUtility.fieldWidth = 140;
			UnityEditor.EditorGUIUtility.labelWidth = 50;
			this.LayoutPropertyField("variable");

			UpdateIOWithSource();
			
			GUILayout.BeginHorizontal();
			UnityEditor.EditorGUIUtility.labelWidth = 1;
			input.DisplayLayout(GUIContent.none);
			output.DisplayLayout(GUIContent.none);
			GUILayout.EndHorizontal();
		}

		void UpdateIOWithSource() {
			if (variable != null) {
				var genericParameterType = variable.GetType().BaseType.GetGenericArguments()[0];
				if (input.GetType().BaseType.GetGenericArguments()[0] != genericParameterType) {
					// recreate IO

					var inputNewBaseType = typeof(NodeInput<>).MakeGenericType(new Type[] { genericParameterType });
					var outputNewBaseType = typeof(NodeOutput<>).MakeGenericType(new Type[] { genericParameterType });

					var inputNewType = Assembly.GetAssembly(typeof(NodeInput<>)).GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(inputNewBaseType)).FirstOrDefault();
					var outputNewType = Assembly.GetAssembly(typeof(NodeOutput<>)).GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(outputNewBaseType)).FirstOrDefault();

					if (inputNewType != null && outputNewType != null) {
						input.OnDelete();
						output.OnDelete();
						input = CreateIO(inputNewType) as NodeInput;
						output = CreateIO(outputNewType) as NodeOutput;
					} else {
						variable.SetValueAsObject(null);
					}
				}
			}
		}
#endif
	}
}
