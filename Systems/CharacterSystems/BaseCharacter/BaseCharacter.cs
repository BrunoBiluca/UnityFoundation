using TMPro;
using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.BaseCharacter
{
    public abstract class DefaultBaseCharacter : BaseCharacter<BaseCharacterState> { }

    public abstract class BaseCharacter<T> : MonoBehaviour where T : BaseCharacterState
    {
        public bool DebugMode;
        public TMP_Text StateText;

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

        public void Awake()
        {
            if(DebugMode)
            {
                // TODO: transfomar esse debug canvas em um debug padrão
                // para outros sistemas do Unity Foundation, como por exemplo HealthSystem
                var canvas = transform.FindComponent<Canvas>("debug_canvas");

                if(canvas != null)
                {
                    canvas.gameObject.SetActive(true);
                    StateText = transform.FindComponent<TMP_Text>("state_text");
                }
            }

            OnAwake();
            SetCharacterStates();
        }

        public void Start()
        {
            OnStart();
        }

        public void OnEnable()
        {
            OnEnableCall();
        }

        public void Update()
        {
            CurrentState?.Update();
            OnUpdate();
        }

        public void FixedUpdate()
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

            ChangeState(newState);

            SetupDebugMode(newState);
        }

        private void ChangeState(T newState)
        {
            var previousState = CurrentState;
            CurrentState = newState;

            previousState?.ExitState();
            CurrentState.EnterState();
        }

        private bool CanTransitionState(T newState)
            => CurrentState == null
                || CurrentState.CanExitState()
                || CurrentState.CanBeInterrupted && newState.ForceInterruption;

        public virtual void TransitionToStateIfDifferent(T newState)
        {
            if(newState == CurrentState) return;

            TransitionToState(newState);
        }

        public virtual void TransitionToStateForce(T newState)
        {
            ChangeState(newState);
        }

        private void SetupDebugMode(T newState)
        {
            if(!DebugMode)
            {
                if(StateText != null)
                    StateText.gameObject.SetActive(false);
                return;
            }

            Debug.Log($"State: {newState.GetType().Name} ({gameObject.name})");
            if(StateText != null)
            {
                StateText.gameObject.SetActive(true);
                StateText.text = "State: " + newState.GetType().Name;
            }
        }
    }
}