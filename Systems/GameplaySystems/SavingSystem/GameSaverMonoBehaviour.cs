using System.Runtime.Serialization;
using UnityFoundation.Code;

namespace UnityFoundation.SavingSystem
{
    public class GameSaverMonoBehaviour : Singleton<GameSaverMonoBehaviour>, IGameFileSaver
    {
        private BinaryGameFileSaver gameSaver;

        protected override void OnAwake()
        {
            gameSaver = new BinaryGameFileSaver();
        }

        public T Load<T>(string fileName)
        {
            return gameSaver.Load<T>(fileName);
        }

        public string Save<T>(string fileName, T obj)
        {
            return gameSaver.Save(fileName, obj);
        }
    }
}