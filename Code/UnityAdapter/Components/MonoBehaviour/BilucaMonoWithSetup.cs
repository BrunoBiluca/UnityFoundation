using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Code
{
    public abstract class BilucaMonoWithSetup<T> : BilucaMono
    {
        public bool WasSetup { get; private set; } = false;

        public void Setup(T parameters) { 
            OnSetup(parameters); 
            WasSetup = true; 
        }

        protected abstract void OnSetup(T parameters);

        public void Update()
        {
            if(!WasSetup) return;
            OnUpdate();
        }

        protected virtual void OnUpdate() { }
    }
}
