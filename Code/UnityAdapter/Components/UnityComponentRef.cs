using System;
using UnityEngine;
using UnityFoundation.Code.DebugHelper;

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
        private string referenceName;

        public event Action OnInvalidState;

        public bool IsValid { get; private set; }

        public UnityComponentRef(T reference)
        {
            this.reference = reference;
            referenceName = reference.name;
            IsValid = reference != null;
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
            UnityDebug.I.LogHighlight(referenceName, "Reference state is invalid");
            IsValid = false;
            OnInvalidState?.Invoke();
        }
    }
}
