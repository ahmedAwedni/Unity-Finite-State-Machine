// 1. State.cs
using UnityEngine;


/// The abstract base class for all states.

public abstract class State
{
    // Called once when the state begins
    public virtual void Enter() { }

    // Called every frame like Update()
    public virtual void Tick() { }

    // Called every physics frame like FixedUpdate()
    public virtual void FixedTick() { }

    // Called once when the state ends
    public virtual void Exit() { }
}