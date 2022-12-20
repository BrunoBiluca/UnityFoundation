using System;
using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public abstract class BilucaMono : MonoBehaviour, IDestroyable
    {
        protected IDestroyBehaviour destroyBehaviour;

        public event Action OnObjectDestroyed;

        public ITransform Transform { get; private set; }

        public void Awake()
        {
            if(!TryGetComponent(out destroyBehaviour))
                destroyBehaviour = gameObject.AddComponent<UnityDestroyBehaviour>();

            Transform = new TransformDecorator(transform);

            OnAwake();
        }

        public void Destroy()
        {
            OnObjectDestroyed?.Invoke();
            destroyBehaviour.Destroy();
        }

        protected virtual void OnAwake() { }
    }
}