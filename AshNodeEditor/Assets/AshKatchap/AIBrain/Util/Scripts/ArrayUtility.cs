using System.Collections.Generic;

public static class ArrayUtility {
	public static int IndexOf<T>(this T[] array, T value) {
		if (array == null) return -1;
		List<T> list = new List<T>(array);
		return list.IndexOf(value);
	}
	
	public static bool Contains<T>(this T[] array, T item) {
		if (array == null) return false;
		List<T> list = new List<T>(array);
		return list.Contains(item);
	}
	
	public static T[] GetCopy<T>(this T[] array) {
		T[] copy = new T[array.Length];
		array.CopyTo(copy, 0);
		return copy;
	}
}
