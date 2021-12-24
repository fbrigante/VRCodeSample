using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTarget : MonoBehaviour
{
    public float speed = 1f;
    public float range = 3f;
    public FoodFightGame game;

    private float initialXPosition;

    private void Start()
    {
        initialXPosition = transform.position.x;
    }

    void Update()
    {
        // Calculate the new X position of the target
        Vector3 newPosition = transform.position;
        newPosition.x = initialXPosition + Mathf.Sin(Time.time * speed) * range;
        transform.position = newPosition;
    }
    
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Food")
        {
            //Add a point
            
            Destroy(gameObject);
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Food")
        {
            FoodFight.instance.UpdateScore();
            FoodFight.instance.spawnTarget();
            Destroy(gameObject);
            //Instantiate(collision.collider.gameObject, new Vector3(-1, 1, 1), Quaternion.identity);
            //Destroy(collision.collider.gameObject);
        }    
    }
}
