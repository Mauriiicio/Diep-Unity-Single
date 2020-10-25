using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Weapon", menuName = "Weapon")]
public class Weapons : ScriptableObject
{
    public GameObject PrefabBullet;
    public float FireRat = 1;
    public int damage = 20;
    


    public void Shoot()
    {


       
            Instantiate(PrefabBullet, GameObject.Find("SpawnBullet").transform.position, Quaternion.identity);
            
        
    }
}
