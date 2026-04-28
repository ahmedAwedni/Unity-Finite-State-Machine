// 5. PlayerStates.cs
using UnityEngine;

// --- TOP LEVEL: GROUNDED ---
public class GroundedState : State
{
    private PlayerController player;
    private StateMachine sm;

    public GroundedState(PlayerController player, StateMachine sm)
    {
        this.player = player;
        this.sm = sm;
    }

    public override void Enter() => Debug.Log("Player is Grounded");
}


// --- TOP LEVEL: AIRBORNE (SUPER STATE) ---
public class AirborneState : SuperState
{
    private PlayerController player;
    private StateMachine parentSM;

    // Sub-States
    public JumpingState JumpingState { get; private set; }
    public FallingState FallingState { get; private set; }

    public AirborneState(PlayerController player, StateMachine parentSM)
    {
        this.player = player;
        this.parentSM = parentSM;

        // Initialize sub-states, passing in the subStateMachine
        JumpingState = new JumpingState(player, subStateMachine, this);
        FallingState = new FallingState(player, subStateMachine, this);
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Player entered AIRBORNE SuperState");
        
        // Start the sub-state machine at jumping
        subStateMachine.Initialize(JumpingState);
    }

    public override void Tick()
    {
        base.Tick(); // This automatically ticks whichever sub-state is active

        // Put top-level transitions here (e.g., if we hit the floor, go back to Grounded)
        // if (player.IsOnFloor) parentSM.ChangeState(player.GroundedState);
    }
}


// --- SUB-STATE: JUMPING ---
public class JumpingState : State
{
    private PlayerController player;
    private StateMachine subSM;
    private AirborneState parentState;
    private float timer;

    public JumpingState(PlayerController player, StateMachine subSM, AirborneState parentState)
    {
        this.player = player;
        this.subSM = subSM;
        this.parentState = parentState;
    }

    public override void Enter()
    {
        Debug.Log("   -> Sub-State: JUMPING");
        timer = 0f;
    }

    public override void Tick()
    {
        timer += Time.deltaTime;
        
        // Internal transition: Switch to falling after 1 second
        if (timer > 1f)
        {
            subSM.ChangeState(parentState.FallingState);
        }
    }
}


// --- SUB-STATE: FALLING ---
public class FallingState : State
{
    private PlayerController player;
    private StateMachine subSM;
    private AirborneState parentState;

    public FallingState(PlayerController player, StateMachine subSM, AirborneState parentState)
    {
        this.player = player;
        this.subSM = subSM;
        this.parentState = parentState;
    }

    public override void Enter() => Debug.Log("   -> Sub-State: FALLING");
}
