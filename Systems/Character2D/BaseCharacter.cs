using UnityEngine;

namespace Assets.UnityFoundation.Code.Character2D
{
    public abstract class BaseCharacter : MonoBehaviour
    {
        public BaseCharacterState CurrentState { get; private set; }

        public BaseCharacterState BaseState { get; private set; }

        protected virtual void OnAwake() { }
        protected virtual void OnStart() { }
        protected virtual void OnEnableCall() { }
        protected virtual void OnUpdate() { }
        protected virtual void OnFixedUpdate() { }
        protected virtual void PosOnCollisionEnter2D(Collision2D collision) { }
        protected virtual void PosOnTriggerStay(Collider2D collision) { }
        protected virtual void SetBaseCharacterState(BaseCharacterState state)
        {
            BaseState = state;
        }
        protected abstract void SetCharacterStates();

        private void Awake()
        {
            OnAwake();
            SetCharacterStates();
        }

        private void Start()
        {
            OnStart();
        }

        private void OnEnable()
        {
            OnEnableCall();
        }

        private void Update()
        {
            CurrentState?.Update();
            OnUpdate();
        }

        private void FixedUpdate()
        {
            CurrentState?.FixedUpdate();
            OnFixedUpdate();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CurrentState?.OnCollisionEnter(collision);
            PosOnCollisionEnter2D(collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            CurrentState.OnTriggerStay(collision);
            PosOnTriggerStay(collision);
        }

        public void TriggerAnimationEvent(string name)
        {
            CurrentState?.TriggerAnimationEvent(name);
        }

        public virtual void TransitionToState(BaseCharacterState newState)
        {
            if(!CanTransitionState(newState)) return;

            if(!newState.CanEnterState()) return;

            var previousState = CurrentState;
            CurrentState = newState;

            previousState?.ExitState();
            CurrentState.EnterState();
        }

        private bool CanTransitionState(BaseCharacterState newState)
            => CurrentState == null
                || CurrentState.CanExitState()
                || (CurrentState.CanBeInterrupted && newState.ForceInterruption);
    }
}