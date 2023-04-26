using System.IO;
using UnityEngine;
using UnityFoundation.Code.DebugHelper;

namespace UnityFoundation.SavingSystem
{
    public class JsonGameFileSaver : IGameFileSaver, IBilucaLoggable
    {
        public IBilucaLogger Logger { get; set; }

        public T Load<T>(string fileName)
        {
            var path = FormatFilePath(fileName);
            if(!File.Exists(path))
                return default;

            return JsonUtility.FromJson<T>(File.ReadAllText(path));
        }

        public string Save<T>(string fileName, T obj)
        {
            string path = FormatFilePath(fileName);

            Logger?.LogHighlight("Saving", "path:", path);
            File.WriteAllLines(path, new string[] { JsonUtility.ToJson(obj) });
            return path;
        }

        private string FormatFilePath(string fileName)
        {
            return $"{Application.persistentDataPath}/{fileName}.json";
        }
    }
}