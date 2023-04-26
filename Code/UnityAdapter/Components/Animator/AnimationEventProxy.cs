using UnityEngine;

namespace UnityFoundation.Code.Extensions
{
    public interface IAnimationEventProxy
    {
        void TriggerAnimationEvent(string eventName);
    }

    /// <summary>
    /// Trigger an event sended by the animation to the animation handler
    /// </summary>
    public class AnimationEventProxy : MonoBehaviour, IAnimationEventProxy
    {
        private IAnimationEventHandler handler;
        [SerializeField] private GameObject goRef;

        void Start()
        {
            if(goRef == null)
                handler = gameObject.GetComponentInParent<IAnimationEventHandler>();
        }

        public void TriggerAnimationEvent(string eventName)
        {
            handler.AnimationEventHandler(eventName);
        }
    }
}
