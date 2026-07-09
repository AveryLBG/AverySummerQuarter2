using UnityEngine;
using UnityEngine.InputSystem; //imports the input system into the script
using System.Collections;

public class BlueKick : MonoBehaviour
{

   private bool BlueLegOut = false;
   

   [SerializeField, Tooltip("Probably bad that this is seperate, but I'll learn whats correct eventually.")] 
   private InputActionAsset InputActions;
   [SerializeField, Tooltip("The player kicking")] private Transform targetObject;
   private InputAction attackAction;
 
   private void Awake()
   {
      attackAction = InputSystem.actions.FindAction("Attack");
   }
   private void OnEnable()
   {
      attackAction.Enable();
   }

   private void OnDisable()
   {
      attackAction.Disable();
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
         if (attackAction.WasPressedThisFrame())
         {
            //IF the leg is in, go out. otherwise, go inwards.
            
            if(!BlueLegOut)
            {
            
            
               
            
               StartCoroutine(WaitAndLogCoroutine());
               

            
            
            }
         
         }


   }
   IEnumerator WaitAndLogCoroutine()
   {
  
        BlueLegOut = true;

      
        

        // 3. This line pauses execution without freezing the game
        yield return new WaitForSeconds(0.1f);

        BlueLegOut = false;



      
  
   }
   
   private void LateUpdate()
   {
      if (targetObject != null)
      {
         if (!BlueLegOut)
         {
            // Matches the exact position every frame
            transform.position = targetObject.position; 
         }
         else
         {
            transform.position = targetObject.position + transform.forward * kickForce;
         }
      }
   }
   
}
