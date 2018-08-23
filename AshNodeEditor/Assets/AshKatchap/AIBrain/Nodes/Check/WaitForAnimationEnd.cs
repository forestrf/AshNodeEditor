using Ashkatchap.AIBrain.GeneratedNodes;
using Ashkatchap.AIBrain.Nodes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ashkatchap.AIBrain {
	// HACER ALGO SIMILAR A VARIABLE: Crear los inputs conforme se necesitan según el tipo del objeto puesto
	[Serializable]
	[CreateNode("Check/Wait Until Animation Ends")]
	public class WaitForAnimationEnd : Node {
		public Input_UnityEngine_Animator animator;
		public Input_System_Int32 animatorLayer;
		public Input_UnityEngine_AnimationClip animationClip;

		public NodeTreeOutput child;

		private static List<AnimatorClipInfo> infos = new List<AnimatorClipInfo>();
		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			animator.GetValue().GetCurrentAnimatorClipInfo(animatorLayer.GetValue(), infos);

			var clip = animationClip.GetValue();
			if (infos.Count > 0) {
				foreach (var info in infos) {
					if (info.clip == clip) {
						executionResult = ExecutionResult.StopExecutionAtMe;
						return null;
					}
				}
			}
			executionResult = ExecutionResult.Success;
			return child;
		}

		public override void Calculate() { }

#if UNITY_EDITOR
		public override void Init() {
			animator = CreateIO<Input_UnityEngine_Animator>();
			animatorLayer = CreateIO<Input_System_Int32>();
			animationClip = CreateIO<Input_UnityEngine_AnimationClip>();
			child = CreateTreeOutput();
		}

		protected override void Draw() {
			UnityEditor.EditorGUIUtility.labelWidth = 60;
			UnityEditor.EditorGUIUtility.fieldWidth = 40;

			animator.DisplayLayout("Animator");
			animatorLayer.DisplayLayout("Layer");
			animationClip.DisplayLayout("Clip");

			GUILayout.BeginHorizontal();
			child.DisplayLayout("After waiting");
			GUILayout.EndHorizontal();
		}
#endif
	}
}
