using System;
using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public abstract class BilucaMono : MonoBehaviour, IDestroyable
    {
        protected IDestroyBehaviour destroyBehaviour;

        public event Action OnDestroyAction;

        public void Awake()
        {
            if(!TryGetComponent(out destroyBehaviour))
                destroyBehaviour = gameObject.AddComponent<UnityDestroyBehaviour>();

            destroyBehaviour.OnBeforeDestroy(() => OnDestroyAction?.Invoke());

            OnAwake();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        protected virtual void OnAwake() { }
    }
}