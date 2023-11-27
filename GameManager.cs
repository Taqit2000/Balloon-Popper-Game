using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  

  
  public TMP_Text scoreBox;
        public int currentScore=0; 
    private static  int playerScore = 0;
     public Transform spawnPoint;
     public GameObject NegativeBalloon; 
     public GameObject Balloon; 
     public float spawnInterval = 1.0f;
      private static int totalScore = 0;
  
     private int currentLevel = 1;
       // private List<int> topScores = new List<int>();
        public TMP_Text highScoreText;
        private bool displayHighScores = false;
        private int[] topScores = new int[5];

         public TMP_InputField playerNameInput;  
    private string playerName = "";
public TMP_Text playerNameText;

  void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
        //startTime = Time.time;
        UpdateScoreText();
        // StartCoroutine(SpawnBalloons());

        SpawnBalloons();
        LoadTopScores();
        UpdateHighScoreText();
         DisplayTopScores();

         playerName = PlayerPrefs.GetString("", "Player");
        UpdatePlayerNameText();
        
    }

    void Update()
{
  
     PlayerPoppedBalloon();
    UpdateScoreText();
    if (SceneManager.GetActiveScene().name == "Game Over")
    {
        displayHighScores = true;
    }
    else
    {
        displayHighScores = false;
    }

}

public void SetPlayerName()
    {
        // Update the player's name from the input field
        playerName = playerNameInput.text;

        // Save the player's name to PlayerPrefs
        PlayerPrefs.SetString("", playerName);
     
        PlayerPrefs.Save();

        // Update the UI to display the player's name
        UpdatePlayerNameText();
    }

      void UpdatePlayerNameText()
    {
        // Update the UI Text to display the player's name
        if (playerNameText != null)
        {
            playerNameText.text = "" + playerName;
        }
    }


 public void Play()
    {
        SceneManager.LoadScene("Level1"); 
        
    }

    IEnumerator SpawnBalloons()
    {

        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            
             Destroy(GameObject.FindWithTag("Balloon"));

        // Instantiate new balloons
        Vector3 fixedPos = new Vector3(3f, spawnPoint.position.y, spawnPoint.position.z);
        GameObject positiveBalloon = Instantiate(Balloon, fixedPos, Quaternion.identity);
        GameObject negativeBalloon = Instantiate(NegativeBalloon, spawnPoint.position, Quaternion.identity);

            
        }
    }

public void PlayerPoppedBalloon()
{
    

     if (BalloonController.instance != null )
    {
        
        if (BalloonController.instance.HasBeenHit())
        {
            TransitionToNextLevel();
        }

        else
        {
            
            Debug.Log("balloon not hit");
        }
    }

   else if (BalloonController1.instance != null )
    {
       
        if (BalloonController1.instance.HasBeenHit())
        {
            TransitionToNextLevel();
        }

        else
        {
            
            Debug.Log("Balloon not hit");
        }
    }
    
      else if (BalloonController2.instance != null )
    {
       
        if (BalloonController2.instance.HasBeenHit())
        {
            TransitionToNextLevel();
        }

        else
        {
           
            Debug.Log("Balloon not hit");
        }
    }
    
        
  
    else
    {

       
       // SceneManager.LoadScene("Game Over");
      //TransitionToNextLevel();
       
       //  Debug.LogError("instance is null");
    }
    
}


void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       
        UpdateScoreText();
    }
void TransitionToNextLevel()
{
    currentLevel++;

    if (currentLevel <= 3)
    {
        
        string nextLevelSceneName = "Level" + currentLevel;
       
       
        SceneManager.LoadScene(nextLevelSceneName);
         SpawnBalloons();
         AdjustBalloonParameters();
        
    }
    else
    {
        Debug.Log("Game Over");
         SceneManager.LoadScene("Game Over");
        //  DisplayTopScores();
       
    }
}



    void AdjustBalloonParameters()
    {
        

      //  BalloonController.instance.AdjustParametersForLevel(currentLevel);
        if (currentLevel==1){BalloonController.instance.AdjustParametersForLevel(currentLevel);}
        else if(currentLevel==2){BalloonController1.instance.AdjustParametersForLevel(currentLevel);}
        else{BalloonController2.instance.AdjustParametersForLevel(currentLevel);}
        
    }

    public void OnRestartButtonClicked()
{
    RestartLevel();
}

  public void OnMainMenuButtonClicked()
{
    SceneManager.LoadScene("Main Menu");
}

    void RestartLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        
          if (currentSceneName == "Game Over")
        {
           SceneManager.LoadScene("Main Menu");
        }
        else{SceneManager.LoadScene(currentSceneName);}
    }


    public void AddPoints(int points)
    {
      //  playerScore += points;
      currentScore+=points; 
        totalScore += points;
        UpdateScoreText();
         
        CheckTopScores(totalScore);

      
        SaveTopScores();
    }

      public void DeductPoints(int points)
    {
        playerScore = Mathf.Max(0, playerScore - points);
        UpdateScoreText();
    }


 void CheckTopScores(int score)
    {
        
        if (topScores.Any(s => score > s))
        {
           
            topScores[4] = score;

          
            System.Array.Sort(topScores);
            System.Array.Reverse(topScores);
        }
    }


    void SaveTopScores()
    {
        
        string topScoresString = string.Join(",", topScores);

       
        PlayerPrefs.SetString("TopScores", topScoresString);
        PlayerPrefs.Save();
    }


    void LoadTopScores()
    {
        
        string topScoresString = PlayerPrefs.GetString("TopScores", "");

        
        string[] scoreStrings = topScoresString.Split(',');

      
        for (int i = 0; i < Mathf.Min(scoreStrings.Length, topScores.Length); i++)
        {
            if (int.TryParse(scoreStrings[i], out int score))
            {
                topScores[i] = score;
            }
        }
    }

    void UpdateHighScoreText()
    {
      
        if (highScoreText != null)
        {
            highScoreText.text = "Top 5 Scores:\n";

            for (int i = 0; i < topScores.Length; i++)
            {
                highScoreText.text += (i + 1) + ". " + topScores[i] + "\n";
            }
        }
    }


    void DisplayTopScores()
    {
        
        System.Array.Sort(topScores);
        System.Array.Reverse(topScores);

       
        if (highScoreText != null)
        {
            highScoreText.text = "Top 5 Scores:\n";

            for (int i = 0; i < topScores.Length; i++)
            {
                highScoreText.text += (i + 1) + ". " + topScores[i] + "\n";
            }
        }
    }

   public void UpdateScoreText()
    {
       

     if (scoreBox == null)
    {
        scoreBox = GameObject.Find("ScoreBox").GetComponent<TMP_Text>();

       
        if (scoreBox == null)
        {
            Debug.LogError("scoreBox not found or assigned in the GameManager.");
            return;
        }
    }

    
    if (scoreBox != null)
    {
        scoreBox.text = "Score: " + currentScore.ToString();
    }
}







}