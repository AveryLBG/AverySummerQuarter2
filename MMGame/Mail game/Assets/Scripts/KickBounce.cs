using UnityEngine;

public class KickBounce : MonoBehaviour
{
      


    [SerializeField] private float bounciness = 20f;
    [SerializeField]private GameObject target; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

            
                //get the point of contact
                ContactPoint contact = collision.contacts[0];

                //get bounce direction
                Vector3 bounceDirection = contact.normal;

                // Bounce their asss using their rb component
                //Uses negative values because the pad bounces in the wrong direction.
                rb.AddForce(-0.1f * bounciness  * bounceDirection * target.GetComponent<Rigidbody>().linearVelocity.magnitude, ForceMode.Impulse);
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
                rb.AddForce( bounciness  * forwardDirection, ForceMode.Impulse);
                

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
    private void FixedUpdate()
    {
            if (bounciness > 17f)
            {
                bounciness -= 0.01f;
                //Debug.Log("B:" + bounciness );
            }
    }
    
}
