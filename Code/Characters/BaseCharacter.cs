using System;
using UnityEngine;

namespace Assets.UnityFoundation.Code.Characters
{
    public abstract class DefaultBaseCharacter : BaseCharacter<BaseCharacterState> { }

    public abstract class BaseCharacter<T> : MonoBehaviour where T : BaseCharacterState
    {
        public T CurrentState { get; protected set; }

        public T BaseState { get; protected set; }

        protected virtual void OnAwake() { }
        protected virtual void OnStart() { }
        protected virtual void OnEnableCall() { }
        protected virtual void OnUpdate() { }
        protected virtual void OnFixedUpdate() { }
        protected virtual void OnTriggerAnimationEvent(string name) { }

        protected virtual void SetBaseCharacterState(T state)
        {
            BaseState = state;
        }

        protected virtual void SetCharacterStates() { }

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

        public void TriggerAnimationEvent(string name)
        {
            CurrentState?.TriggerAnimationEvent(name);
            OnTriggerAnimationEvent(name);
        }

        public virtual void TransitionToState(T newState)
        {
            if(!CanTransitionState(newState)) return;

            if(!newState.CanEnterState()) return;

            var previousState = CurrentState;
            CurrentState = newState;

            previousState?.ExitState();
            CurrentState.EnterState();
        }

        private bool CanTransitionState(T newState)
            => CurrentState == null
                || CurrentState.CanExitState()
                || CurrentState.CanBeInterrupted && newState.ForceInterruption;
    }
}