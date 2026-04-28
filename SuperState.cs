// 3. SuperState.cs
using UnityEngine;

/// A state that contains its own internal State Machine.
/// Perfect for grouping related sub-states together (e.g., an Airborne state managing Jump and Fall).
public abstract class SuperState : State
{
    protected StateMachine subStateMachine;

    public SuperState()
    {
        subStateMachine = new StateMachine();
    }

    public override void Enter()
    {
        base.Enter();
        // NOTE: Sub-classes must call subStateMachine.Initialize(startingSubState) in their overrides!
    }

    public override void Tick()
    {
        base.Tick();
        // Automatically tick the active sub-state
        subStateMachine.Tick(); 
    }

    public override void FixedTick()
    {
        base.FixedTick();
        subStateMachine.FixedTick();
    }

    public override void Exit()
    {
        base.Exit();
        // Ensure the active sub-state also runs its Exit routine when the SuperState finishes
        if (subStateMachine.CurrentState != null)
        {
            subStateMachine.CurrentState.Exit();
        }
    }
}
