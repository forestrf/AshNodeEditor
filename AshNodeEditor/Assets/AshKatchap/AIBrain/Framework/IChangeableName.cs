public interface IChangeableName {
#if UNITY_EDITOR
	void SetName(string newName);
	string GetName();
#endif
}
