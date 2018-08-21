using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	public class GeneratorWindow : EditorWindow {
		[MenuItem("Window/Ashkatchap/AIBrain/Reflection Node Generator")]
		static void Init() {
			GeneratorWindow window = GetWindow<GeneratorWindow>();
			window.Show();
		}

		HashSet<MemberInfo> WantedMembers = new HashSet<MemberInfo>();
		HashSet<Type> WantedStructs = new HashSet<Type>();

		Type clickedType;
		Vector2 scroll;
		string search = "";

		MethodInfo[] currentTypeMethods;
		MemberInfo[] currentTypePropertiesAndFields;

		int step = 1;

		// Do something similar for structs and enums like Vector3
		void OnGUI() {
			var toggleStyle = EditorStyles.toggle;
			toggleStyle.richText = true;
			var buttonStyle = GUI.skin.button;
			buttonStyle.richText = true;
			buttonStyle.alignment = TextAnchor.MiddleLeft;



			if (CheckStep("Step 1: Select Nodes to create", 1)) {
				string searchLower = search.ToLower();
				
				int matches = TypeFinder.Types.Where(t => search == "" || t.FullName.ToLower().Contains(searchLower)).Count();

				search = EditorGUILayout.TextField("Search (Matches: " + matches + ")", search);
				searchLower = search.ToLower();

				scroll = GUILayout.BeginScrollView(scroll, false, true);
				foreach (var type in TypeFinder.Types) {
					if (search == "" || type.FullName.ToLower().Contains(searchLower)) {
						if (GUILayoutCull.Button(position, scroll, type.FullName, buttonStyle)) {
							clickedType = clickedType == type ? null : type;

							if (clickedType == type) {
								currentTypeMethods = TypeFinder.GetMethods(type);
								currentTypePropertiesAndFields = TypeFinder.GetPropertiesAndFields(type);
							}
						}
						if (clickedType == type) {
							GUILayout.Label("Struct/class", EditorStyles.boldLabel);
							bool inside = WantedStructs.Contains(type);
							if (inside != GUILayout.Toggle(inside, type.Name, toggleStyle)) {
								if (inside) WantedStructs.Remove(type);
								else WantedStructs.Add(type);
							}
							/*
							GUILayout.Label("Constructors", EditorStyles.boldLabel);
							// TO DO
							*/
							GUILayout.Label("Methods", EditorStyles.boldLabel);
							foreach (var elem in currentTypeMethods) {
								inside = WantedMembers.Contains(elem);
								if (inside != GUILayout.Toggle(inside, TypeFinder.GetDisplayName(elem, true, true), toggleStyle)) {
									if (inside) WantedMembers.Remove(elem);
									else WantedMembers.Add(elem);
								}
							}

							GUILayout.Label("Properties and Fields", EditorStyles.boldLabel);
							foreach (var elem in currentTypePropertiesAndFields) {
								inside = WantedMembers.Contains(elem);
								if (inside != GUILayout.Toggle(inside, TypeFinder.GetDisplayName(elem, true, true), toggleStyle)) {
									if (inside) WantedMembers.Remove(elem);
									else WantedMembers.Add(elem);
								}
							}
						}
					}
				}
				GUILayout.EndScrollView();
			}
			EditorGUILayout.EndToggleGroup();

			if (CheckStep("Step 2: Generate files", 2)) {
				scroll = GUILayout.BeginScrollView(scroll, false, true);
				GUILayout.Label("Structs", EditorStyles.boldLabel);
				foreach (var elem in WantedStructs) {
					if (!GUILayout.Toggle(true, elem.FullName + " - " + elem.FullName, toggleStyle)) {
						WantedStructs.Remove(elem);
						break;
					}
				}

				GUILayout.Label("Methods, Properties and Fields", EditorStyles.boldLabel);
				foreach (var elem in WantedMembers) {
					if (!GUILayout.Toggle(true, elem.DeclaringType.FullName + " - " + TypeFinder.GetDisplayName(elem, true, true), toggleStyle)) {
						WantedMembers.Remove(elem);
						break;
					}
				}
				GUILayout.EndScrollView();

				if (GUILayout.Button("Generate Files")) {
					var fullPath = GetFullPath();

					foreach (var elem in WantedMembers) {
						string filename;
						string textFile = TemplateNode.GenerateFile(elem, out filename);
						WriteFile(fullPath, "Nodes", filename, textFile);
					}

					foreach (var elem in WantedStructs) {
						string filename;
						string textFile = TemplateIO.GenerateFileInput(elem, out filename);
						WriteFile(fullPath, "IO", filename, textFile);
						textFile = TemplateIO.GenerateFileOutput(elem, out filename);
						WriteFile(fullPath, "IO", filename, textFile);
						textFile = TemplateIO.GenerateValueFile(elem, out filename);
						WriteFile(fullPath, "Values", filename, textFile);
					}
					
					AssetDatabase.Refresh();
				}
			}
			EditorGUILayout.EndToggleGroup();
		}

		string GetFullPath() {
			var script = MonoScript.FromScriptableObject(this);
			var path = AssetDatabase.GetAssetPath(script);
			var pathList = path.Split('/').ToList();
			pathList.RemoveAt(0);
			pathList.RemoveAt(pathList.Count - 1);
			pathList.RemoveAt(pathList.Count - 1);
			pathList.Add("Generated/");
			path = "/" + string.Join("/", pathList.ToArray());
			Debug.Log(path);
			return Application.dataPath + path;
		}

		void WriteFile(string fullPath, string folder, string filename, string textFile) {
			Debug.Log(textFile);
			if (Directory.Exists(fullPath)) Directory.CreateDirectory(fullPath);
			if (Directory.Exists(fullPath + folder)) Directory.CreateDirectory(fullPath + folder);
			File.WriteAllText(fullPath + folder + "/" + filename, textFile);
		}

		bool CheckStep(string txt, int newStep) {
			return (step = EditorGUILayout.BeginToggleGroup(txt, step == newStep) ? newStep : step) == newStep;
		}
	}
}

