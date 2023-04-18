namespace UnityFoundation.SavingSystem
{
    public interface IGameFileSaver
    {
        T Load<T>(string fileName);
        string Save<T>(string fileName, T obj);
    }
}