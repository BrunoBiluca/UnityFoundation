using System;
using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public abstract class BilucaMonoBehaviour : MonoBehaviour, IDestroyable
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

        protected virtual void OnAwake() { }
    }
}