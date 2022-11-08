using System;
using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class UnityComponentRef<T> : IValidState
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
