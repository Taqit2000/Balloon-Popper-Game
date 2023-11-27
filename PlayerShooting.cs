using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
   


    
    public string arrowPrefabName = "arrow1";  
    private Transform firePoint;               

    void Start()
    {
       
        firePoint = transform; 
    }

    void Update()
    {
       
      //  firePoint.position = transform.position;

      firePoint.position = transform.position;

        if (Input.GetMouseButtonDown(0)) 
        {
            ShootArrow();
        }
    }

void ShootArrow()
{
    
    GameObject arrowPrefab = GameObject.FindWithTag("arrow");

   
    if (arrowPrefab != null)
    {
        GameObject arrowPrefabInstance = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);

                    Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;


        //arrowPrefabInstance.GetComponent<ArrowMovement>().ShootArrow(); 

                    arrowPrefabInstance.GetComponent<ArrowMovement>().ShootArrow(direction);


                    

    }
    else
    {
        Debug.LogError("Arrow Prefab not found! Make sure the arrow has the 'arrow' tag.");
    }
}
     
    

 











}
