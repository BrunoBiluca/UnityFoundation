using System.Collections.Generic;

namespace UnityFoundation.SavingSystem
{
    public class MemoryGameSaver : IGameFileSaver
    {
        private readonly Dictionary<string, object> files;

        public MemoryGameSaver()
        {
            files = new Dictionary<string, object>();
        }

        public T Load<T>(string fileName)
        {
            if(!files.ContainsKey(fileName))
                return default;

            return (T)files[fileName];
        }

        public string Save<T>(string fileName, T obj)
        {
            files.Add(fileName, obj);
            return fileName;
        }
    }
}