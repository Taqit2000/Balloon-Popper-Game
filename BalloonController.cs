using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;


public class BalloonController : MonoBehaviour
{


 public float growthRate = 0.1f; // Adjust the growth rate as needed
    public float destroyTime = 20.0f; // Time after which the balloon is automatically destroyed if not shot

    private float currentSize = 1.0f; // Initial size
    private float startTime; // Time when the script started


    public float moveSpeed = 2.0f; // Adjust the speed as needed
    public float maxSize = 5.0f;   // Adjust the maximum size as needed
   
    public float destroySize = 20.0f; // Size at which the balloon is automatically destroyed

    private int moveDirection = 1; // 1 for right, -1 for left
     public int balloonPoints = 10;

    public string arrowTag = "arrow";
    
        [SerializeField] AudioSource audio;

    private bool hasBeenHit = false;
     
        public string popSoundPath = "sounds/game over";

        public int negativePoints = -5;
        private GameManager gameManager;
        public static BalloonController instance;

    
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

    }

  

     void MoveBalloon()
    {
        
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
        
        Debug.Log("Adding 10 points");
        GameManager.instance.AddPoints(10);
    }

            

           
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
   
    switch (level)
    {
        case 1:
           
            break;
        case 2:
        case 3:
            
            moveSpeed = 10.0f;
            maxSize -= 1.0f;
            break;
            
        
    }
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
