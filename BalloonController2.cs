using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;


public class BalloonController2 : MonoBehaviour
{


 public float growthRate = 0.3f; 
    public float destroyTime = 20.0f; 

    private float currentSize = 1.0f; 
    private float startTime; 


    public float moveSpeed = 30.0f; 
    public float maxSize = 5.0f;  
   
    public float destroySize = 20.0f; 

    private int moveDirection = 1; 
     public int balloonPoints = 10;

    public string arrowTag = "arrow";
    
        [SerializeField] AudioSource audio;

    private bool hasBeenHit = false;
     
        public string popSoundPath = "sounds/game over";

        public int negativePoints = -5;
        private GameManager gameManager;
        public static BalloonController2 instance;

    
    /*
void Awake()
{
    if (instance == null)
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    else
    {
        Destroy(gameObject);
    }
}
*/
 private void Awake()
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
    {    gameManager = GameManager.instance;
      startTime = Time.time;
      if (audio == null)
            audio = GetComponent<AudioSource>();
 
    }

    void Update()
    {    MoveBalloon();
        GrowBalloon();
        CheckDestroy();
       GameManager.instance. UpdateScoreText();
    }

  

     void MoveBalloon()
    {
         //hasBeenHit = false;
       
        transform.Translate(Vector2.right * moveSpeed * moveDirection * Time.deltaTime);

        
        if (transform.position.x >= 7.0f) 
        {
            moveDirection = -1; 
        }
        else if (transform.position.x <= -7.0f) 
        {
            moveDirection = 1; 
        }
    }

    void GrowBalloon()
    {
        
        currentSize += growthRate * Time.deltaTime;
        transform.localScale = new Vector3(currentSize, currentSize, 1.0f);
    }

    void CheckDestroy()
    {
        
        if (Time.time - startTime >= destroyTime)
        {
            Destroy(gameObject);
        }
    }


void OnTriggerEnter2D(Collider2D other)
    {
           
    if (Time.time - startTime >= destroyTime)
    {
      
        if (!hasBeenHit)
        {
            GameManager.instance.AddPoints(0);  
        }

        Destroy(gameObject);
    }
     
    if (!hasBeenHit && other.CompareTag("arrow"))  
        {
           
            Destroy(other.gameObject);  

            if (Time.time - startTime <= 10.0f)
    {
        
         Debug.Log("Adding 20 points");
        GameManager.instance.AddPoints(20);
    }

    else
    {
        // If the balloon is hit after the first 10 seconds, give the player 10 points
        Debug.Log("Adding 10 points");
        GameManager.instance.AddPoints(10);
    }

            // Handle scoring
           // GameManager.instance.AddPoints(balloonPoints);  

           
            BurstBalloon();
           
             
            
            hasBeenHit = true;
        }
        AudioSource.PlayClipAtPoint(audio.clip, transform.position);
        
    }
void BurstBalloon()
    { 

        hasBeenHit = true;
         
      //  GameManager.instance.AddPoints(balloonPoints);
        Destroy(gameObject);
       GameManager.instance.PlayerPoppedBalloon();
                  

    }

 public void AdjustParametersForLevel(int level)
{
    
      moveSpeed = 30.0f; 
     maxSize = 5.0f;
}


 public float GetStartTime()
    {
        return startTime;
    }

    public bool HasBeenHit()
    {
        return hasBeenHit;
    }


}
