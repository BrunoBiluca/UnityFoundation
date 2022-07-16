using UnityEngine;

namespace UnityFoundation.Code
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

        [field: SerializeField] public bool DestroyOnLoad { get; set; }

        public void Awake()
        {
            ConfigSingleton();

            if(instance == null)
            {
                instance = this;
                if(!DestroyOnLoad) DontDestroyOnLoad(gameObject);
            }
            else
            {
#if !UNITY_EDITOR
                Destroy(gameObject);
#else
                DestroyImmediate(gameObject);
#endif
            }

            OnAwake();
        }

        protected virtual void OnAwake() { }

        protected virtual void ConfigSingleton() { }

    }
}