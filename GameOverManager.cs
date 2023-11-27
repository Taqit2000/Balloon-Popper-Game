using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // Add a reference to your "Main Menu" scene in the Inspector
   // public string mainMenuSceneName = "MainMenu";

   [SerializeField] private string mainMenuSceneName = "Main Menu";

    public void LoadMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene(mainMenuSceneName);
    }
}