using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using TMPro;

public class PlayerNameInput : MonoBehaviour
{
    public TMP_InputField playerNameInputField;
    public TMP_Text playerNameText;

    private static PlayerNameInput instance;
    string playerName;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this); // Make this object persistent
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Optionally, you can set a default value or placeholder text here
        playerNameInputField.text = "";
    }

    public void OnNameEntered()
    {
         playerName = playerNameInputField.text;
        Debug.Log("Player's Name: " + playerName);

        // Save the player's name for use in other scenes
        PlayerPrefs.SetString("PlayerName", playerName);
    }

    public string GetPlayerName()
    {
        // Retrieve the player's name
        return PlayerPrefs.GetString("PlayerName", "Player");
    }


       void UpdatePlayerNameText()
    {
        // Update the UI Text to display the player's name
        if (playerNameText != null)
        {
            playerNameText.text = "Player: " + playerName;
        }
    }
}