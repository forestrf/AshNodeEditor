using System.Collections.Generic;
using System;
using System.Linq;

namespace Ashkatchap.AIBrain {
	[AttributeUsage(AttributeTargets.Class)]
	public class CreateNodeAttribute : Attribute {
		public readonly string name, description;

		public CreateNodeAttribute(string name) {
			this.name = name;
		}
		public CreateNodeAttribute(string name, string description) {
			this.name = name;
			this.description = description;
		}

		public static List<KeyValuePair<Type, CreateNodeAttribute>> Cached = new List<KeyValuePair<Type, CreateNodeAttribute>>();
		public static void Prepare() {
			Cached.Clear();
			var type = typeof(Node);
			var types = AppDomain.CurrentDomain.GetAssemblies();
			for (int i = 0; i < types.Length; i++) {
				Type[] t = types[i].GetTypes().Where(tp => type.IsAssignableFrom(tp)).ToArray();
				for (int j = 0; j < t.Length; j++) {
					var attr = t[j].GetCustomAttributes(typeof(CreateNodeAttribute), false);
					if (attr.Length > 0) {
						Cached.Add(new KeyValuePair<Type, CreateNodeAttribute>(t[j], ((CreateNodeAttribute) attr[0])));
					}
				}
			}
		}
	}
}
