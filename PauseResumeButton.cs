using UnityEngine;
using UnityEngine.UI;

public class PauseResumeButton : MonoBehaviour
{
    private bool isPaused = false;
    private Button pauseResumeButton;
    private Text buttonText;

    void Start()
    {
        // Get the Button component and add a listener to the OnClick event
        pauseResumeButton = GetComponent<Button>();
        if (pauseResumeButton != null)
        {
            buttonText = pauseResumeButton.GetComponentInChildren<Text>();
            pauseResumeButton.onClick.AddListener(OnClick);
            UpdateButtonText();
        }
    }

    void OnClick()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }

        // Toggle the pause state
        isPaused = !isPaused;
        UpdateButtonText();
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // Set time scale to 0 to pause the game
        Debug.Log("Game Paused");
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; // Set time scale back to 1 to resume the game
        Debug.Log("Game Resumed");
    }

    void UpdateButtonText()
    {
        // Update the button text based on the game state
        if (buttonText != null)
        {
            buttonText.text = isPaused ? "Resume" : "Pause";
        }
    }
}