//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Values/System/Int32")]
	public class Value_System_Int32 : ValueBase<System.Int32> {
		public Input_System_Int32 valueInput;
		public Output_System_Int32 valueOutput;

#if UNITY_EDITOR
		public override void Init() {
			SetName("Int32");
			valueOutput = CreateIO<Output_System_Int32>();
			valueInput = CreateIO<Input_System_Int32>();
		}
#endif

		public override void Calculate() {
			valueOutput.SetValue(value);
		}

		public override NodeInput GetInput() { return valueInput; }
		public override NodeOutput GetOutput() { return valueOutput; }
	}
}
