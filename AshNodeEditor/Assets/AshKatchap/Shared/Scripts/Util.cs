using System;
using UnityEngine;

public static class Util {
	public static Vector3 Round(this Vector3 v, int decimals) {
		v.x = (float) Math.Round(v.x, decimals);
		v.y = (float) Math.Round(v.y, decimals);
		v.z = (float) Math.Round(v.z, decimals);
		return v;
	}

	public static Vector3 Abs(this Vector3 v) {
		if (v.x < 0) v.x = -v.x;
		if (v.y < 0) v.y = -v.y;
		if (v.z < 0) v.z = -v.z;
		return v;
	}
	public static Vector3 AbsX(this Vector3 v) {
		if (v.x < 0) v.x = -v.x;
		return v;
	}
	public static Vector3 AbsY(this Vector3 v) {
		if (v.y < 0) v.y = -v.y;
		return v;
	}
	public static Vector3 AbsZ(this Vector3 v) {
		if (v.z < 0) v.z = -v.z;
		return v;
	}
	public static Vector3 AbsXY(this Vector3 v) {
		if (v.x < 0) v.x = -v.x;
		if (v.y < 0) v.y = -v.y;
		return v;
	}
	public static Vector3 AbsXZ(this Vector3 v) {
		if (v.x < 0) v.x = -v.x;
		if (v.z < 0) v.z = -v.z;
		return v;
	}
	public static Vector3 AbsYZ(this Vector3 v) {
		if (v.y < 0) v.y = -v.y;
		if (v.z < 0) v.z = -v.z;
		return v;
	}

	public static Vector2 Abs(this Vector2 v) {
		if (v.x < 0) v.x = -v.x;
		if (v.y < 0) v.y = -v.y;
		return v;
	}

	public static float Abs(this float f) {
		return f < 0 ? -f : f;
	}

	public static Vector2 AbsX(this Vector2 v) {
		if (v.x < 0) v.x = -v.x;
		return v;
	}
	public static Vector2 AbsY(this Vector2 v) {
		if (v.y < 0) v.y = -v.y;
		return v;
	}

	public static Vector3 XZY(this Vector3 v) { return new Vector3(v.x, v.z, v.y); }
	public static Vector3 YXZ(this Vector3 v) { return new Vector3(v.y, v.x, v.z); }
	public static Vector3 YZX(this Vector3 v) { return new Vector3(v.y, v.z, v.x); }
	public static Vector3 ZXY(this Vector3 v) { return new Vector3(v.z, v.x, v.y); }
	public static Vector3 ZYX(this Vector3 v) { return new Vector3(v.z, v.y, v.x); }
	public static Vector2 XY(this Vector3 v) { return new Vector2(v.x, v.y); }
	public static Vector2 XZ(this Vector3 v) { return new Vector2(v.x, v.z); }
	public static Vector2 YX(this Vector3 v) { return new Vector2(v.y, v.x); }
	public static Vector2 YZ(this Vector3 v) { return new Vector2(v.y, v.z); }
	public static Vector2 ZX(this Vector3 v) { return new Vector2(v.z, v.x); }
	public static Vector2 ZY(this Vector3 v) { return new Vector2(v.z, v.y); }
	
	public static Vector2 YX(this Vector2 v) { return new Vector2(v.y, v.x); }


	public static Vector3 XY_(this Vector3 v, float f) { return new Vector3(v.x, v.y, f); }
	public static Vector3 XZ_(this Vector3 v, float f) { return new Vector3(v.x, v.z, f); }
	public static Vector3 YX_(this Vector3 v, float f) { return new Vector3(v.y, v.x, f); }
	public static Vector3 YZ_(this Vector3 v, float f) { return new Vector3(v.y, v.z, f); }
	public static Vector3 ZX_(this Vector3 v, float f) { return new Vector3(v.z, v.x, f); }
	public static Vector3 ZY_(this Vector3 v, float f) { return new Vector3(v.z, v.y, f); }
	public static Vector3 X_Y(this Vector3 v, float f) { return new Vector3(v.x, f, v.y); }
	public static Vector3 X_Z(this Vector3 v, float f) { return new Vector3(v.x, f, v.z); }
	public static Vector3 Y_X(this Vector3 v, float f) { return new Vector3(v.y, f, v.x); }
	public static Vector3 Y_Z(this Vector3 v, float f) { return new Vector3(v.y, f, v.z); }
	public static Vector3 Z_X(this Vector3 v, float f) { return new Vector3(v.z, f, v.x); }
	public static Vector3 Z_Y(this Vector3 v, float f) { return new Vector3(v.z, f, v.y); }
	public static Vector3 _XY(this Vector3 v, float f) { return new Vector3(f, v.x, v.y); }
	public static Vector3 _XZ(this Vector3 v, float f) { return new Vector3(f, v.x, v.z); }
	public static Vector3 _YX(this Vector3 v, float f) { return new Vector3(f, v.y, v.x); }
	public static Vector3 _YZ(this Vector3 v, float f) { return new Vector3(f, v.y, v.z); }
	public static Vector3 _ZX(this Vector3 v, float f) { return new Vector3(f, v.z, v.x); }
	public static Vector3 _ZY(this Vector3 v, float f) { return new Vector3(f, v.z, v.y); }

	public static Vector3 XY_(this Vector2 v, float f) { return new Vector3(v.x, v.y, f); }
	public static Vector3 YX_(this Vector2 v, float f) { return new Vector3(v.y, v.x, f); }
	public static Vector3 X_Y(this Vector2 v, float f) { return new Vector3(v.x, f, v.y); }
	public static Vector3 Y_X(this Vector2 v, float f) { return new Vector3(v.y, f, v.x); }
	public static Vector3 _XY(this Vector2 v, float f) { return new Vector3(f, v.x, v.y); }
	public static Vector3 _YX(this Vector2 v, float f) { return new Vector3(f, v.y, v.x); }

	public static Vector2 Rotate(this Vector2 v, float degrees) {
		float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
		float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

		float tx = v.x;
		float ty = v.y;
		v.x = (cos * tx) - (sin * ty);
		v.y = (sin * tx) + (cos * ty);
		return v;
	}

	public static Vector3 Add(this Vector3 a, float b) {
		a.x += b;
		a.y += b;
		a.z += b;
		return a;
	}
	public static Vector2 Add(this Vector2 a, float b) {
		a.x += b;
		a.y += b;
		return a;
	}

	public static Vector2 Divide(this Vector2 a, Vector2 by) {
		by.x = a.x / by.x;
		by.y = a.y / by.y;
		return by;
	}
	public static Vector3 Divide(this Vector3 a, Vector3 by) {
		by.x = a.x / by.x;
		by.y = a.y / by.y;
		by.z = a.z / by.z;
		return by;
	}

	public static Vector2 Multiply(this Vector2 a, Vector2 by) {
		by.x = a.x * by.x;
		by.y = a.y * by.y;
		return by;
	}
	public static Vector3 Multiply(this Vector3 a, Vector3 by) {
		by.x = a.x * by.x;
		by.y = a.y * by.y;
		by.z = a.z * by.z;
		return by;
	}
	public static Vector3 Multiply(this Vector3 a, float x, float y, float z) {
		Vector3 by = new Vector3(x, y, z);
		return a.Multiply(by);
	}

	public static Vector2 Clamp(this Vector2 a, Vector2 from, Vector2 to) {
		from.x = from.x < to.x ? Mathf.Clamp(a.x, from.x, to.x) : Mathf.Clamp(a.x, to.x, from.x);
		from.y = from.y < to.y ? Mathf.Clamp(a.y, from.y, to.y) : Mathf.Clamp(a.y, to.y, from.y);
		return from;
	}

	public static Vector3 Clamp(this Vector3 a, Vector3 from, Vector3 to) {
		from.x = from.x < to.x ? Mathf.Clamp(a.x, from.x, to.x) : Mathf.Clamp(a.x, to.x, from.x);
		from.y = from.y < to.y ? Mathf.Clamp(a.y, from.y, to.y) : Mathf.Clamp(a.y, to.y, from.y);
		from.z = from.z < to.z ? Mathf.Clamp(a.z, from.z, to.z) : Mathf.Clamp(a.z, to.z, from.z);
		return from;
	}

	public static Vector4 Clamp(this Vector4 a, Vector4 from, Vector4 to) {
		from.x = from.x < to.x ? Mathf.Clamp(a.x, from.x, to.x) : Mathf.Clamp(a.x, to.x, from.x);
		from.y = from.y < to.y ? Mathf.Clamp(a.y, from.y, to.y) : Mathf.Clamp(a.y, to.y, from.y);
		from.z = from.z < to.z ? Mathf.Clamp(a.z, from.z, to.z) : Mathf.Clamp(a.z, to.z, from.z);
		from.w = from.w < to.w ? Mathf.Clamp(a.w, from.w, to.w) : Mathf.Clamp(a.w, to.w, from.w);
		return from;
	}
}
