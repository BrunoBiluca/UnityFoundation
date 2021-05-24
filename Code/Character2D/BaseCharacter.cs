using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    public BaseCharacterState CurrentState { get; private set; }

    public BaseCharacterState BaseState { get; private set; }

    protected virtual void OnAwake() { }
    protected virtual void OnUpdate() { }
    protected virtual void OnFixedUpdate() { }
    protected virtual void PosOnCollisionEnter2D(Collision2D collision) { }
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

    public virtual void TransitionToState(BaseCharacterState newState)
    {
        if(
            !newState.ForceInterruption
            && CurrentState != null 
            && !CurrentState.CanExitState()
        ) return;
        
        if(!newState.CanEnterState()) return;

        var previousState = CurrentState;
        CurrentState = newState;

        previousState?.ExitState();
        CurrentState.EnterState();
    }
}
