using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	public struct MemberFull {

	}

	public static class TypeFinder {
		public static readonly Type[] Types;
		private static readonly Dictionary<Type, MemberInfo[]> MembersByType = new Dictionary<Type, MemberInfo[]>();

		static TypeFinder() {
			if (Types == null || Types.Length == 0) {
				Types = AppDomain.CurrentDomain.GetAssemblies()
					.Where(a => !a.GetName().Name.StartsWith("UnityEditor") &&
					!a.GetName().Name.StartsWith("Assembly-CSharp-Editor"))
					.SelectMany(a => a.GetTypes())
					.Where(t =>
					!typeof(Group).IsAssignableFrom(t) &&
					!typeof(Context).IsAssignableFrom(t) &&
					(typeof(UnityEngine.Object).IsAssignableFrom(t) ||
					t.IsValueType ||
					t.FullName.StartsWith("UnityEngine") ||
					t == typeof(string) ||
					t.IsSerializable ||
					(t.IsAbstract && t.GetConstructors(BindingFlags.Public).Length == 0 &&
					(
					t.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly).Any(m => supportedTypes.Contains(m.ReturnType) && m.GetParameters().All(p => p.ParameterType.IsValueType || supportedTypes.Contains(p.ParameterType))) ||
					t.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly).Any(p => p.PropertyType.IsValueType || supportedTypes.Contains(p.PropertyType)) ||
					t.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly).Any(f => f.FieldType.IsValueType || supportedTypes.Contains(f.FieldType))
					)
					)
					)
					).ToArray();
			}
		}

		static BindingFlags methodsBF = BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static;

		private static void PrepareMembersByType(Type type) {
			MemberInfo[] methods;
			if (!MembersByType.TryGetValue(type, out methods)) {
				methods = type.GetMembers(methodsBF).ToArray();
				MembersByType.Add(type, methods);
			}
		}

		static Type[] supportedTypes = GetFilteredStaticTypes();

		public static MethodInfo[] GetMethods(Type type) {
			PrepareMembersByType(type);

			MemberInfo[] members = MembersByType[type];

			List<MethodInfo> validMethods = new List<MethodInfo>();

			// Next list all methods
			for (int i = 0; i < members.Length; i++) {
				if (members[i].MemberType == MemberTypes.Method) {
					MethodInfo methodInfo = members[i] as MethodInfo;
					if (!methodInfo.IsDefined(typeof(ObsoleteAttribute), true)) {
						validMethods.Add(methodInfo);
					}
				}
			}

			// Sort members alphabetically
			return validMethods.OrderBy(x => x.Name).ToArray();
		}

		public static MemberInfo[] GetPropertiesAndFields(Type type) {
			PrepareMembersByType(type);

			MemberInfo[] members = MembersByType[type];

			List<MemberInfo> validMembers = new List<MemberInfo>();

			// List all properties and fields first
			for (int i = 0; i < members.Length; i++) {
				if (members[i].MemberType == MemberTypes.Property) {
					PropertyInfo propertyInfo = members[i] as PropertyInfo;
					if (!propertyInfo.IsDefined(typeof(ObsoleteAttribute), true) && propertyInfo.CanWrite && IsSupportedType(supportedTypes, propertyInfo.PropertyType))
						validMembers.Add(propertyInfo);
				}
				else if (members[i].MemberType == MemberTypes.Field) {
					FieldInfo fieldInfo = members[i] as FieldInfo;
					if (!fieldInfo.IsDefined(typeof(ObsoleteAttribute), true) && IsSupportedType(supportedTypes, fieldInfo.FieldType))
						validMembers.Add(fieldInfo);
				}
			}

			// Sort members alphabetically
			return validMembers.OrderBy(x => x.Name).ToArray();
		}

		static bool AreSupportedTypes(Type[] array, Type[] values) {
			for (int i = 0; i < values.Length; i++) {
				if (!IsSupportedType(array, values[i]))
					return false;
			}
			return true;
		}

		static bool IsSupportedType(Type[] array, Type value) {
			for (int i = 0; i < array.Length; i++)
				if (IsSameOrSubclass(array[i], value))
					return true;
			return false;
		}

		static bool IsSameOrSubclass(Type potentialBase, Type potentialDescendant) {
			return potentialDescendant.IsSubclassOf(potentialBase) || potentialDescendant == potentialBase;
		}

		static Type[] GetFilteredStaticTypes() {
			List<Type> filteredTypes = new List<Type>();

			filteredTypes.Add(typeof(UnityEngine.Object));
			filteredTypes.Add(typeof(string));
			filteredTypes.Add(typeof(int));
			filteredTypes.Add(typeof(uint));
			filteredTypes.Add(typeof(short));
			filteredTypes.Add(typeof(ushort));
			filteredTypes.Add(typeof(long));
			filteredTypes.Add(typeof(ulong));
			filteredTypes.Add(typeof(byte));
			filteredTypes.Add(typeof(sbyte));
			filteredTypes.Add(typeof(float));
			filteredTypes.Add(typeof(double));
			filteredTypes.Add(typeof(bool));
			filteredTypes.Add(typeof(Color));
			filteredTypes.Add(typeof(Color32));
			filteredTypes.Add(typeof(Vector2));
			filteredTypes.Add(typeof(Vector3));
			filteredTypes.Add(typeof(Vector4));
			filteredTypes.Add(typeof(Quaternion));
			filteredTypes.Add(typeof(Plane));
			filteredTypes.Add(typeof(Ray));
			// Incomplete
			filteredTypes.Add(typeof(Enum));

			return filteredTypes.ToArray();
		}

		public static string GetDisplayName(MemberInfo memberInfo, bool prependMemberType, bool richFormat = false) {
			if (memberInfo.MemberType == MemberTypes.Method) {
				var method = memberInfo as MethodInfo;
				ParameterInfo[] parameters = method.GetParameters();
				string displayName = GetNameFormat(memberInfo.Name, richFormat) + " (";
				for (int i = 0; i < parameters.Length; i++) {
					if (parameters[i].IsOut)
						displayName += richFormat ? "<color=#000066>out</color> " : "out ";
					else if (parameters[i].ParameterType.IsByRef)
						displayName += richFormat ? "<color=#000066>ref</color> " : "ref ";
					displayName += GetCleanTypeName(parameters[i].ParameterType, richFormat) + " " + parameters[i].Name;
					if (i < parameters.Length - 1)
						displayName += ", ";
				}
				displayName += ")";
				displayName += " : " + GetCleanTypeName(method.ReturnType, richFormat);
				return displayName;
			}
			else if (memberInfo.MemberType == MemberTypes.Property) {
				PropertyInfo propertyInfo = memberInfo as PropertyInfo;
				return (prependMemberType ? GetCleanTypeName(propertyInfo.PropertyType, richFormat) + " " : "") + GetNameFormat(propertyInfo.Name, richFormat);
			}
			else {
				FieldInfo fieldInfo = memberInfo as FieldInfo;
				return (prependMemberType ? GetCleanTypeName(fieldInfo.FieldType, richFormat) + " " : "") + GetNameFormat(fieldInfo.Name, richFormat);
			}
		}

		public static string GetNameFormat(string name, bool richFormat) {
			if (richFormat)
				return "<b>" + name + "</b>";
			return name;
		}

		public static string GetCleanTypeName(Type type, bool richFormat) {
			var str = type.Name.Split('.').Last().Replace("&", "");
			if (richFormat) {
				str = "<color=#660000>" + str + "</color>";
			}
			return str;
		}
	}
}
