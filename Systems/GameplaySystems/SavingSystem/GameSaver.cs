using UnityFoundation.Code;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.SavingSystem
{
    public class GameSaver : Singleton<GameSaver>
    {
        public void Save<T>(string fileName, T obj)
        {
            var binaryFormatter = new BinaryFormatter();
            using var file = File.Create($"{Application.persistentDataPath}/{fileName}");

            binaryFormatter.Serialize(file, obj);
        }

        public T Load<T>(string fileName)
        {
            if(!File.Exists($"{Application.persistentDataPath}/{fileName}"))
                return default;

            var binaryFormatter = new BinaryFormatter();
            using var file = File.Open(
                $"{Application.persistentDataPath}/{fileName}",
                FileMode.Open
            );

            return (T)binaryFormatter.Deserialize(file);
        }
    }
}