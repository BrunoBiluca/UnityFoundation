using System;
using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class BilucaMonoBehaviour : MonoBehaviour, IDestroyable
    {
        protected IDestroyBehaviour destroyBehaviour;

        public event Action OnDestroyAction;

        public void Awake()
        {
            destroyBehaviour = GetComponent<IDestroyBehaviour>();
            if(destroyBehaviour == null)
                destroyBehaviour = gameObject.AddComponent<UnityDestroyBehaviour>();

            destroyBehaviour.OnBeforeDestroy(() => OnDestroyAction?.Invoke());

            OnAwake();
        }

        protected virtual void OnAwake() { }
    }
}