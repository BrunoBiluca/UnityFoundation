using System;
using UnityFoundation.Code.DebugHelper;

namespace UnityFoundation.Code.UnityAdapter
{
    public abstract class BilucaMono : 
        ObjectSingleton<BilucaMonoInstance>, 
        IDestroyable,
        IBilucaLoggable
    {
        public ITransform Transform => Obj.Transform;
        public IBilucaLogger Logger { get => Obj.Logger; set => Obj.Logger = value; }

        public event Action OnObjectDestroyed;

        public void Destroy()
        {
            Obj.Destroy();
        }

        protected override BilucaMonoInstance CreateInstance()
        {
            var inst = new BilucaMonoInstance();

            if(TryGetComponent(out IDestroyBehaviour destroyBehaviour))
                inst.DestroyBehaviour = destroyBehaviour;
            else
                inst.DestroyBehaviour = gameObject.AddComponent<UnityDestroyBehaviour>();

            inst.Transform = new TransformDecorator(transform);
            inst.OnObjectDestroyed += () => OnObjectDestroyed?.Invoke();
            return inst;
        }
    }
}