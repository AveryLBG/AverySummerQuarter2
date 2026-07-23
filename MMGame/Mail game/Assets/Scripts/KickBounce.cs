using UnityEngine;
using UnityEngine.InputSystem; //imports the input system into the script
using System.Collections;

public class KickBounce : MonoBehaviour
{
      

    [SerializeField, Tooltip("A variable to store the input action sheet we use for input.")] 
    private InputActionAsset InputActions;
    [SerializeField] private float bounciness = 20f;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject HitstopScreen;
    [SerializeField] private GameObject CritstopScreen;
    [SerializeField] private AudioManager AudioManager;
    private InputAction moveAction;
    private InputAction move2Action;
    private float random;


    
    public static bool isWaiting = false; 
    private float critical;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        
        moveAction = InputSystem.actions.FindAction("Move");
        move2Action = InputSystem.actions.FindAction("Move2");
    }
    void OnCollisionEnter(Collision collision)
    {

        //Debug.Log("Something hit the pad!");
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        Player2Controller player2 = collision.gameObject.GetComponent<Player2Controller>();
        RedBounceBox rbb = collision.gameObject.GetComponent<RedBounceBox>();
        BlueBounceBox bbb = collision.gameObject.GetComponent<BlueBounceBox>();
        

        KickBounce OtherLeg = collision.gameObject.GetComponent<KickBounce>();
        
        if (player != null || player2 != null) 
        {
           
           // Grab and store that players rigid body compoenent
            Rigidbody rb = collision.rigidbody;
            

            if (rb == null) return;
            else
            {
                random = Random.value;
                if (random <= 0.3)
                {
                    AudioManager.PlaySound("SoftHit3");
                }
                else
                {
                    if (random <= 0.6)
                    {
                        AudioManager.PlaySound("SoftHit2"); 
                    }
                    else
                    {
                        AudioManager.PlaySound("SoftHit");
                    }
                }
                


            }

                if (isWaiting) return;
                //get the point of contact
                ContactPoint contact = collision.contacts[0];

                //get bounce direction
                Vector3 bounceDirection = contact.normal;

                // Bounce their asss using their rb component
                //Uses negative values because the pad bounces in the wrong direction.
                rb.AddForce(-0.1f * bounciness  * bounceDirection * target.GetComponent<Rigidbody>().linearVelocity.magnitude, ForceMode.Impulse);
                if (target.GetComponent<Rigidbody>().linearVelocity.magnitude >= 10f)
                {
                    if (isWaiting) return;
                    critical = Random.value;
                    
                    if (critical <= 0.1 || target.GetComponent<Rigidbody>().linearVelocity.magnitude >= 20f)
                    {
                        AudioManager.PlaySound("Crit");
                        Debug.Log("CRITICAL HIT");  


                        if (player != null)
                        {
                            StartCoroutine(Cripple(true));
                        }
                        else
                        {
                            StartCoroutine(Cripple(false));
                        }
                        StartCoroutine(Weaken(rb));
                        StartCoroutine(ExecuteCritStop(0.5f + (target.GetComponent<Rigidbody>().linearVelocity.magnitude - 15f)/10f));



                    }
                    else
                    {
                        AudioManager.PlaySound("Hit");
                       
                        StartCoroutine(ExecuteHitStop(0.5f + (target.GetComponent<Rigidbody>().linearVelocity.magnitude - 15f)/10f));
                    }
                    
                    

                    
                }
                //Debug.Log("Grandparent: " + target);
                bounciness += 3f;
                //Debug.Log("B:" + bounciness );
                

            
           

        }
        if (rbb != null || bbb != null) 
        {
           
           // Grab and store that players rigid body compoenent
            Rigidbody rb = collision.rigidbody;
            

            if (rb == null) return;

            
                //get the point of contact
                ContactPoint contact = collision.contacts[0];

                //get bounce direction
                Vector3 forwardDirection = transform.parent.forward;

                // Bounce their asss using their rb component
                //Uses negative values because the pad bounces in the wrong direction.
                rb.AddForce( 3f * bounciness  * forwardDirection, ForceMode.Impulse);
                

                bounciness += 3f;
                //Debug.Log("B:" + bounciness );
                

            
           

        }
        if (collision.gameObject.tag == "Box")
        {
            // Grab and store that players rigid body compoenent
            Rigidbody rb = collision.rigidbody;
            

            if (rb == null) return;

            
                //get the point of contact
                ContactPoint contact = collision.contacts[0];

                //get bounce direction
                Vector3 forwardDirection = transform.parent.forward;

                // Bounce their asss using their rb component
                //Uses negative values because the pad bounces in the wrong direction.
                rb.AddForce(0.25f * bounciness  * forwardDirection, ForceMode.Impulse);
                

                bounciness += 3f;
                //Debug.Log("B:" + bounciness );    
        }
  

    }
    
    private IEnumerator ExecuteHitStop(float duration)
    {
        isWaiting = true;
        HitstopScreen.SetActive(true);
        Time.timeScale = 0f; // Freeze all game logic and physics

        // Must use Realtime because Time.timeScale is 0
        yield return new WaitForSecondsRealtime(duration); 

        Time.timeScale = 1f; // Restore normal speed
        HitstopScreen.SetActive(false);
        
        yield return new WaitForSecondsRealtime(5); 
        isWaiting = false;
    }
    private IEnumerator ExecuteCritStop(float duration)
    {
        isWaiting = true;
        CritstopScreen.SetActive(true);
        Time.timeScale = 0f; // Freeze all game logic and physics

        // Must use Realtime because Time.timeScale is 0
        yield return new WaitForSecondsRealtime(duration); 

        Time.timeScale = 1f; // Restore normal speed
        CritstopScreen.SetActive(false);
        
        yield return new WaitForSecondsRealtime(5); 
        isWaiting = false;
    }
    private IEnumerator Cripple(bool player2)
    {
        if (player2)
        {
            moveAction.Disable();
            yield return new WaitForSeconds(3f);
            moveAction.Enable();
        }
        else
        {
            move2Action.Disable();
            yield return new WaitForSeconds(3f);
            move2Action.Enable();
        }

    }
    private IEnumerator Weaken(Rigidbody rb)
    {
            rb.mass = 0.75f;
            yield return new WaitForSeconds(5f);
            rb.mass = 1f;

    }
    private void FixedUpdate()
    {
            if (bounciness > 17f)
            {
                bounciness -= 0.03f;
                //Debug.Log("B:" + bounciness );
            }
    }
    
}
