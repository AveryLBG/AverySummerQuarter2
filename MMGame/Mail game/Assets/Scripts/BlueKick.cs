using UnityEngine;

public class BlueKick : MonoBehaviour

{
  
   
   [SerializeField]private GameObject target; 
   [SerializeField] private float kickForce = 5f;
   private void Update()
   {
      transform.LookAt(target.transform);

      if (IsRunning)
      {
      
      transform.position += transform.forward * kickForce * Time.deltaTime;


      }
      if (IsRunning2)
      {
         
         transform.position += transform.forward * -1 * kickForce * Time.deltaTime;
         
      }
   }
   private bool IsRunning = false;
   private bool IsRunning2 = false;
   private void GoBack()
   {
           if (!IsRunning)
      {
         IsRunning2 = true;
      }
      
      
      
   }
   

   public void HandleKick()
    {
      if (!IsRunning)
      {
         IsRunning = true;
      }
      
      
    }
}
