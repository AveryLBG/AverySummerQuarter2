using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform Player1Transform;
    [SerializeField] private Transform Player2Transform;
    [SerializeField] private Transform mainCamera;
    
    
    void Update()
    {
        int targetLayer = LayerMask.GetMask("Default", "Ground");
        if (mainCamera != null && Player1Transform != null)
        {
            RaycastHit hit;
            if(Physics.Linecast(mainCamera.position, Player1Transform.position, out hit, targetLayer))
            {
                Debug.DrawLine(mainCamera.position, Player1Transform.position);
                Renderer renderer = hit.collider.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Debug.Log("WE HIT" + hit.collider.name);

                    Material mat = renderer.material;
                    Color currentColor = mat.GetColor("_BaseColor");
                    Debug.Log("color: "+ currentColor);
                    if (currentColor.a != 0.1f)
                    {
                     currentColor.a = 0.1f;
                    }
                    Debug.Log("new color: "+ currentColor);
                    mat.SetColor("_BaseColor", currentColor);

                   
                }
            }
            
        }
    }
    void LateUpdate()
    {
    
        
    }

}
