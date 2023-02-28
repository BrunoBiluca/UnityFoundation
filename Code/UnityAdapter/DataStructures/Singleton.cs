using UnityEngine;

namespace UnityFoundation.Code
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static Singleton<T> instance;
        public static T I => Instance;
        public static T Instance {
            get {
                if(instance == null)
                {
                    var className = typeof(T).Name;
                    instance = new GameObject(className).AddComponent<T>();
                    instance.Awake();
                    Debug.LogWarning($"{className} was created automatically and was not found on scene. Is this the expected behaviour?");
                }
                return (T)instance;
            }
        }

        [field: SerializeField] public virtual bool DestroyOnLoad { get; set; } = true;

        public void Awake()
        {
            PreAwake();

            if(instance == null)
            {
                instance = this;
                if(!DestroyOnLoad) DontDestroyOnLoad(gameObject);
            }

            if(this != instance)
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

        protected virtual void PreAwake() { }

        public void Start()
        {
            OnStart();
        }

        protected virtual void OnStart() { }

    }
}