// using UnityEngine;

// public class PlayerMovementController:SuperStateMachine
// {
//     [Header("Components")]
//     private PlayerController playerController;

//     [Header("Movement")]
//     public float movementAcceleration = 90.0f;
//     public float runSpeed = 6f;
//     private readonly float rotationSpeed = 40f;
//     public float groundFriction = 50f;
//     [HideInInspector] public Vector3 currentVelocity;

//     [HideInInspector] public Vector3 lookDirection { get; private set; }

//     private void Start()
//     {
//         playerController = GetComponent<PlayerController>();
        
//         // Set currentState to idle on startup.
//         currentState = PlayerState.Idle;
//     }

//     #region Updates

//     /*void Update () {
//         * Update is normally run once on every frame update. We won't be using it in this case, since the SuperCharacterController component sends a callback Update called SuperUpdate. 
//         * SuperUpdate is recieved by the SuperStateMachine, and then fires further callbacks depending on the state.
//     }*/

//     // Put any code in here you want to run BEFORE the state's update function. 
//     // This is run regardless of what state you're in.
//     protected override void EarlyGlobalSuperUpdate()
//     {
//     }

//     // Put any code in here you want to run AFTER the state's update function.  
//     // This is run regardless of what state you're in.
//     protected override void LateGlobalSuperUpdate()
//     {
//         // Move the player by our velocity every frame.
//         transform.position += currentVelocity * playerController.superCharacterController.deltaTime;

//         // If alive and is moving, set animator.
//         if (playerController.canMove) {
//             if (currentVelocity.magnitude > 0 && playerController.HasMoveInput()) {
//                 playerController.isMoving = true;
//                 playerController.SetAnimatorBool("Moving", true);
//                 playerController.SetAnimatorFloat("Velocity", currentVelocity.magnitude);
//             } else {
//                 playerController.isMoving = false;
//                 playerController.SetAnimatorBool("Moving", false);
//                 playerController.SetAnimatorFloat("Velocity", 0);
//             }
//         }

//         RotateTowardsMovementDir();

//         // Update animator with local movement values.
//         playerController.SetAnimatorFloat("Velocity", transform.InverseTransformDirection(currentVelocity).z);
//     }

//     #endregion

//     #region States

//     // Below are the state functions. 
//     // Each one is called based on the name of the state, so when currentState = Idle, we call Idle_EnterState. 
//     // If currentState = Jump, we call Jump_SuperUpdate()
//     private void Idle_EnterState()
//     {
//         playerController.SetAnimatorBool("Moving", false);
//     }

//     // Run every frame we are in the idle state.
//     private void Idle_SuperUpdate()
//     {
//         if (playerController.HasMoveInput() && playerController.canMove) {
//             currentState = PlayerState.Move;
//             return;
//         }
//         // Apply friction to slow to a halt.
//         currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, groundFriction 
//             * playerController.superCharacterController.deltaTime);
//     }

//     // Run once when exit the idle state.
//     private void Idle_ExitState()
//     {
//     }

//     // Run once when exit the idle state.
//     private void Idle_MoveState()
//     {
//         playerController.SetAnimatorBool("Moving", true);
//     }

//     private void Move_SuperUpdate()
//     {
//         // Set speed determined by movement type.
//         if (playerController.HasMoveInput() && playerController.canMove) {
//             currentVelocity = Vector3.MoveTowards(currentVelocity, playerController.moveInput 
//                 * runSpeed, movementAcceleration 
//                 * playerController.superCharacterController.deltaTime);
//         } else {
//             currentState = PlayerState.Idle;
//         }
//     }

//     #endregion

//     /// <summary>
//     /// Rotate towards the direction the Player is moving.
//     /// </summary>
//     private void RotateTowardsMovementDir()
//     {
//         if (playerController.moveInput != Vector3.zero) {
//             transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerController.moveInput), Time.deltaTime * rotationSpeed);
//         }
//     }
// }
