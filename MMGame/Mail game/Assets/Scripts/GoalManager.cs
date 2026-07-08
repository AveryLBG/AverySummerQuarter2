using UnityEngine;
using UnityEngine.UI;
using TMPro; // Required namespace for TextMesh Pro
using System.Collections;  //for Coroutines

public class GoalManager : MonoBehaviour
{
    public static GoalManager Instance {get; private set;}
    private void Awake()
    {
        //Check singleton
        if (Instance == null)
        {
            Instance = this;
        
        }
        else
        {
            Destroy(gameObject);
        }
    }
      
    [SerializeField] public int BlueScore = 0;
    [SerializeField] public int RedScore = 0;
    [SerializeField] public TextMeshProUGUI BlueScoretext;
    [SerializeField] public TextMeshProUGUI RedScoretext;
    [SerializeField] public TextMeshProUGUI WinnerAlert;
    [SerializeField] private GameManager GameManager; 
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
                    if (BlueScore >= 3)
                    {
                        StartCoroutine(BlueWin());                     
                        
                    }
                    if (RedScore >= 3)
                    {
                       
                        StartCoroutine(RedWin());
                        
                    }
        
                }

           }

        }

    }
    private void Update()
    {
        BlueScoretext.text = BlueScore.ToString();
        RedScoretext.text = RedScore.ToString();

    }
    IEnumerator BlueWin()
    {
        WinnerAlert.text = "Blue Won!";
        yield return new WaitForSeconds(3f);
        GameManager.GameOver();
    }
    IEnumerator RedWin()
    {
        WinnerAlert.text = "Red Won";
        yield return new WaitForSeconds(3f);
        GameManager.GameOver();
    }
    
}

