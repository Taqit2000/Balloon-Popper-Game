using UnityEngine;
using UnityEngine.UI;

public class BalloonVolumeControl : MonoBehaviour
{
    /*
    private AudioSource balloonAudio; // Reference to the Audio Source on the balloon
    public Slider volumeSlider; // Reference to the UI Slider

    void Start()
    {
        // Get the AudioSource component on the balloon dynamically
        balloonAudio = GetComponent<AudioSource>();

        // Initialize the slider value based on saved preferences or default value
        if (volumeSlider != null)
        {
            float savedVolume = PlayerPrefs.GetFloat("BalloonVolume", 1f); // Default volume is 1
            volumeSlider.value = savedVolume;
            SetBalloonVolume(savedVolume);
        }
    }

    // Called when the slider value changes
    public void SetBalloonVolume(float volume)
    {
        if (balloonAudio != null)
        {
            balloonAudio.volume = volume;
            PlayerPrefs.SetFloat("BalloonVolume", volume); // Save the volume preference
        }
    }
    */

 private AudioSource balloonAudio; // Reference to the Audio Source on the balloon
    public Slider volumeSlider; // Reference to the UI Slider

    void Start()
    {
        // Get the AudioSource component on the balloon dynamically
        balloonAudio = GetComponent<AudioSource>();

        // Find the Slider in the scene by name
        volumeSlider = GameObject.Find("Slider").GetComponent<Slider>();

        // Initialize the slider value based on saved preferences or default value
        if (volumeSlider != null)
        {
            float savedVolume = PlayerPrefs.GetFloat("BalloonVolume", 1f); // Default volume is 1
            volumeSlider.value = savedVolume;
            SetBalloonVolume(savedVolume);
        }
    }

    // Called when the slider value changes
    public void SetBalloonVolume(float volume)
    {
        if (balloonAudio != null)
        {
            balloonAudio.volume = volume;
            PlayerPrefs.SetFloat("BalloonVolume", volume); // Save the volume preference
        }
    }

    // Play the "game over" audio clip
    public void PlayGameOverSound()
    {
        if (balloonAudio != null)
        {
            // Assuming the audio clip is in a "Sounds" folder in the "Resources" folder
            AudioClip gameOverClip = Resources.Load<AudioClip>("game over");

            if (gameOverClip != null)
            {
                balloonAudio.clip = gameOverClip;
                balloonAudio.Play();
            }
            else
            {
                Debug.LogError("Audio clip 'game over' not found in Resources/Sounds folder.");
            }
        }
        else
        {
            Debug.LogError("AudioSource not found on the balloon object.");
        }
    }






}