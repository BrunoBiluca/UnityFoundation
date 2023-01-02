using System;
using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class UnityDestroyBehaviour : MonoBehaviour, IDestroyBehaviour
    {
        public event Action OnDestroyAction;

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void Destroy(float time)
        {
            Destroy(gameObject, time);
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