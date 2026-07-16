using UnityEngine;
using UnityEngine.UI;
using TMPro; // Required namespace for TextMesh Pro
using System.Collections;  //for Coroutines
using UnityEngine.InputSystem; //imports the input system into the script

public class GlobalScoreManager : MonoBehaviour
{   
    [SerializeField] public TextMeshProUGUI BlueScoretext;
    [SerializeField] public TextMeshProUGUI RedScoretext;
    public static GlobalScoreManager Instance {get; private set;}


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


        //Gets the saved data
        blueWins = PlayerPrefs.GetInt("BlueWins", 0);
        redWins = PlayerPrefs.GetInt("RedWins", 0);

        BlueScoretext.text = blueWins.ToString();
        RedScoretext.text = redWins.ToString();

    }
      

   
}

