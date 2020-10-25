using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 4;
    private Vector2 direction;

    
    void Start()
    {
        direction = GameObject.Find("DirectionBullet").transform.position;
        transform.position = GameObject.Find("SpawnBullet").transform.position;
        Destroy(gameObject, 5);
    }

    
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Box")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }

    }
}
