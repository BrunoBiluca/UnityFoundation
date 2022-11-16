using System;
using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public abstract class BilucaMono : MonoBehaviour, IDestroyable
    {
        protected IDestroyBehaviour destroyBehaviour;

        public event Action OnObjectDestroyed;

        public void Awake()
        {
            if(!TryGetComponent(out destroyBehaviour))
                destroyBehaviour = gameObject.AddComponent<UnityDestroyBehaviour>();

            destroyBehaviour.OnBeforeDestroy(() => OnObjectDestroyed?.Invoke());

            OnAwake();
        }

        public void Destroy()
        {
            destroyBehaviour.Destroy();
        }

        protected virtual void OnAwake() { }
    }
}