// 4. EnemyStates.cs
using UnityEngine;

// --- IDLE STATE ---
public class IdleState : State
{
    private EnemyController enemy;
    private StateMachine sm;

    public IdleState(EnemyController enemy, StateMachine sm)
    {
        this.enemy = enemy;
        this.sm = sm;
    }

    public override void Enter()
    {
        Debug.Log("Enemy entered IDLE state.");
    }

    public override void Tick()
    {
        // Transition condition: If target gets too close, switch to Chase state
        if (Vector3.Distance(enemy.transform.position, enemy.target.position) <= enemy.chaseRange)
        {
            sm.ChangeState(enemy.ChaseState);
        }
    }
}

// --- CHASE STATE ---
public class ChaseState : State
{
    private EnemyController enemy;
    private StateMachine sm;

    public ChaseState(EnemyController enemy, StateMachine sm)
    {
        this.enemy = enemy;
        this.sm = sm;
    }

    public override void Enter()
    {
        Debug.Log("Enemy entered CHASE state!");
    }

    public override void Tick()
    {
        // Simple chase logic
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.target.position, enemy.moveSpeed * Time.deltaTime);

        // Transition condition: If target escapes, switch back to Idle state
        if (Vector3.Distance(enemy.transform.position, enemy.target.position) > enemy.chaseRange)
        {
            sm.ChangeState(enemy.IdleState);
        }
    }
}