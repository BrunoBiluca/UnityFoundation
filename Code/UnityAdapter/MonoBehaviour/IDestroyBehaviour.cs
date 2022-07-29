using System;

namespace UnityFoundation.Code.UnityAdapter
{

    public interface IDestroyBehaviour
    {
        public void Destroy();
        public void Destroy(float time);
        public void OnBeforeDestroy(Action preDestroyAction);
    }
}