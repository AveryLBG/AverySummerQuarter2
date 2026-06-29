using UnityEngine;

public class BouncePad : MonoBehaviour
{
      


    [SerializeField] private float bounciness = 20f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnCollisionEnter(Collision collision)
    {

        //Debug.Log("Something hit the pad!");
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        Player2Controller player2 = collision.gameObject.GetComponent<Player2Controller>();
        if (player != null || player2 != null) 
        {
           {
           // Grab and store that players rigid body compoenent
            Rigidbody rb = collision.rigidbody;

            if (rb == null) return;

            
                //get the point of contact
                ContactPoint contact = collision.contacts[0];

                //get bounce direction
                Vector3 bounceDirection = contact.normal;

                // Bounce their asss using their rb component
                //Uses negative values because the pad bounces in the wrong direction.
                rb.AddForce(-1 * bounciness  * bounceDirection, ForceMode.Impulse);
           }

        }

    }
    
}
