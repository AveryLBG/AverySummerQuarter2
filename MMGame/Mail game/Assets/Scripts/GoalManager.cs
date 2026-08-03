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
    [SerializeField] public RectTransform BTImage;
    [SerializeField] public RectTransform RTImage;

    [SerializeField] private GameManager GameManager; 
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private InputActionAsset InputActions;
    [SerializeField] private AudioManager AudioManager;
    private float random;
    
    

    private InputAction menuAction;
    private InputAction rtauntAction;
    private InputAction btauntAction;
    public static GoalManager Instance {get; private set;}
    private bool menuOpen = false;

    public int blueWins {get; private set;}
    public int redWins {get; private set;}
    private float screenhMult;
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
        rtauntAction = InputSystem.actions.FindAction("RTaunt");
        btauntAction = InputSystem.actions.FindAction("BTaunt");

        //Gets the saved data
        blueWins = PlayerPrefs.GetInt("BlueWins", 0);
        redWins = PlayerPrefs.GetInt("RedWins", 0);
        screenhMult = Screen.height / 1080f;
        //Debug.Log("Screen Height in Pixels: " + Screen.height);
        
        
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
                        AudioManager.PlaySound("Score");
                    }
                    if (player2 != null)
                    {
                        BlueScore += 1;
                        StartCoroutine(BlueCrowd());
                        AudioManager.PlaySound("Score");

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
        if (btauntAction.WasPressedThisFrame())
        {
            random = Random.value;
            StartCoroutine(BTaunt());
            if (random <= 0.1)
            {
                AudioManager.PlaySound("Taunt3");
            }
            else
            {
                if (random <= 0.75)
                {
                    AudioManager.PlaySound("Taunt");
                }
                else
                {
                AudioManager.PlaySound("Taunt2"); 
                }
            }

        }
        if (rtauntAction.WasPressedThisFrame())
        {
            random = Random.value;
            StartCoroutine(RTaunt());
            if (random <= 0.1)
            {
                AudioManager.PlaySound("Taunt3");
            }
            else
            {
                if (random <= 0.75)
                {
                    AudioManager.PlaySound("Taunt");
                }
                else
                {
                AudioManager.PlaySound("Taunt2"); 
                }
            }
        }


    }
    private IEnumerator BlueCrowd() //CHANGE ALL OF THIS TO USE SCREEN %
    {
        
        RectTransform rectTransform = BlueImage.GetComponent<RectTransform>();
        for (float i = 0; i < 250; i+= 2 * screenhMult)
        {
           rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, i  - 750 ); 
           yield return new WaitForSeconds(0.001f); 
        }
        
        for (float i = 250; i < 300; i+= 1 * screenhMult )
        {
           rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, i - 750); 
           yield return new WaitForSeconds(0.001f); 
        }
        for (float i = 300; i > 250; i-= 1.5f * screenhMult )
        {
           rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, i - 750); 
           yield return new WaitForSeconds(0.001f); 
        }

        
        for (float i = 250; i > 0; i-= 2 * screenhMult)
        {
           rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, i - 750); 
           yield return new WaitForSeconds(0.001f); 
        }


    }
    private IEnumerator RedCrowd()
    {
        RectTransform rectTransform = RedImage.GetComponent<RectTransform>();
        for (float i = 0; i < 250; i+= 2 * screenhMult)
        {
           rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, i  - 780); 
           yield return new WaitForSeconds(0.001f); 
        }
        
        for (float i = 250; i < 300; i+= 1 * screenhMult )
        {
           rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, i - 780); 
           yield return new WaitForSeconds(0.001f); 
        }
        for (float i = 300; i > 250; i-= 1.5f * screenhMult )
        {
           rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, i - 780); 
           yield return new WaitForSeconds(0.001f); 
        }

        
        for (float i = 250; i > 0; i-= 2 * screenhMult)
        {
           rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, i - 780); 
           yield return new WaitForSeconds(0.001f); 
        }

        //Debug.Log(screenhMult);
        
        
    }
    private IEnumerator BTaunt()
    {
        RectTransform rectTransform = BTImage.GetComponent<RectTransform>();
        for (float i = 0; i < 250; i+= 2 * screenhMult)
        {
           rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, i  - 750); 
           yield return new WaitForSeconds(0.001f); 
        }
        
        for (float i = 250; i < 300; i+= 1 * screenhMult )
        {
           rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, i - 750); 
           yield return new WaitForSeconds(0.001f); 
        }
        for (float i = 300; i > 250; i-= 1.5f * screenhMult )
        {
           rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, i - 750); 
           yield return new WaitForSeconds(0.001f); 
        }

        
        for (float i = 250; i > 0; i-= 2 * screenhMult)
        {
           rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, i - 750); 
           yield return new WaitForSeconds(0.001f); 
        }

        //Debug.Log(screenhMult);
        
        
    }
    private IEnumerator RTaunt()
    {
        RectTransform rectTransform = RTImage.GetComponent<RectTransform>();
        for (float i = 0; i < 250; i+= 2 * screenhMult)
        {
           rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, i  - 750); 
           yield return new WaitForSeconds(0.001f); 
        }
        
        for (float i = 250; i < 300; i+= 1 * screenhMult )
        {
           rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, i - 750); 
           yield return new WaitForSeconds(0.001f); 
        }
        for (float i = 300; i > 250; i-= 1.5f * screenhMult )
        {
           rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, i - 750); 
           yield return new WaitForSeconds(0.001f); 
        }

        
        for (float i = 250; i > 0; i-= 2 * screenhMult)
        {
           rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, i - 750); 
           yield return new WaitForSeconds(0.001f); 
        }

        //Debug.Log(screenhMult);
        
        
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

