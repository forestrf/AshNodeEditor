using UnityEngine;

public static class GUILayoutCull {
	public static bool Button(Rect position, Vector2 scroll, string text, GUIStyle style) {
		var rect = GUILayoutUtility.GetRect(position.width - 20, 20);
		if (rect.y + rect.height >= scroll.y && rect.y < position.height + scroll.y) {
			return GUI.Button(rect, text, style);
		}
		else {
			return false;
		}
	}
}
