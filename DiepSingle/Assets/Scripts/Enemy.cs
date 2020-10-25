using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float health;
    private Transform playerPosition;

    public float speed;
    public int scorePoints;
    


    private void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, playerPosition.position) > 0.8f)
            transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, speed * Time.deltaTime);



        if (health < 1)
        {
            GameControler.gamecontroller.SetScore(scorePoints);
            Destroy(gameObject);

        }
        
    }
     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            health -= GameObject.Find("Player").GetComponent<Player>().weaponMoment.damage;
            Destroy(other.gameObject);
        }

    }
}
