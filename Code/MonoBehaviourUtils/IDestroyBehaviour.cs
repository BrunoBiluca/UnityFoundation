using System;
using UnityEngine;

namespace Assets.UnityFoundation.Code.MonoBehaviourUtils
{
    public interface IDestroyBehaviour
    {

        public void Destroy();
        public void Destroy(float time);
        public void OnBeforeDestroy(Action preDestroyAction);
    }
}