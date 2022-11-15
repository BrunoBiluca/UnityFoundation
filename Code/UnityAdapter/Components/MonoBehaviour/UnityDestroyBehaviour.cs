using System;
using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class UnityDestroyBehaviour : MonoBehaviour, IDestroyBehaviour
    {
        public event Action OnDestroyAction;

        public void Destroy()
        {
#if !UNITY_EDITOR
            Destroy(gameObject);
#else
            DestroyImmediate(gameObject);
#endif
        }

        public void Destroy(float time)
        {
#if !UNITY_EDITOR
            Destroy(gameObject, time);
#else
            DestroyImmediate(gameObject);
#endif
        }

        public void OnBeforeDestroy(Action preDestroyAction)
        {
            OnDestroyAction = preDestroyAction;
        }

        ///
        /// UNITY CALLBACK METHODS
        ///

        private void OnDestroy()
        {
            OnDestroyAction?.Invoke();
        }

    }
}