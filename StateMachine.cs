// 2. StateMachine.cs
using UnityEngine;

/// The brain that handles switching from one state to another.

public class StateMachine
{
    public State CurrentState { get; private set; }

    // Sets the initial state without calling Exit on a previous null state
    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    // Transitions to a new state
    public void ChangeState(State newState)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }

        CurrentState = newState;
        CurrentState.Enter();
    }

    // Must be called by the MonoBehaviour's Update
    public void Tick()
    {
        CurrentState?.Tick();
    }

    // Must be called by the MonoBehaviour's FixedUpdate
    public void FixedTick()
    {
        CurrentState?.FixedTick();
    }
}