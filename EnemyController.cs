// 3. EnemyController.cs
using UnityEngine;

/// The main MonoBehaviour attached to your object. It holds the StateMachine and the specific states.

public class EnemyController : MonoBehaviour
{
    public StateMachine StateMachine { get; private set; }

    // Define your specific states
    public IdleState IdleState { get; private set; }
    public ChaseState ChaseState { get; private set; }

    [Header("Enemy Data")]
    public Transform target;
    public float moveSpeed = 3f;
    public float chaseRange = 5f;

    private void Awake()
    {
        StateMachine = new StateMachine();

        // Pass 'this' controller to the states so they can access its data (like moveSpeed or target)
        IdleState = new IdleState(this, StateMachine);
        ChaseState = new ChaseState(this, StateMachine);
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.Tick();
    }

    private void FixedUpdate()
    {
        StateMachine.FixedTick();
    }
}