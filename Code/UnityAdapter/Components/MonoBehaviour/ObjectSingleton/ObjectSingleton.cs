using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
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
                instance.Set(CreateInstance());
            }
            Obj = instance.Get<T>();
        }

        protected virtual void OnAwake() { }
        protected abstract T CreateInstance();
    }
}