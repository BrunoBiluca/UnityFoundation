using Assets.UnityFoundation.Code.Common;
using UnityEngine;

namespace Assets.UnityFoundation.GameManagers
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