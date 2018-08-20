using Ashkatchap.AIBrain.GeneratedNodes;
using Ashkatchap.AIBrain.Nodes;
using Ashkatchap.Shared;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace Ashkatchap.AIBrain {
	[Serializable]
	[CreateNode("Utility/Decider")]
	public class Decider : Node {
		public Input_System_Single[] scoreInputs;
		public NodeTreeOutput[] scoreOutputs;
		public Input_System_Boolean firstScoreWins; // Otherwise Highest wins
		public Input_System_Boolean checkContinually; // Otherwise Highest wins

		[NonSerialized] int lastWinner = -1;
		[NonSerialized] bool waiting = false;
		

		public override NodeTreeOutput Tick(out ExecutionResult executionResult, ExecutionResult childResult) {
			if (waiting) {
				executionResult = childResult;
				InterruptExecution();
				return null;
			}
			int winner = GetWinner();
			lastWinner = winner;
			if (winner == -1) {
				executionResult = ExecutionResult.Failure;
				waiting = false;
				return null;
			} else {
				if (checkContinually.GetValue()) {
					executionResult = ExecutionResult.Running;
					AddAsInterruptor();
					waiting = true;
				} else {
					executionResult = ExecutionResult.Success;
					waiting = false;
				}
				return scoreOutputs[winner];
			}
		}

		[NonSerialized] List<KeyValuePair<int, float>> newValues = new List<KeyValuePair<int, float>>();
		private int GetWinner() {
#if UNITY_EDITOR
			if (graph == null) graph = new GraphDrawer<int>(64);
			newValues.Clear();
			for (int i = 0; i < scoreInputs.Length; i++) {
				newValues.Add(new KeyValuePair<int, float>(i, scoreInputs[i].GetValue()));
			}
			graph.UpdateHistory(newValues);
#endif
			if (firstScoreWins.GetValue()) {
				for (int i = 0; i < scoreInputs.Length; i++) {
					if (scoreInputs[i].GetValue() > 0) {
						return i;
					}
				}
			} else {
				int winner = -1;
				float winnerScore = 0;
				for (int i = 0; i < scoreInputs.Length; i++) {
					float score = scoreInputs[i].GetValue();
					if (winnerScore < score) {
						winnerScore = score;
						winner = i;
					}
				}
				if (winnerScore > 0) {
					return winner;
				}
			}
			
			return -1;
		}

		public override void Calculate() { }

		public override void InterruptExecution() {
			waiting = false;
			RemoveAsInterruptor();
		}

		public override bool InterruptNeeded() {
			if (checkContinually.GetValue()) {
				int winner = GetWinner();
				if (lastWinner != winner) {
					return true;
				} else {
					return false;
				}
			} else {
				return false;
			}
		}

#if UNITY_EDITOR
		// history of scores here somehow
		[NonSerialized] GraphDrawer<int> graph;

		public override void Init() {
			scoreInputs = new Input_System_Single[0];
			scoreOutputs = new NodeTreeOutput[0];
			firstScoreWins = CreateIO<Input_System_Boolean>();
			checkContinually = CreateIO<Input_System_Boolean>();
		}

		protected override void Draw() {
			UnityEditor.EditorGUIUtility.fieldWidth = 25;
			UnityEditor.EditorGUIUtility.labelWidth = 100;
			firstScoreWins.DisplayLayout(firstScoreWins.GetValue() ? new GUIContent("First wins", "First score greater than 0 will be selected") : new GUIContent("Higher wins", "Highest value greather than 0 will be selected"));
			checkContinually.DisplayLayout("Test Continually");

			UnityEditor.EditorGUIUtility.labelWidth = 25;
			Color originalColor = GUI.color;
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();
			for (int i = 0; i < scoreInputs.Length; i++) {
				if (lastWinner == i) GUI.color = Color.green;
				GUILayout.BeginHorizontal();
				scoreInputs[i].DisplayLayout(i.ToString());
				if (GUILayout.Button("-")) {
					scoreInputs[i].OnDelete();
					scoreOutputs[i].OnDelete();
					this.RemoveAt("scoreInputs", i);
					this.RemoveAt("scoreOutputs", i);
				}
				GUILayout.EndHorizontal();
				if (lastWinner == i) GUI.color = originalColor;
			}
			if (GUILayout.Button("+")) {
				this.Add("scoreInputs", CreateIO<Input_System_Single>());
				this.Add("scoreOutputs", CreateTreeOutput());
			}
			GUILayout.EndVertical();

			Rect graphRect = GUILayoutUtility.GetRect(64, 50);
			if (isVisible && Event.current.type == EventType.Repaint) {
				if (graph == null) graph = new GraphDrawer<int>(64);
				graph.Draw(graphRect);
			}
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			for (int i = 0; i < scoreOutputs.Length; i++) {
				scoreOutputs[i].DisplayLayout(i.ToString());
			}
			GUILayout.EndHorizontal();
		}
#endif

		[Serializable]
		public struct DeciderInput {
			public Input_System_Single score;
			public NodeTreeOutput output;

#if UNITY_EDITOR
			public DeciderInput(Decider decider) {
				score = decider.CreateIO<Input_System_Single>();
				output = decider.CreateTreeOutput();
			}

			public void OnDelete() {
				score.OnDelete();
				output.OnDelete();
			}
#endif
		}
	}
}
