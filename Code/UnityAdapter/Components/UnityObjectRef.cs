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
    public class UnityObjectRef<T>
        : IComponentState
        , IBilucaLoggable
        where T : UnityEngine.Object
    {
        private readonly T reference;
        private readonly string referenceName;

        public event Action OnInvalidState;

        public IBilucaLogger Logger { get; set; }

        public bool IsValid { get; private set; }

        public UnityObjectRef(T reference)
        {
            this.reference = reference;
            referenceName = reference.name;
            IsValid = reference != null;
        }

        public void Ref(Action<T> call)
        {
            if(!IsValid) return;

            try
            {
                if(reference == null)
                {
                    SetInvalidState();
                    return;
                }

                call(reference);
            }
            catch(MissingReferenceException)
            {
                SetInvalidState();
            }
        }

        public TResult Ref<TResult>(Func<T, TResult> call)
        {
            if(!IsValid) return default;

            try
            {
                if(reference == null)
                {
                    SetInvalidState();
                    return default;
                }

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
            Logger?.LogHighlight(referenceName, "Reference state is invalid");
            IsValid = false;
            OnInvalidState?.Invoke();
        }
    }
}
