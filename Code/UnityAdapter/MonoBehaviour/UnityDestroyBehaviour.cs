using System;
using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class UnityDestroyBehaviour : MonoBehaviour, IDestroyBehaviour
    {
        private Action onDestroyAction;

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
            onDestroyAction = preDestroyAction;
        }

        ///
        /// UNITY CALLBACK METHODS
        ///

        private void OnDestroy()
        {
            onDestroyAction?.Invoke();
        }

    }
}