using UnityEngine;
using UnityEngine.SceneManagement; // Enable this script to change scenes and more


public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject TutorialPanel;
    [SerializeField] private GameObject GWPanel;
    public void StartGame()
    {
        // Load the next scene in the build index (the game scene)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);    
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4); 
    }
    public void LoadMap1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }
    public void LoadMap2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
    public void LoadMap3()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}