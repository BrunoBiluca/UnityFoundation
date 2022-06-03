using UnityEngine;

namespace UnityFoundation.Code.MonoBehaviourUtils
{
    public class UnityDestroyBehaviour : MonoBehaviour, IDestroyBehaviour
    {
        private System.Action onDestroyAction;

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void Destroy(float time)
        {
            Destroy(gameObject, time);
        }

        public void OnBeforeDestroy(System.Action preDestroyAction)
        {
            onDestroyAction = preDestroyAction;
        }

        ///
        /// UNITY CALLBACK METHODS
        ///

        private void OnDestroy()
        {
            onDestroyAction?.Invoke();
        }

    }
}