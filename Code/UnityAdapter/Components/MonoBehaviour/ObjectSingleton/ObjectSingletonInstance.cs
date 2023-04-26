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
}