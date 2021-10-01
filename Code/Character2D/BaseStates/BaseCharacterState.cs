using UnityEngine;

namespace Assets.UnityFoundation.Code.Character2D
{
    public abstract class BaseCharacterState
    {
        public virtual bool ForceInterruption => false;
        public virtual bool CanBeInterrupted => true;
        public virtual bool CanEnterState() => true;
        public virtual bool CanExitState() => CanBeInterrupted;
        public virtual void EnterState() { }
        public virtual void TriggerAnimationEvent(string eventName) { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void OnCollisionEnter(Collision2D collision) { }
        public virtual void OnCollisionStay(Collision2D collision) { }
        public virtual void OnTriggerStay(Collider2D collision) { }
        public virtual void ExitState() { }
    }
}