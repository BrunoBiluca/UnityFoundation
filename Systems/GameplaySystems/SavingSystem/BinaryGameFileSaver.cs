using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityFoundation.Code.DebugHelper;

namespace UnityFoundation.SavingSystem
{
    public class BinaryGameFileSaver : IGameFileSaver, IBilucaLoggable
    {
        public IBilucaLogger Logger { get; set; }

        public string Save<T>(string fileName, T obj)
        {
            var binaryFormatter = new BinaryFormatter();
            string path = FormatFilePath(fileName);
            
            using var file = File.Create(path);
            binaryFormatter.Serialize(file, obj);

            Logger?.LogHighlight("Saving binary", "path:", path);
            return path;
        }

        public T Load<T>(string fileName)
        {
            if(!File.Exists(FormatFilePath(fileName)))
                return default;

            using var file = File.Open(FormatFilePath(fileName), FileMode.Open);

            return (T)new BinaryFormatter().Deserialize(file);
        }

        private string FormatFilePath(string fileName)
        {
            return $"{Application.persistentDataPath}/{fileName}.bdata";
        }
    }
}