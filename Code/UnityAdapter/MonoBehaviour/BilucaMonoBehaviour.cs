using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class BilucaMonoBehaviour : MonoBehaviour
    {
        protected IDestroyBehaviour destroyBehaviour;

        public void Awake()
        {
            destroyBehaviour = GetComponent<IDestroyBehaviour>();
            if(destroyBehaviour == null)
                destroyBehaviour = gameObject.AddComponent<UnityDestroyBehaviour>();

            OnAwake();
        }

        protected virtual void OnAwake() { }
    }
}