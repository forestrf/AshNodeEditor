namespace Ashkatchap.AIBrain {
	public interface INodeInterruptor {
		/// <returns>true to continue the execution, false to interrupt from this node</returns>
		bool InterruptNeeded();
	}
}
