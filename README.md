# Unity Modular Finite State Machine (FSM)

A clean, class-based Finite State Machine for Unity. This system completely replaces messy "if/else" and "switch" statements in your AI and Player controllers, separating every behavior into highly organized, independent classes. It fully supports **Hierarchical Sub-States** for complex character logic.

---

## ✨ Features

* **Class-Based States:** Behaviors are written as pure C# classes rather than heavy MonoBehaviours, making the system highly memory efficient.
* **Hierarchical State Machines (HFSM):** Includes a "SuperState" class. Top-level states can seamlessly manage their own internal sub-states (e.g., an "Airborne" state that internally manages "Jumping" and "Falling").
* **Separation of Concerns:** Each state handles its own logic and transition rules. The main controller just tells the State Machine to "Tick()".
* **Easy Debugging:** Because states are isolated, if an enemy's "Chase" logic breaks, you know exactly which script to open.
* **Highly Reusable:** The core "StateMachine" and "State" classes can be used for AI, Player Controllers, UI Managers, or even Game Flow managers.

---

## 🧠 Design Notes

Beginner scripts usually put everything in "Update()". Over time, this becomes a tangled mess of boolean flags like "isJumping", "isAttacking", and "isDead".

This system uses the **State Pattern**. 
The "StateMachine" is a simple container that holds the "CurrentState". Every frame, the controller calls "StateMachine.Tick()". The State Machine blindly passes that call to whatever the "CurrentState" happens to be. 

By adding the **SuperState** architecture, we eliminate redundant code. Instead of checking if the player hits the ground in the "JumpingState", "FallingState", and "GlidingState", we just put that transition logic into the parent "AirborneState". If the parent transitions, it gracefully exits the active sub-state for you.

---

## 📂 Included Scripts

* "State.cs" - The abstract base class featuring virtual "Enter()", "Tick()", "FixedTick()", and "Exit()" methods.
* "StateMachine.cs" - The brain that manages the current state and gracefully handles transitioning between them.
* "SuperState.cs" - An advanced base class that contains a secondary "StateMachine" for managing complex sub-hierarchies.
* "PlayerController.cs" - An example MonoBehaviour that initializes the State Machine and holds data for the states to read.
* "PlayerStates.cs" - Example implementations showcasing a top-level "Grounded" state and an "Airborne" SuperState running "Jumping/Falling" sub-states.

---

## 🧩 How To Use

1. **Setup the Controller:** Create a new script for your object (e.g., "PlayerController").
2. **Instantiate the Machine:** Inside "Awake()", create a new instance of the State Machine:

"
public StateMachine sm;

void Awake() 
{
    sm = new StateMachine();
}
"

3. **Create Your States:** Create standard C# classes that inherit from "State" or "SuperState". 
4. **Initialize Sub-States:** If using a "SuperState", override the "Enter()" method and call "subStateMachine.Initialize(myStartingSubState)".
5. **Initialize Main Machine:** In your controller's "Start()" method, call "sm.Initialize(yourStartingState);".
6. **Tick the Machine:** In your controller's "Update()" loop, simply call "sm.Tick();".

---

## 🚀 Possible Extensions

* **State Actions & Decisions:** To make it *hyper-modular* for designers, you can convert "State" into a ScriptableObject, allowing designers to plug in modular "Action" (Move, Shoot) and "Decision" (LineOfSight, TimePassed) ScriptableObjects in the inspector.
* **Animation Hooks:** Pass an "Animator" into the state machine, and automatically trigger animation hash strings inside the "Enter()" and "Exit()" methods.
* **Global States:** Modify the "StateMachine" to accept an "AnyState" transition condition (like taking damage) that interrupts the current state no matter what is currently running.

---

## 🛠 Unity Version

Tested in Unity6+ (should work without any problems in newer versions)

---

## 📜 License

MIT
