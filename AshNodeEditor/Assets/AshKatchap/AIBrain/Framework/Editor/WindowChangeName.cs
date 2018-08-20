using UnityEngine;
using UnityEditor;

namespace Ashkatchap.AIBrain {
	public class WindowChangeName : EditorWindow {
		string newName;
		IChangeableName node;

		public static void AskName(IChangeableName changeableName, Vector2 windowCenter) {
			WindowChangeName window = CreateInstance<WindowChangeName>();
			window.titleContent.text = "Change name";
			window.minSize = window.maxSize = new Vector2(350, 45);
			window.node = changeableName;
			window.newName = changeableName.GetName();
			window.ShowUtility();
			window.position = new Rect(windowCenter - window.maxSize / 2, window.maxSize);
		}

		void OnGUI() {
			GUI.SetNextControlName("Name");
			newName = EditorGUILayout.TextField("Name", newName);

			EditorGUI.FocusTextInControl("Name");

			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Set Name") || Event.current.isKey && Event.current.keyCode == KeyCode.Return) {
				node.SetName(newName);
				Close();
			}
			if (GUILayout.Button("Cancel")) {
				Close();
			}
			GUILayout.EndHorizontal();
		}
	}
}
