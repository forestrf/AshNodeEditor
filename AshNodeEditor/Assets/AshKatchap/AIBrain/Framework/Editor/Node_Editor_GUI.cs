using Ashkatchap.AIBrain.Nodes;
using Ashkatchap.Shared;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Ashkatchap.AIBrain {
	public class Node_Editor_GUI : EditorWindow {
		[SerializeField]
		private int nodeCanvasInstanceId;

		public Context nodeCanvas;

		public const string editorPath = "Assets/Ashkatchap/AIBrain/";

		public static int sideWindowWidth = 200;
		public const int knobSize = 14;
		public const int groupBorder = 4;
		
		public bool EDIT_GROUPS = false;

		[NonSerialized] public Color firstNodeColor = new Color(1, 1, 0, 1);
		[NonSerialized] public Color nodeInStackColor = new Color(.9f, .9f, .9f, 1);
		[NonSerialized] public Color executingNodeColor = new Color(1, 0, 0, 1);
		[NonSerialized] public Color nonExecutedColor = new Color(.5f, .5f, .5f, 1);
		[NonSerialized] public Color selectedNodeColor = new Color(1, 1, 1, 1);
		public float decayInSeconds = 0.5f;

		[NonSerialized] NodeOutput connectOutput;
		[NonSerialized] NodeTreeOutput connectTreeOutput;
		[NonSerialized] NodeInput[] connectInput = new NodeInput[0];
		[NonSerialized] bool scrollWindow = false;
		public static Vector2 mousePos;
		[SerializeField] private Node selectedNode;

		private int curDebugStep;
		Vector2 debugBoxSize = new Vector2(200, 20);

		static Texture2D texLose = null; // solo sirve para ver si se han perdido las referencias

		public static Dictionary<Type, Color> type_2_color; // Stop recalculating every time

		private bool initiated;

		[MenuItem("Window/Ashkatchap/AIBrain/Node Editor")]
		static void CreateEditor() {
			CreateInstance<Node_Editor_GUI>().Show();
		}

		#region GUI

		public static void CheckInitStatic() {
			if (texLose == null || GUI_Info.nodeBox == null) {
				texLose = ColorToTex(new Color(0, 0, 0));

				string miscPath = editorPath + "Framework/Editor/Misc/";
				
				GUI_Info.ConnectorKnob = AssetDatabase.LoadAssetAtPath(miscPath + "knob.png", typeof(Texture2D)) as Texture2D;
				GUI_Info.TreeKnob = AssetDatabase.LoadAssetAtPath(miscPath + "treeKnob.png", typeof(Texture2D)) as Texture2D;
				GUI_Info.Background = AssetDatabase.LoadAssetAtPath(miscPath + "background.png", typeof(Texture2D)) as Texture2D;
				GUI_Info.window = AssetDatabase.LoadAssetAtPath(miscPath + "window.png", typeof(Texture2D)) as Texture2D;

				GUI.skin.box.richText = true;
				GUI.skin.button.richText = true;

				GUI_Info.nodeBase = new GUIStyle(GUI.skin.box);
				GUI_Info.nodeBase.normal.background = ColorToTex(new Color(0.25f, 0.25f, 0.25f));
				GUI_Info.nodeBase.normal.textColor = new Color(0.7f, 0.7f, 0.7f);

				GUI_Info.nodeBox = new GUIStyle(GUI_Info.nodeBase) {
					margin = new RectOffset(8, 8, 5, 8),
					padding = new RectOffset(8, 8, 8, 8)
				};
				GUI_Info.nodeBox.normal.textColor = Color.white;

				GUI_Info.nodeLabelBold = new GUIStyle(GUI_Info.nodeBase) {
					fontStyle = FontStyle.Bold,
					wordWrap = false
				};
				GUI_Info.nodeLabelBold.normal.textColor = Color.white;

				GUI_Info.nodeButton = new GUIStyle(GUI.skin.button);
				GUI_Info.nodeButton.normal.textColor = new Color(0.3f, 0.3f, 0.3f);

				GUI_Info.info = new GUIStyle(GUI.skin.box) {
					alignment = TextAnchor.UpperCenter
				};
				GUI_Info.info.normal.background = ColorToTex(new Color(0, 0.389f, 0.6f));
				GUI_Info.info.normal.textColor = new Color(0.9f, 0.9f, 0.9f);
				GUI_Info.info.stretchWidth = true;

				GUI_Info.variablePreview = new GUIStyle(GUI.skin.box);
				GUI_Info.variablePreview.border.bottom = GUI_Info.variablePreview.border.top = GUI_Info.variablePreview.border.left = GUI_Info.variablePreview.border.right = 1;
				GUI_Info.variablePreview.normal.background = ColorToTex(new Color(0.7f, 0.7f, 0.7f, 0.8f), new Color(0.1f, 0.1f, 0.1f, 0.8f));
				GUI_Info.variablePreview.normal.textColor = new Color(0.9f, 0.9f, 0.9f);

				GUI_Info.nodeStyle = new GUIStyle(GUI.skin.window);
				GUI_Info.nodeStyle.normal.background = GUI_Info.window;
				GUI_Info.nodeStyle.onNormal.background = GUI_Info.window;
				GUI_Info.nodeStyle.border = new RectOffset(10, 10, 22, 10);
				GUI_Info.nodeStyle.padding = new RectOffset(2, 2, 20, 2);
				GUI_Info.nodeStyle.normal.textColor = Color.white;
				GUI_Info.nodeStyle.onNormal.textColor = Color.white;
			}

			if (CreateNodeAttribute.Cached.Count == 0) {
				CreateNodeAttribute.Prepare();
			}
		}

		public void checkInit() {
			CheckInitStatic();

			if (nodeCanvas == null) return;

			if (!initiated) {
				minSize = new Vector2(300, 300);
				initiated = true;
			}
		}

		Color originalColor_label_normal;
		Color originalColor_boldLabel_normal;
		Color originalColor_label_focused;
		Color originalColor_boldLabel_focused;

		void SetEditorColor() {
			EditorStyles.label.richText = true;
			EditorStyles.boldLabel.richText = true;

			originalColor_label_normal = EditorStyles.label.normal.textColor;
			originalColor_boldLabel_normal = EditorStyles.boldLabel.normal.textColor;
			originalColor_label_focused = EditorStyles.label.focused.textColor;
			originalColor_boldLabel_focused = EditorStyles.boldLabel.focused.textColor;

			EditorStyles.label.normal.textColor =
			EditorStyles.boldLabel.normal.textColor = Color.white;
			EditorStyles.label.focused.textColor =
			EditorStyles.boldLabel.focused.textColor = new Color(0.8f, 0.92f, 1);
		}
		void UndoEditorColor() {
			EditorStyles.label.normal.textColor = originalColor_label_normal;
			EditorStyles.boldLabel.normal.textColor = originalColor_boldLabel_normal;
			EditorStyles.label.focused.textColor = originalColor_label_focused;
			EditorStyles.boldLabel.focused.textColor = originalColor_boldLabel_focused;
		}

		[NonSerialized] GUI_Info guiInfo;
		public void OnGUI() {
			checkInit();

			guiInfo = new GUI_Info(scrollWindow, knobSize, null != nodeCanvas ? nodeCanvas.zoom.GetZoom() : 1);

			nodeCanvas = (Context) EditorGUILayout.ObjectField(nodeCanvas, typeof(Context), true);
			
			if (nodeCanvas == null && 0 != nodeCanvasInstanceId) {
				nodeCanvas = EditorUtility.InstanceIDToObject(nodeCanvasInstanceId) as Context;
				if (null == nodeCanvas) {
					nodeCanvasInstanceId = 0;
				}
			}

			if (nodeCanvas == null) return;

			nodeCanvasInstanceId = nodeCanvas.GetInstanceID();

			SetEditorColor();

			try {
				// Draw Background when Repainting
				if (Event.current.type == EventType.Repaint) { // Draw background when repainting
					float w = GUI_Info.Background.width * nodeCanvas.zoom.GetZoom();
					float h = GUI_Info.Background.height * nodeCanvas.zoom.GetZoom();

					Vector2 offset = new Vector2((nodeCanvas.scrollOffset.x * nodeCanvas.zoom.GetZoom()) % w - w,
													(nodeCanvas.scrollOffset.y * nodeCanvas.zoom.GetZoom()) % h - h);
					int tileX = Mathf.CeilToInt((position.width + (w - offset.x)) / w);
					int tileY = Mathf.CeilToInt((position.height + (h - offset.y)) / h);

					for (int x = 0; x < tileX; x++)
						for (int y = 0; y < tileY; y++)
							GUI.DrawTexture(new Rect(offset.x + x * w, offset.y + y * h, w, h), GUI_Info.Background);
				}


				// We want to scale our nodes, but as GUI.matrix also scales our widnow's clipping group, 
				// we have to scale it up first to receive a correct one as a result

				// End the default clipping group
				GUI.EndGroup();

				// The Rect of the new clipping group to draw our nodes in
				Rect ScaledCanvasRect = new Rect(0, 23, (position.width - sideWindowRect.width) / nodeCanvas.zoom.GetZoom(), position.height / nodeCanvas.zoom.GetZoom());

				// Now continue drawing using the new clipping group
				GUI.BeginGroup(ScaledCanvasRect);

				// Apply zoom
				GUIUtility.ScaleAroundPivot(Vector2.one * nodeCanvas.zoom.GetZoom(), Vector2.zero);


				// Draw groups
				nodeCanvas.ForeachGroup(g => g.Draw(guiInfo));

				// draw window connectors + window buttons
				nodeCanvas.ForeachNode(DrawNodeCurves);
				nodeCanvas.ForeachNode(DrawNodeKnobs);


				InputEvents();

				// Draw nodes
				BeginWindows();
				for (int i = 0; i < nodeCanvas.GetNodesLength(); i++) {
					var node = nodeCanvas.GetNode(i);
					if (!node.CalculateVisibility(position.size / nodeCanvas.zoom.GetZoom())) {
						GUILayout.Window(i, node.rectPixelCorrected, DrawNode, node.GetName(), GUI_Info.nodeStyle);
					}
					else {
						Color originalColor = GUI.backgroundColor;
						GUI.backgroundColor = nodeCanvas.GetFirstNode() == node ? firstNodeColor : nodeCanvas.StackContainsNode(node) ? nodeInStackColor : node == selectedNode ? selectedNodeColor : nonExecutedColor;
						GUI.backgroundColor = Color.Lerp(executingNodeColor, GUI.backgroundColor, (Time.time - node.lastExecutedTime) / decayInSeconds);
						GUI.color = Color.white;
						Rect startRect = node.positionSize;
						Rect r = GUILayout.Window(i, node.rectPixelCorrected, DrawNode, node.GetName(), GUI_Info.nodeStyle);
						if (!nodeCanvas.zoom.IsZooming) {
							node.SetCanvasPosition(r.position, guiInfo);
							if (r.size != Vector2.zero) node.positionSize.size = r.size;
							if (startRect != node.positionSize) {
								nodeCanvas.ForeachGroup(g => g.needsUpdate = true);
							}
						}
						GUI.backgroundColor = originalColor;
					}
				}
				EndWindows();

				// Draw gui order debug
				if (nodeCanvas.debug) {
					Color c = GUI.color;
					if (nodeCanvas.stepByStep && nodeCanvas.calledElementsInOrder.Count > 0 && nodeCanvas.calledElementsInOrder[curDebugStep].sender != null) {
						Vector2 pos = nodeCanvas.calledElementsInOrder[curDebugStep].sender.GetGUIPosForOrderGraph(debugBoxSize, guiInfo);
						GUI.color = Color.red;
						GUI.Box(new Rect(pos, debugBoxSize), nodeCanvas.calledElementsInOrder[curDebugStep].text);
					}
					GUI.color = c;
				}

				Repaint();

				// undo zoom
				GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one);


				GUI.EndGroup();

				if (selectedNode != null) {
					selectedNode.OnSelectedDrawCanvas(guiInfo);
				}

				// Now continue drawing using the new clipping group
				GUI.BeginGroup(new Rect(0, 23, position.width, position.height));


				// Now continue drawing using the new clipping group
				GUILayout.BeginArea(sideWindowRect, GUI_Info.nodeBox);
				DrawSideWindow();
				GUILayout.EndArea();

				if (Event.current.type == EventType.Repaint) {
					ProcessDeferredNodesToDelete();
					ProcessDeferredGroupsToDelete();
				}
			} catch (Exception e) {
				Debug.LogError(e, this);
			}
			UndoEditorColor();

			nodeCanvas.zoom.GUIUpdate(this);
		}


		public void DrawNodeKnobs(Node node) {
			Color c = GUI.color;

			// NodeIO knobs
			foreach (var input in node.GetInputs()) {
				input.DrawKnob(guiInfo);
				GUI.color = c;
			}
			foreach (var output in node.GetOutputs()) {
				output.DrawKnob(guiInfo);
				GUI.color = c;
			}

			foreach (var input in node.GetInputs()) {
				GUI.color = c;
				
				if (input != null && input.nodeOutput != null) {
					var val = input.nodeOutput != null ? input.nodeOutput.GetValueAsObject() : input.GetValueAsObject();
					if (val != null) {
						var to = input.GetKnobRect(guiInfo);
						var content = new GUIContent(val.ToString());
						Vector2 size = GUI.skin.box.CalcSize(content);
						Rect r = new Rect(to.center - new Vector2(size.x + knobSize * 0.5f, size.y * 0.5f), size);
						GUI.color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
						GUI.Box(r, content, GUI_Info.variablePreview);
						GUI.color = c;
					}
				}
			}

			// Tree knobs
			foreach (var treeOutput in node.GetTreeOutputs()) {
				treeOutput.DrawKnob(guiInfo);
				GUI.color = c;
			}
		}

		public Rect GetInputTreeNodeKnob(Node node) {
			//return new Rect(node.canvasPositionPixelCorrected + new Vector2((node.positionSize.width - knobSize) / 2, -knobSize), new Vector2(knobSize, knobSize));
			return new Rect(node.canvasPositionPixelCorrected + new Vector2(node.positionSize.width / 2, 0), new Vector2(0, 0));
		}

		public void DrawNodeCurves(Node node) {
			Color c = GUI.color;

			// NodeIO
			foreach (var output in node.GetOutputs()) {
				foreach (var input in output.GetInputs()) {
					Rect from = output.GetKnobRect(guiInfo);
					Rect to = input.GetKnobRect(guiInfo);
					Color shadow = output.GetColor();
					shadow.a = 0.4f;
					DrawNodeCurve(from.center, to.center, output.GetColor(), (Time.time - input.lastExecutedTime) / decayInSeconds);
				}
			}

			// Tree
			GUIStyle styleCloseButton = EditorStyles.miniButton;
			styleCloseButton.richText = true;
			styleCloseButton.padding = new RectOffset(0, 0, 0, 0);
			styleCloseButton.margin = new RectOffset(0, 0, 0, 0);
			styleCloseButton.alignment = TextAnchor.MiddleCenter;
			foreach (var treeOutput in node.GetTreeOutputs()) {
				var input = treeOutput.outputNode;
				if (input == null) continue;

				Rect from = treeOutput.GetKnobRect(guiInfo);
				Rect to = GetInputTreeNodeKnob(input);

				Color shadow = NodeTreeOutput.GetColor();
				shadow.a = 0.7f;
				DrawNodeCurve(from.center, to.center, NodeTreeOutput.GetColor(), (Time.time - treeOutput.lastExecutedTime) / decayInSeconds, TangetType.Vertical);

				GUI.color = NodeTreeOutput.GetColor();
				if (GUI.Button(new Rect(from.center + (to.center - from.center) / 2 - new Vector2(8, 8), new Vector2(16, 16)), "<color=white>X</color>", styleCloseButton)) {
					treeOutput.Set(null);
				}
				GUI.color = c;
			}
			GUI.color = c;
		}

		Stack<Node> nodesToDelete = new Stack<Node>();
		void DeleteNodeDeferred(Node node) {
			nodesToDelete.Push(node);
		}
		void ProcessDeferredNodesToDelete() {
			while (nodesToDelete.Count > 0) {
				var node = nodesToDelete.Pop();
				nodeCanvas.ForeachGroup(group => {
					if (group.ContainsNode(node)) group.needsUpdate = true;
				});
				node.OnDelete();
			}
		}
		Stack<Group> groupsToDelete = new Stack<Group>();
		void DeleteGroupDeferred(Group node) {
			groupsToDelete.Push(node);
		}
		void ProcessDeferredGroupsToDelete() {
			while (groupsToDelete.Count > 0) {
				groupsToDelete.Pop().OnDelete();
			}
		}

		public void DrawSideWindow() {
			EditorGUIUtility.labelWidth = 80;
			EditorGUIUtility.fieldWidth = 75;
			GUI.color = Color.white;
			GUI.contentColor = Color.white;

			var newNodeCanvas = (Context) EditorGUILayout.ObjectField(nodeCanvas, typeof(Context), true);
			if (newNodeCanvas != nodeCanvas) {
				nodeCanvas = newNodeCanvas;
				Repaint();
			}
			GUILayout.Label(new GUIContent("Changes made during gameplay are lost after playing!!!!"), GUI_Info.nodeBase);

			this.LayoutPropertyField("selectedNode");

			/*
			firstNodeColor = EditorGUILayout.ColorField(new GUIContent("firstNodeColor"), firstNodeColor);
			nodeInStackColor = EditorGUILayout.ColorField(new GUIContent("executingNodeColor"), nodeInStackColor);
			executingNodeColor = EditorGUILayout.ColorField(new GUIContent("executedNodeColor"), executingNodeColor);
			nonExecutedColor = EditorGUILayout.ColorField(new GUIContent("nonExecutedColor"), nonExecutedColor);
			*/

			EDIT_GROUPS = EditorGUILayout.Toggle(new GUIContent("Edit Groups"), EDIT_GROUPS);

			if (GUILayout.Button(new GUIContent("Go To First Node", "Are you lost? Teleport yourself to the first Node!"), GUI_Info.nodeButton)) {
				GotoFirstNode();
			}

			if (nodeCanvas.debug = EditorGUILayout.BeginToggleGroup(new GUIContent("Debug mode", "Check the time each node needs to do its thing"), nodeCanvas.debug)) {
				nodeCanvas.stepByStep = EditorGUILayout.Toggle(new GUIContent("Step by step", "Check the time each node needs to do its thing"), nodeCanvas.stepByStep);

				int startCurDebugStep = curDebugStep;

				GUILayout.BeginHorizontal();
				if (GUILayout.Button("-")) curDebugStep--;
				if (GUILayout.Button("+")) curDebugStep++;
				GUILayout.EndHorizontal();
				if (!nodeCanvas.stepByStep) curDebugStep = 0;
				curDebugStep = EditorGUILayout.IntSlider(curDebugStep, 0, nodeCanvas.calledElementsInOrder.Count - 1);
				
				if (nodeCanvas.calledElementsInOrder.Count > 0 && startCurDebugStep != curDebugStep) {
					GoToOffset(nodeCanvas.calledElementsInOrder[curDebugStep].sender.GetGUIPosForOrderGraph(debugBoxSize, guiInfo) + debugBoxSize / 2 - nodeCanvas.scrollOffset);
				}

				if (GUILayout.Button(new GUIContent("Pass GC", "Force a Garbage Collector Pass"), GUI_Info.nodeButton)) {
					GC.Collect();
					GC.WaitForPendingFinalizers();
				}
			}
			EditorGUILayout.EndToggleGroup();

			if (selectedNode != null) {
				GUILayout.Space(20);
				GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);
				GUI.color = new Color(1, 1, 1);
				GUILayout.BeginVertical(new GUIContent(selectedNode.GetName()),  GUI_Info.nodeStyle);
				GUI.color = Color.white;
				selectedNode.isNodeSelected = true;
				selectedNode.DrawInternalInspectorNode();
				GUILayout.EndVertical();
			}
		}
		#endregion

		#region Events

		Group movingGroup;

		/// <summary>
		/// Processes input events
		/// </summary>
		private void InputEvents() {
			Event e = Event.current;
			mousePos = e.mousePosition;

			Node clickedNode = null;
			Group clickedGroup = null;
			NodeOutput clickedOutput = null;
			NodeInput clickedInput = null;
			NodeTreeOutput clickedTreeOutput = null;
			if (e.type == EventType.MouseDown || e.type == EventType.MouseUp) {
				clickedNode = NodeAtPosition(mousePos);
				clickedGroup = EDIT_GROUPS ? GroupAtPosition(mousePos) : null;
				if (clickedNode != null) {
					clickedOutput = clickedNode.GetOutputAtPos(mousePos, guiInfo);
					clickedInput = clickedNode.GetInputAtPos(mousePos, guiInfo);
					clickedTreeOutput = clickedNode.GetTreeOutputAtPos(mousePos, guiInfo);
				}
			}

			if (e.type == EventType.MouseDown) {
				if (e.button == 2) { // Init scroll
					scrollWindow = true;
					e.delta = new Vector2(0, 0);
					e.Use();
				} else if (e.button == 1) { // Right click -> Editor Context Click
					if (clickedOutput != null) {
						connectInput = new List<NodeInput>(clickedOutput.GetInputs()).ToArray();
						foreach (var i in connectInput) {
							clickedOutput.RemoveNodeInput(i);
						}
					} else {
						GenericMenu menu = new GenericMenu();

						if (clickedNode != null) {
							menu.AddItem(new GUIContent("Set as Start Node"), false, () => { nodeCanvas.SetFirstNode(clickedNode); });
							menu.AddSeparator("");
							nodeCanvas.ForeachGroup(group => {
								if (!group.ContainsNode(clickedNode)) {
									menu.AddItem(new GUIContent("Add to Group/" + group.GetName()), false, () => { group.AddNode(clickedNode); group.needsUpdate = true; });
								}
							});
							menu.AddItem(new GUIContent("Add to Group/New Group"), false, () => { AddGroup().AddNode(clickedNode); });
							nodeCanvas.ForeachGroup(group => {
								if (group.ContainsNode(clickedNode)) {
									menu.AddItem(new GUIContent("Remove from Group/" + group.GetName()), false, () => { group.RemoveNode(clickedNode); group.needsUpdate = true; });
								}
							});
							menu.AddSeparator("");
							menu.AddItem(new GUIContent("Rename Node"), false, () => { WindowChangeName.AskName(clickedNode, mousePos + position.position); });
							menu.AddItem(new GUIContent("Delete Node"), false, () => {
								if (EditorUtility.DisplayDialog("¿Delete, are you sure?", "¿Do you really want to delete this node?", "Delete it", "Nope, Don't touch it")) {
									DeleteNodeDeferred(clickedNode);
								}
							});
						} else if (clickedGroup != null) {
							menu.AddItem(new GUIContent("Rename Group"), false, () => { WindowChangeName.AskName(clickedGroup, mousePos + position.position); });
							menu.AddItem(new GUIContent("Remove Group"), false, () => {
								// ok, alt, nope
								int response = EditorUtility.DisplayDialogComplex("¿Delete, are you sure?", "¿Do you really want to delete this group?", "Delete it", "Nope, Don't touch it", "Delete it + nodes");
								if (response == 2) {
									foreach (var node in clickedGroup.GetNodes()) {
										DeleteNodeDeferred(node);
									}
									DeleteGroupDeferred(clickedGroup);
								}
								if (response == 0) {
									DeleteGroupDeferred(clickedGroup);
								}
							});
						} else {
							foreach (var elem in CreateNodeAttribute.Cached) {
								menu.AddItem(new GUIContent(elem.Value.name, elem.Value.description), false, MenuNodeContextCallback, elem.Key);
							}
						}
						menu.ShowAsContext();
					}

					e.Use();
				} else if (e.button == 0) {
					scrollWindow = false;
					e.delta = new Vector2(0, 0);
					// If a Connection was left clicked, try edit it's transition
					if (clickedNode != null) {
						if (clickedOutput != null) {
							if (connectInput.Length > 0) {
								bool connectedOk = true;
								foreach (var i in connectInput) {
									if (!clickedNode.TryApplyConnection(clickedOutput, i)) {
										connectedOk = false;
										break;
									}
								}
								if (connectedOk) {
									connectInput = new NodeInput[0];
								}
							} else {
								connectOutput = clickedOutput;
							}
							e.Use();
						} else if (clickedTreeOutput != null) {
							connectTreeOutput = clickedTreeOutput;
							connectOutput = null;
							e.Use();
						} else if (connectOutput != null) {
							if (!ArrayUtility.Contains((NodeOutput[]) clickedNode.GetOutputs(), connectOutput)) { // If an input was clicked, it'll will now be connected
								clickedNode.TryApplyConnection(connectOutput, clickedInput);
								if (!e.control && !e.shift)
									connectOutput = null;
							} else {
								connectOutput = null;
							}
							e.Use();
						} else if (connectTreeOutput != null) {
							if (clickedNode != connectTreeOutput.GetBody()) {
								if (clickedNode != null) {
									connectTreeOutput.Set(clickedNode);
								}
							}
							connectTreeOutput = null;
							e.Use();
						} else if (clickedInput != null && clickedInput.nodeOutput != null) { // Input node -> Loose and edit Connection
							connectOutput = clickedInput.nodeOutput;
							connectOutput.RemoveNodeInput(clickedInput);
							e.Use();
						} else {
							if (selectedNode != null) selectedNode.isNodeSelected = false;
							selectedNode = clickedNode;
						}
					} else if (clickedGroup != null) {
						movingGroup = clickedGroup;
					} else {
						selectedNode = null;
						if (connectOutput != null) {
							connectOutput = null;
							e.Use();
						} else if (connectTreeOutput != null) {
							connectTreeOutput = null;
							e.Use();
						} else if (clickedGroup == null) { // Init scroll
							scrollWindow = true;
							e.delta = new Vector2(0, 0);
							e.Use();
						}
					}
				}
			} else if (e.type == EventType.MouseUp) {
				if (movingGroup != null) {
					movingGroup = null;
				} else if (e.button == 0 && connectOutput != null) { // Apply a connection if theres a clicked input
					if (clickedNode == null) {
						connectOutput = null;
						e.Use();
					} else if (!ArrayUtility.Contains((NodeOutput[]) clickedNode.GetOutputs(), connectOutput)) { // If an input was clicked, it'll will now be connected
						clickedNode.TryApplyConnection(connectOutput, clickedInput);
						if (!e.control && !e.shift)
							connectOutput = null;
						e.Use();
					}
				} else if (e.button == 0 && connectTreeOutput != null) {
					if (clickedNode != connectTreeOutput.GetBody()) {
						if (clickedNode != null) {
							connectTreeOutput.Set(clickedNode);
						}
						connectTreeOutput = null;
						e.Use();
					}
				} else if (e.button == 2 || e.button == 0) { // Left/Middle click up -> Stop scrolling
					scrollWindow = false;
				}
			} else if (e.type == EventType.Repaint) {
				// Draw the currently drawn connection
				if (connectOutput != null) {
					DrawNodeCurve(connectOutput.GetKnobRect(guiInfo).center, mousePos, connectOutput.GetColor());
					Repaint();
				} else if (connectTreeOutput != null) {
					DrawNodeCurve(connectTreeOutput.GetKnobRect(guiInfo).center, mousePos, NodeTreeOutput.GetColor(), TangetType.Vertical);
					Repaint();
				} else if (connectInput.Length > 0) {
					foreach (var i in connectInput) {
						DrawNodeCurve(mousePos, i.GetKnobRect(guiInfo).center, i.GetColor());
					}
					Repaint();
				}
			}

			if (e.type == EventType.ScrollWheel) {
				float multiplier = e.delta.y < 0 ? 2 : e.delta.y > 0 ? 0.5f : 0;

				if (multiplier != 0) {
					nodeCanvas.zoom.DoZoom(multiplier);
					/*
					float diff = nodeCanvas.zoom.zoom - initialZoom;

					if (diff > 0) {
						nodeCanvas.scrollOffset -= (e.mousePosition * initialZoom) / nodeCanvas.zoom.zoom;
					} else if (diff < 0) {
						nodeCanvas.scrollOffset += (e.mousePosition * targetZoom) / nodeCanvas.zoom.zoom;
					}
					*/
				}

				e.Use();
			}

			if (scrollWindow) {
				nodeCanvas.scrollOffset += e.delta / 2;
			} else if (movingGroup) {
				movingGroup.AddDelta(e.delta, guiInfo);
			} else {
				nodeCanvas.scrollOffset = new Vector2((int) nodeCanvas.scrollOffset.x, (int) nodeCanvas.scrollOffset.y);
			}
		}

		public Group AddGroup() {
			var group = Undo.AddComponent<Group>(nodeCanvas.GroupsGO.gameObject);
			group.Init(nodeCanvas);
			return group;
		}

		/// <summary>
		/// Teleport the node view to the first Node
		/// </summary>
		public void GotoFirstNode() {
			if (nodeCanvas.GetNodesLength() == 0)
				GoToOffset(Vector2.zero);
			else
				GoToOffset(nodeCanvas.GetNode(0).positionSize.position + nodeCanvas.GetNode(0).positionSize.size / 2);
		}

		public void GoToOffset(Vector2 offset) {
			nodeCanvas.scrollOffset = -offset + (position.size - new Vector2(sideWindowRect.width, 0)) / 2;
			Repaint();
		}

		/// <summary>
		/// Context Click selection. Here you'll need to register your own using a string identifier
		/// </summary>
		public void MenuNodeContextCallback(object obj) {
			Node node = Undo.AddComponent(nodeCanvas.NodesGO.gameObject, (Type) obj) as Node;
			node.Init(nodeCanvas, mousePos - nodeCanvas.scrollOffset);
		}

		#endregion

		#region GUI Functions

		public Rect sideWindowRect {
			get { return new Rect(position.width - sideWindowWidth, 0, sideWindowWidth, position.height); }
		}

		/// <summary>
		/// Create a 1x1 tex with color col
		/// </summary>
		public static Texture2D ColorToTex(Color inside) {
			Texture2D tex = new Texture2D(1, 1, TextureFormat.ARGB32, false, true);
			tex.SetPixel(0, 0, inside);
			tex.Apply();
			return tex;
		}

		/// <summary>
		/// Create a 1x1 tex with color col
		/// </summary>
		public static Texture2D ColorToTex(Color border, Color inside) {
			Texture2D tex = new Texture2D(3, 3, TextureFormat.ARGB32, false, true) {
				filterMode = FilterMode.Point
			};
			tex.SetPixel(0, 0, border); tex.SetPixel(1, 0, border); tex.SetPixel(2, 0, border);
			tex.SetPixel(0, 1, border); tex.SetPixel(1, 1, inside); tex.SetPixel(2, 1, border);
			tex.SetPixel(0, 2, border); tex.SetPixel(1, 2, border); tex.SetPixel(2, 2, border);
			tex.Apply();
			return tex;
		}

		/// <summary>
		/// Returns the node at the position
		/// </summary>
		public Node NodeAtPosition(Vector2 pos) {
			if (sideWindowRect.Contains(pos))
				return null;

			// Check if we clicked inside a window (or knobSize pixels left or right of it at outputs, for easier knob recognition)
			for (int i = 0; i < nodeCanvas.GetNodesLength(); i++) { // From top to bottom because of the render order (though overwritten by active Window, so be aware!)
				var node = nodeCanvas.GetNode(i);
				Rect NodeRect = new Rect(node.canvasPosition.x - knobSize, node.canvasPosition.y, node.positionSize.width + knobSize * 2, node.positionSize.height + knobSize);
				if (NodeRect.Contains(pos)) return node;
			}
			return null;
		}

		/// <summary>
		/// Returns the node at the position
		/// </summary>
		public Group GroupAtPosition(Vector2 pos) {
			if (sideWindowRect.Contains(pos))
				return null;

			// Check if we clicked inside a window (or knobSize pixels left or right of it at outputs, for easier knob recognition)
			for (int i = 0; i < nodeCanvas.GetGroupsLength(); i++) { // From top to bottom because of the render order (though overwritten by active Window, so be aware!)
				var group = nodeCanvas.GetGroup(i);
				Rect NodeRect = new Rect(group.CanvasPosition.x - groupBorder, group.CanvasPosition.y - groupBorder, group.PositionSize.width + groupBorder * 2, group.PositionSize.height + groupBorder * 2);
				if (NodeRect.Contains(pos)) return group;
			}
			return null;
		}

		/// <summary>
		/// Draws the node
		/// </summary>
		void DrawNode(int id) {
			nodeCanvas.GetNode(id).DrawNode();
			GUI.DragWindow();
		}

		public enum TangetType { Horizontal, Vertical }

		public static void DrawNodeCurve(Vector2 start, Vector2 end, Color lineColor, TangetType tangent = TangetType.Horizontal) {
			DrawNodeCurve(start, end, lineColor, 1, tangent);
		}
		
		public static void DrawNodeCurve(Vector2 start, Vector2 end, Color lineColor, float relativeSize, TangetType tangent = TangetType.Horizontal) {
			Vector3 startPos = new Vector3(start.x, start.y);
			Vector3 endPos = new Vector3(end.x, end.y);
			Vector3 startTan = startPos;
			Vector3 endTan = endPos;

			if (tangent == TangetType.Horizontal) {
				start.y *= 0.2f;
				end.y *= 0.2f;
			} else {
				start.x *= 0.2f;
				end.x *= 0.2f;
			}

			
			if (tangent == TangetType.Horizontal) {
				float hDist = Mathf.Abs(end.x - start.x) * 0.75f;
				if (endPos.x - startPos.x < 0)
					hDist /= Mathf.Sqrt(Mathf.Max(1, hDist * 0.01f));
				startTan += Vector3.right * hDist;
				endTan += Vector3.left * hDist;
			} else {
				float hDist = Mathf.Abs(end.y - start.y) * 0.75f;
				if (endPos.y - startPos.y < 0)
					hDist /= Mathf.Sqrt(Mathf.Max(1, hDist * 0.01f));
				startTan += Vector3.up * hDist;
				endTan += Vector3.down * hDist;
			}
			
			Handles.DrawBezier(startPos, endPos, startTan, endTan, lineColor, null, Mathf.Lerp(10, 2, relativeSize));
		}

		#endregion
	}
}
