# Unity Modular Finite State Machine (FSM)

A clean, class-based Finite State Machine for Unity. This system completely replaces messy "if/else" and "switch" statements in your AI and Player controllers, separating every behavior into highly organized, independent classes.

---

## ✨ Features

* **Class-Based States:** Behaviors are written as pure C# classes rather than heavy MonoBehaviours, making the system highly memory efficient.
* **Separation of Concerns:** Each state (e.g., "Idle", "Attack", "Flee") handles its own logic and its own transition rules. The main controller just tells the State Machine to "Tick()".
* **Easy Debugging:** Because states are isolated, if an enemy's "Chase" logic breaks, you know exactly which script to open. You don't have to scroll through a 2,000-line master script.
* **Highly Reusable:** The core "StateMachine" and "State" classes can be used for AI, Player Controllers, UI Managers, or even entire Game Flow managers (e.g., MainMenuState, GameplayState, PauseState).

---

## 🧠 Design Notes

Beginner AI scripts usually put everything in "Update()". Over time, this becomes a tangled mess of boolean flags like "isJumping", "isAttacking", and "isDead".

This system uses the **State Pattern**. 
The "StateMachine" is a simple container that holds the "CurrentState". Every frame, the "EnemyController" calls "StateMachine.Tick()". The State Machine blindly passes that call to whatever the "CurrentState" happens to be. 
By passing the "EnemyController" reference into the states via their constructors, the states can freely read the enemy's variables (like speed or health) and trigger animations, while remaining completely decoupled from each other.

---

## 📂 Included Scripts

* "State.cs" - The abstract base class featuring virtual "Enter()", "Tick()", "FixedTick()", and "Exit()" methods.
* "StateMachine.cs" - The brain that manages the current state and gracefully handles transitioning between them.
* "EnemyController.cs" - An example MonoBehaviour that initializes the State Machine and holds the data (variables, components) the states need to read.
* "EnemyStates.cs" - Example implementations ("IdleState", "ChaseState") showing how to write logic and trigger transitions.

---

## 🧩 How To Use

1. **Setup the Controller:** Create a new script for your object (e.g., "PlayerController" or "BossController").
2. **Instantiate the Machine:** Inside "Awake()", create a new instance of the State Machine:

"""
public StateMachine sm;

void Awake() 
{
    sm = new StateMachine();
}
"""

3. **Create Your States:** Create standard C# classes that inherit from "State". Pass your controller and the state machine into their constructors.
4. **Initialize:** In your controller's "Start()" method, call "sm.Initialize(yourStartingState);".
5. **Tick the Machine:** In your controller's "Update()" loop, simply call "sm.Tick();".

---

## 🚀 Possible Extensions

* **State Actions & Decisions:** To make it *hyper-modular* for designers, you can convert "State" into a ScriptableObject, allowing designers to plug in modular "Action" (Move, Shoot) and "Decision" (LineOfSight, TimePassed) ScriptableObjects in the inspector.
* **Animation Hooks:** Pass an "Animator" into the state machine, and automatically trigger animation hash strings inside the "Enter()" and "Exit()" methods.
* **Sub-State Machines:** For complex characters (like a Player), you can have states contain their own State Machines (e.g., "AirborneState" contains a sub-machine that switches between "Jumping", "Falling", and "Gliding").

---

## 🛠 Unity Version

* Tested in Unity6+ (should work without any problems in newer versions)

---

## 📜 License

MIT