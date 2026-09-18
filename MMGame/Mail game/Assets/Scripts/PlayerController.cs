using UnityEngine;
using UnityEngine.InputSystem; //imports the input system into the script
using System.Collections;
using UnityEngine.VFX;

public class PlayerController : MonoBehaviour
{
    
    
    [SerializeField, Tooltip("A variable to store the input action sheet we use for input.")] 
    private InputActionAsset InputActions;

    // ACTIONS
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;
 
    private Vector2 moveInput;

    //LOGIC
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 10f;

    [SerializeField] private GameObject indic;
    //[SerializeField] float speed; //DELETE LATER
    private bool slamcd1 = true;
    private bool wavedash1 = false;

   
    private float speed;

    // COMPONENTS
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject leg;

    // PLAYER SETTINGS
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private ParticleSystem wavedashParticles;

    



    // Awake is called when the script is loaded.
    private void Awake()
    {
        //Assign our input action variables to their respective input actions
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        attackAction = InputSystem.actions.FindAction("Attack");

        // Assign the rb variable to the player's rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        //Turn on the player Action Map when this is enabled
        InputActions.FindActionMap("Player")?.Enable();
    }



     private void OnDisable()
    {
        //Turn on the player Action Map when this is disabled
        InputActions.FindActionMap("Player")?.Disable();
    }
    
    //Update= 1 per frame, so 60-120 per second.
    private void Update()
    {
        if (GameManager.isGameOver)
            {
                return;
            }
        // Read & store movement
        moveInput = moveAction.ReadValue<Vector2>();

        if (jumpAction.WasPressedThisFrame())
        {
            //Tell the player to jump.
            HandleJump();
        }
        
        //speed = rb.linearVelocity.magnitude;
       

    }

    //Fixed update happens 50 time per second no matter what
    private void FixedUpdate()
    {
        HandleMovement();
        if (rb.linearVelocity.y < 0.5)
        {


            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y - 0f, rb.linearVelocity.z);
            
            
        }

    }

    //Cooldown handler
    IEnumerator CooldownSequence()
    {
        slamcd1 = false;
        wavedash1 = true;
        indic.SetActive(false);

         //Pause for 0.5 seconds
        yield return new WaitForSeconds(0.5f);
        wavedash1 = false;
        //Reset mass
        rb.mass = 1;


        // Pause the coroutine for 2 seconds
        yield return new WaitForSeconds(2f);
        
        slamcd1 = true;
        indic.SetActive(true);

    
    }
    

    private void HandleMovement()
    {
        //Calculate and store the direction the player will move based on the input
        //Vector3 moveDirection = new Vector3 transform.forward * moveInput.y + transform.right * moveInput.x;
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
        // Prevent diagonals from being faster
        moveDirection.Normalize();
        // Apply the movement of the player.
       
            rb.AddForce(moveDirection * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);

    }

    

    private void HandleJump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            if (wavedash1)
            {
                //Debug.Log("Wavedash");
                
                //StartCoroutine(Wavedash());
                wavedashParticles.transform.position = transform.position;
                wavedashParticles.Play();
                rb.linearVelocity = leg.transform.forward * rb.linearVelocity.magnitude;
                rb.AddForce(Vector3.up * jumpForce * 3f, ForceMode.Impulse);
                rb.AddForce(leg.transform.forward * (rb.linearVelocity.magnitude + 50f), ForceMode.Impulse);
                
            }
        }
        else
        {
            if (slamcd1)
            {
                speed = rb.GetComponent<Rigidbody>().linearVelocity.magnitude;
                rb.AddForce(Vector3.down * jumpForce * 1f, ForceMode.Impulse);
                rb.AddForce(leg.transform.forward * 5f, ForceMode.Impulse);
                rb.mass = 10f;
                StartCoroutine(CooldownSequence());
            }
        }
        
    }

    public bool IsGrounded()
    {
          //Draw the raycast for debug
        //Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance);
    
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
      
    }
}
   
