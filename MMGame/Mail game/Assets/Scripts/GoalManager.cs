using UnityEngine;

public class GoalManager : MonoBehaviour
{
      
    [SerializeField] public int BlueScore = 0;
    [SerializeField] public int RedScore = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("DIH CHEESE");
        //Debug.Log("Something hit the pad!");
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        Player2Controller player2 = collision.gameObject.GetComponent<Player2Controller>();
        if (player != null || player2 != null) 
        {
           {
           // Grab and store that players rigid body compoenent
            Rigidbody rb = collision.rigidbody;
            
            if (rb == null) return;
                {
                    
                    collision.transform.position = new Vector3(0,10,0);
                    rb.linearVelocity = Vector3.zero; 
                    if (player != null)
                    {
                        RedScore += 1;
                    }
                    if (player2 != null)
                    {
                        BlueScore += 1;
                    }
                    if (BlueScore >= 3);
                    {
                        //show UI
                        //Use wait coroutine
                        //end game                        
                        
                    }
                    if (RedScore >= 3);
                    {
                        //show UI
                        //Use wait coroutine
                        //end game
                        
                    }
        
                }

           }

        }

    }
    
}

