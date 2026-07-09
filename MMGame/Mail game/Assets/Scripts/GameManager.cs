using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Stores the one (and only) instance of this script
    public static GameManager Instance {get; private set;}
    
    [SerializeField] public static bool isGameOver = false;
   

    private void Awake()
    {
        // Check our singleton
        if (Instance == null)
        {
            // Assign this instance of the script as THE instance
            Instance = this; 
        }
        else // There is already a GameManager assigned
        {
            // Destroy this extra copy of this script
            Destroy(gameObject);
        }
        isGameOver = false;
    } 

    public void GameOver()
    {
        // Trigger Lose state UI
        // ...
        if(isGameOver)
        {
            return; ///Do nothing if the game is over
        }
        else
        {
            isGameOver = true;
            GoalManager.Instance.ToggleGameOverUI(true);
            
            
            
        }
       
    }
    public void LoadCurrentScene() //restart the scene
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);  
    }

}