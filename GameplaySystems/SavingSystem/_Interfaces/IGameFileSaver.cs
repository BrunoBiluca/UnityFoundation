namespace UnityFoundation.SavingSystem
{
    public interface IGameFileSaver
    {
        void Clear(string saveFile) { }
        T Load<T>(string fileName);
        string Save<T>(string fileName, T obj);
        bool SaveFileExists(string fileName) { return false; }
    }
}