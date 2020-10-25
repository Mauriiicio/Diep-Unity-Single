using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaeponsChange : MonoBehaviour
{
    public Weapons weapon;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().weaponMoment = weapon;
            Destroy(gameObject);
        }
    }
}
