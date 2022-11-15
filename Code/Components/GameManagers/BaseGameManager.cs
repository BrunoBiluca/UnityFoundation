using UnityEngine;

namespace UnityFoundation.Code.GameManagers
{
    public class BaseGameManager : BaseGameManager<BaseGameManager> { }

    public class BaseGameManager<T> : Singleton<T> where T : BaseGameManager<T>
    {
        [SerializeField]
        private bool debugMode;
        public bool DebugMode {
            get { return debugMode; }
            private set { debugMode = value; }
        }
    }
}