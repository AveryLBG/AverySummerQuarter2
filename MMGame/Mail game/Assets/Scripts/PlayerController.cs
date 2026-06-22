using UnityEngine;
using UnityEngine.InputSystem; //imports the input system into the script

public class PlayerController : MonoBehaviour
{
    
    [SerializeField, Tooltip("A variable to store the input action sheet we use for input.")] 
    private InputActionAsset InputActions;

    // ACTIONS
    private InputAction moveAction;
    private InputAction jumpAction;
 
    private Vector2 moveInput;

    // COMPONENTS
    [SerializeField] private Rigidbody rb;

    // PLAYER SETTINGS
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;


    // Awake is called when the script is loaded.
    private void Awake()
    {
        //Assign our input action variables to their respective input actions
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        // Assign the rb variable to the player's rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        //Turn on the player Action Map when this is enabled
        InputActions.FindActionMap("Player").Enable();
    }



     private void OnDisable()
    {
        //Turn on the player Action Map when this is disabled
        InputActions.FindActionMap("Player").Disable();
    }
    
    //Update= 1 per frame, so 60-120 per second.
    private void Update()
    {
        // Read & store movement
        moveInput = moveAction.ReadValue<Vector2>();

        if (jumpAction.WasPressedThisFrame())
        {
            //Tell the player to jump.
            HandleJump();
        }

    }

    //Fixed update happens 50 time per second no matter what
    private void FixedUpdate()
    {
        HandleMovement();

    }

    private void HandleMovement()
    {
        //Calculate and store the direction the player will move based on the input
        Vector3 moveDirection = transform.forward * moveInput.y + transform.right * moveInput.x;
        // Prevent diagonals from being faster
        moveDirection.Normalize();
        // Apply the movement of the player.
        rb.MovePosition(+ moveDirection * moveSpeed * Time.deltaTime);

    }

    private void HandleJump()
    {
        Debug.Log("Jump my bones");
    }
}
