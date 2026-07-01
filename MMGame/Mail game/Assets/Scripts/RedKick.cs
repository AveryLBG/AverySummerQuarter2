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
         transform.LookAt(target.transform);
         if (attack2Action.WasPressedThisFrame())
         {
            //IF the leg is in, go out. otherwise, go inwards.
            
            if(!RedLegOut)
            {
               
               
                        
               
               Debug.Log("kick2");
               StartCoroutine(WaitAndLogCoroutine2());
               

               
            
            }
         
         }

   }
   IEnumerator WaitAndLogCoroutine2()
   {
        Debug.Log("Leg out2");
        RedLegOut = true;
        transform.position = targetObject.position + transform.forward * kickForce;

        // 3. This line pauses execution without freezing the game
        yield return new WaitForSeconds(0.1f);

        RedLegOut = false;
        Debug.Log("Leg in2");
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
