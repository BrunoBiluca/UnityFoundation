namespace UnityFoundation.Code.Characters
{
    public class BaseCharacterState
    {

        public virtual bool ForceInterruption => false;
        public virtual bool CanBeInterrupted => true;
        public virtual bool CanEnterState() => true;
        public virtual bool CanExitState() => CanBeInterrupted;
        public virtual void EnterState() { }
        public virtual void TriggerAnimationEvent(string eventName) { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void ExitState() { }
    }
}