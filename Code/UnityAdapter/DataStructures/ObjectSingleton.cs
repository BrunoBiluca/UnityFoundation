using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public sealed class ObjectSingletonInstance : MonoBehaviour
    {
        private object obj;

        public void Set<T>(T instance)
        {
            obj = instance;
        }

        public T Get<T>()
        {
            return (T)obj;
        }
    }

    public abstract class ObjectSingleton<T> : MonoBehaviour
    {
        public T Obj { get; private set; }

        public void Awake()
        {
            AwakeInstance();
            OnAwake();
        }

        private void AwakeInstance()
        {
            if(!TryGetComponent(out ObjectSingletonInstance instance))
            {
                instance = gameObject.AddComponent<ObjectSingletonInstance>();
                instance.Set<T>(CreateInstance());
            }
            Obj = instance.Get<T>();
        }

        protected virtual void OnAwake() { }
        protected abstract T CreateInstance();
    }
}