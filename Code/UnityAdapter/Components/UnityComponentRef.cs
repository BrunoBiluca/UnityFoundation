using System;
using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    /// <summary>
    /// Class responsable for controle Unity components states.
    /// When the referenced component is destroyed trigger an OnInvalidState event.
    /// </summary>
    /// <typeparam name="T">Unity component</typeparam>
    public class UnityComponentRef<T> : IComponentState where T : Component
    {
        private readonly T reference;

        public event Action OnInvalidState;

        public bool IsValid { get; private set; }

        public UnityComponentRef(T reference)
        {
            this.reference = reference;
        }

        public void Ref(Action<T> call)
        {
            try
            {
                call(reference);
            }
            catch(MissingReferenceException)
            {
                SetInvalidState();
            }
        }

        public TResult Ref<TResult>(Func<T, TResult> call)
        {
            try
            {
                return call(reference);
            }
            catch(MissingReferenceException)
            {
                SetInvalidState();
                return default;
            }
        }

        private void SetInvalidState()
        {
            IsValid = false;
            OnInvalidState?.Invoke();
        }
    }
}
