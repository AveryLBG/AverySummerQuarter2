using UnityEngine;
using UnityEngine.InputSystem; //imports the input system into the script
using System.Collections;

public class DeathChecker : MonoBehaviour
{
      

    [SerializeField] private GameObject BFX;
    [SerializeField] private GameObject RFX;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnCollisionEnter(Collision collision)
    {

        //Debug.Log("Something hit the pad!");
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        Player2Controller player2 = collision.gameObject.GetComponent<Player2Controller>();
        if (player != null) 
        {
           {
           // Grab and store that players rigid body compoenent
            Rigidbody rb = collision.rigidbody;

            if (rb == null) return;

            
                //get the point of contact
                Vector3 hitpoint = collision.contacts[0].point;

                //get bounce direction
                //Vector3 fxDirection = contact.normal;

                BFX.transform.position = hitpoint;

                StartCoroutine(BlueFlash());
                
                
                
                
           }

        }
        if (player2 != null)
        {
           {
           // Grab and store that players rigid body compoenent
            Rigidbody rb = collision.rigidbody;

            if (rb == null) return;

            
                //get the point of contact
                Vector3 hitpoint = collision.contacts[0].point;

                //get bounce direction
                //Vector3 fxDirection = contact.normal;

                RFX.transform.position = hitpoint;

                StartCoroutine(RedFlash());
                
                
                
                
           }
        }

        

    }
    private IEnumerator BlueFlash()
    {
        BFX.SetActive(true);
        yield return new WaitForSeconds(1f);
        BFX.SetActive(false);
    }
    private IEnumerator RedFlash()
    {
        RFX.SetActive(true);
        yield return new WaitForSeconds(1f);
        RFX.SetActive(false);
    }
    
}
