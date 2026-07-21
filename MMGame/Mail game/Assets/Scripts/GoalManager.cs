using UnityEngine;
using UnityEngine.UI;
using TMPro; // Required namespace for TextMesh Pro
using System.Collections;  //for Coroutines
using UnityEngine.InputSystem; //imports the input system into the script

public class GoalManager : MonoBehaviour
{   
    [SerializeField] public int BlueScore = 0;
    [SerializeField] public int RedScore = 0;
    [SerializeField] public TextMeshProUGUI BlueScoretext;
    [SerializeField] public TextMeshProUGUI RedScoretext;
    [SerializeField] public TextMeshProUGUI WinnerAlert;

    [SerializeField] public RectTransform RedImage;
    [SerializeField] public RectTransform BlueImage;

    [SerializeField] private GameManager GameManager; 
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private InputActionAsset InputActions;
    

    private InputAction menuAction;
    public static GoalManager Instance {get; private set;}
    private bool menuOpen = false;

    public int blueWins {get; private set;}
    public int redWins {get; private set;}
    //1. Players play
    //2. When the game is over calc liftime wins
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
        ToggleGameOverUI(false);
        menuAction = InputSystem.actions.FindAction("Menu");

        //Gets the saved data
        blueWins = PlayerPrefs.GetInt("BlueWins", 0);
        redWins = PlayerPrefs.GetInt("RedWins", 0);
    }
      

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnCollisionEnter(Collision collision)
    {
    
        //Debug.Log("Something hit the pad!");
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        Player2Controller player2 = collision.gameObject.GetComponent<Player2Controller>();
        //if (player != null || player2 != null) 
        {
           {
           // Grab and store that players rigid body compoenent
            Rigidbody rb = collision.rigidbody;
            
            if (rb == null) return;
                {
                    
                    collision.transform.position = new Vector3(0,10,0);
                    rb.linearVelocity = Vector3.zero; 
                    rb.angularVelocity = Vector3.zero;
                    if (player != null)
                    {
                        RedScore += 1;
                        StartCoroutine(RedCrowd());
                    }
                    if (player2 != null)
                    {
                        BlueScore += 1;
                        StartCoroutine(BlueCrowd());

                    }
                    if (BlueScore >= 3)
                    {
                        BlueWin();
                                            
                        
                    }
                    if (RedScore >= 3)
                    {
                       
                       RedWin();
                       
                        
                    }
        
                }

           }

        }

    }
    private void Update()
    {
        BlueScoretext.text = BlueScore.ToString();
        RedScoretext.text = RedScore.ToString();
        if (menuAction.WasPressedThisFrame())
        {
            if (menuOpen)
            {
                GameOverPanel.SetActive(false);
                menuOpen = false;
            }
            else
            {
               GameOverPanel.SetActive(true);
               menuOpen = true;
            }
            
        }

    }
    private IEnumerator BlueCrowd() //CHANGE ALL OF THIS TO USE SCREEN %
    {
        
        RectTransform rectTransform = BlueImage.GetComponent<RectTransform>();
        for (int i = 0; i < 250; i+= 2)
        {
           rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, i - 770); 
           yield return new WaitForSeconds(0.001f); 
        }
        for (int i = 250; i < 300; i++ )
        {
           rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, i - 770); 
           yield return new WaitForSeconds(0.001f); 
        }
        for (int i = 300; i > 250; i-- )
        {
           rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, i -770); 
           yield return new WaitForSeconds(0.001f); 
        }

        
        for (int i = 250; i > 0; i-= 2)
        {
           rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, i -770); 
           yield return new WaitForSeconds(0.001f); 
        }


    }
    private IEnumerator RedCrowd()
    {
        RectTransform rectTransform = RedImage.GetComponent<RectTransform>();
        for (int i = 0; i < 250; i+= 2)
        {
           rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, i - 780); 
           yield return new WaitForSeconds(0.001f); 
        }
        
        for (int i = 250; i < 300; i++ )
        {
           rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, i - 780); 
           yield return new WaitForSeconds(0.001f); 
        }
        for (int i = 300; i > 250; i-- )
        {
           rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, i -780); 
           yield return new WaitForSeconds(0.001f); 
        }

        
        for (int i = 250; i > 0; i-= 2)
        {
           rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, i -780); 
           yield return new WaitForSeconds(0.001f); 
        }

        
        
        
    }
    private void BlueWin()
    {
        WinnerAlert.text = "Blue Won!";
        blueWins ++;
        PlayerPrefs.SetInt("BlueWins", blueWins);
        PlayerPrefs.Save();
        GameManager.GameOver();
    }
    private void RedWin()
    {
        WinnerAlert.text = "Red Won";
        redWins ++;
        PlayerPrefs.SetInt("RedWins", redWins);
        PlayerPrefs.Save();
        GameManager.GameOver();
    }
    public void ToggleGameOverUI(bool flag)
    {
        GameOverPanel.SetActive(flag);
    }
    
}

