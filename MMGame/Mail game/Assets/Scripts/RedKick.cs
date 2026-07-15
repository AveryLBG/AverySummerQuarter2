using UnityEngine;
using UnityEngine.InputSystem; //imports the input system into the script
using System.Collections;

public class RedKick : MonoBehaviour
{

   private bool RedLegOut = false;

   [SerializeField, Tooltip("Probably bad that this is seperate, but I'll learn whats correct eventually.")] 
   private InputActionAsset InputActions;
   [SerializeField, Tooltip("The player kicking")] private Transform targetObject;
   private InputAction attack2Action;
 
   private void Awake()
   {
      attack2Action = InputSystem.actions.FindAction("Attack2");
   }
   private void OnEnable()
   {
      attack2Action.Enable();
   }

   private void OnDisable()
   {
      attack2Action.Disable();
   }

  
   
   [SerializeField]private GameObject target; 
   [SerializeField] private float kickForce = 0.5f;
   private void Update()
   {
      if (GameManager.isGameOver)
            {
                return;
            }
         transform.LookAt(target.transform);
         if (attack2Action.WasPressedThisFrame())
         {
            //IF the leg is in, go out. 
            
            if(!RedLegOut)
            {
               
               
                        
            
         
                  StartCoroutine(WaitAndLogCoroutine2());
               

               
            
            }
            else
            {
            
            }
  
         
         }
         if (RedLegOut)
         {
            transform.position = targetObject.position + transform.forward * kickForce;
         }

   }
   IEnumerator WaitAndLogCoroutine2()
   {
   
        RedLegOut = true;
        

        // 3. This line pauses execution without freezing the game
        yield return new WaitForSeconds(0.1f);

        RedLegOut = false;
        //make it so that it gets shorter


      


   

    
   }
   
   private void LateUpdate()
   {
      if (targetObject != null)
      {
         if (!RedLegOut)
         {
            // Matches the exact position every frame
            transform.position = targetObject.position; 
         }
      }
   }
   
}
