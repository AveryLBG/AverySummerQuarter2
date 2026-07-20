using UnityEngine;
using UnityEngine.SceneManagement; // Enable this script to change scenes and more


public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject TutorialPanel;
    [SerializeField] private GameObject GWPanel;
    public void StartGame()
    {
        // Load the next scene in the build index (the game scene)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);    
    }

    public void ExitGame()
    {
        // Close the game application
        Application.Quit();
    }

    public void OpenTutorial()
    {
        TutorialPanel.SetActive(true);
    }

    public void CloseTutorial()
    {
        TutorialPanel.SetActive(false);
    }
        public void OpenStats()
    {
        GWPanel.SetActive(true);
    }

    public void CloseStats()
    {
        GWPanel.SetActive(false);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); 
    }

}