// 4. PlayerController.cs 
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public StateMachine StateMachine { get; private set; }

    // Top-Level States
    public GroundedState GroundedState { get; private set; }
    public AirborneState AirborneState { get; private set; }

    private void Awake()
    {
        StateMachine = new StateMachine();

        GroundedState = new GroundedState(this, StateMachine);
        AirborneState = new AirborneState(this, StateMachine);
    }

    private void Start()
    {
        StateMachine.Initialize(GroundedState);
    }

    private void Update()
    {
        StateMachine.Tick();

        // Spacebar switches the top-level state to Airborne
        if (Input.GetKeyDown(KeyCode.Space) && StateMachine.CurrentState == GroundedState)
        {
            StateMachine.ChangeState(AirborneState);
        }
    }
}
