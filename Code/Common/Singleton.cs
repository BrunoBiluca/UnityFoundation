using UnityEngine;

namespace Assets.UnityFoundation.Code.Common
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {

        private static Singleton<T> instance;
        public static T Instance {
            get {
                if(instance == null)
                {
                    var className = typeof(T).Name;
                    instance = new GameObject(className)
                        .AddComponent<T>();
                    Debug.LogWarning($"{className} was created automatically and was not found on scene. Is this the expected behaviour?");
                }


                return (T)instance;
            }
        }

        [SerializeField] private bool destroyOnLoad;
        public virtual bool DestroyOnLoad => destroyOnLoad;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
                if(!DestroyOnLoad) DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            OnAwake();
        }

        protected virtual void OnAwake() { }

    }
}