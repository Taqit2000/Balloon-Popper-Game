using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{

public float arrowSpeed = 10.0f;  
    private bool isShot = false;

    void Update()
    {
        if (isShot)
        {
            MoveArrow();
        }
        
    }

    void MoveArrow()
    {
        
        transform.Translate(Vector2.up * arrowSpeed * Time.deltaTime);

        
        if (transform.position.y > 10.0f)  
        {
            Destroy(gameObject);
        }
    }

    public void ShootArrow(Vector2 direction)
    {
        isShot = true;


       Vector2 adjustedDirection = new Vector2(direction.x, direction.y * 2.0f);
       
       GetComponent<Rigidbody2D>().velocity = direction.normalized * arrowSpeed;
      //GetComponent<Rigidbody2D>().velocity = Vector2.up * arrowSpeed;
    }








}



