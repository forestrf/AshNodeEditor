//////////////////////////////////////
//// FILE GENERATED AUTOMATICALLY ////
//////////////////////////////////////

using System;
using Ashkatchap.AIBrain.Nodes;

namespace Ashkatchap.AIBrain.GeneratedNodes {
	[Serializable]
	[CreateNode("Values/UnityEngine/AI/NavMeshAgent")]
	public class Value_UnityEngine_AI_NavMeshAgent : ValueBase<UnityEngine.AI.NavMeshAgent> {
		public Input_UnityEngine_AI_NavMeshAgent valueInput;
		public Output_UnityEngine_AI_NavMeshAgent valueOutput;

#if UNITY_EDITOR
		public override void Init() {
			SetName("NavMeshAgent");
			valueOutput = CreateIO<Output_UnityEngine_AI_NavMeshAgent>();
			valueInput = CreateIO<Input_UnityEngine_AI_NavMeshAgent>();
		}
#endif

		public override void Calculate() {
			valueOutput.SetValue(value);
		}

		public override NodeInput GetInput() { return valueInput; }
		public override NodeOutput GetOutput() { return valueOutput; }
	}
}
