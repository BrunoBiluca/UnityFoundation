using System;
using UnityFoundation.Code.DebugHelper;

namespace UnityFoundation.Code.UnityAdapter
{
    public class BilucaMonoInstance : IDestroyable, IBilucaLoggable
    {
        public IDestroyBehaviour DestroyBehaviour { get; set; }
        public ITransform Transform { get; set; }
        public IBilucaLogger Logger { get; set; }

        public event Action OnObjectDestroyed;

        public void Destroy()
        {
            OnObjectDestroyed?.Invoke();
            DestroyBehaviour.Destroy();
        }
    }
}