//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Values/System/Object")]
	public class Value_System_Object : ValueBase<System.Object> {
		public Input_System_Object valueInput;
		public Output_System_Object valueOutput;

#if UNITY_EDITOR
		public override void Init() {
			SetName("Object");
			valueOutput = CreateIO<Output_System_Object>();
			valueInput = CreateIO<Input_System_Object>();
		}
#endif

		public override void Calculate() {
			valueOutput.SetValue(value);
		}

		public override NodeInput GetInput() { return valueInput; }
		public override NodeOutput GetOutput() { return valueOutput; }
	}
}
