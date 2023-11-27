using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeBalloonController : MonoBehaviour
{
     public float moveSpeed = 2.0f; 
       private int moveDirection = 1; 
    private bool hasBeenHit = false;

    void Start()
    {
        StartCoroutine(MoveBalloon());
    }

    IEnumerator MoveBalloon()
    {
       while (!hasBeenHit)
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

            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("arrow"))
        {
            
            Destroy(other.gameObject);  

            
            GameManager.instance.DeductPoints(5);  
           
            BurstNegativeBalloon();
        }
    }

    void BurstNegativeBalloon()
    {
        Destroy(gameObject);
    }
}
