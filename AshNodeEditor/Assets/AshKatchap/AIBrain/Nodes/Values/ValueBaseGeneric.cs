using Ashkatchap.AIBrain.Nodes;
using System;

namespace Ashkatchap.AIBrain {
	[Serializable]
	public abstract class ValueBase<T> : ValueBase {
		public T value;

		public override object GetValueAsObject() {
			return value;
		}
		public override void SetValueAsObject(object value) {
			this.value = (T) value;
		}

		public override void SetValueFromInput(NodeInput input) {
			value = ((NodeInput<T>) input).GetValue();
			((NodeOutput<T>) outputs[0]).value = value;
		}
	}
}
