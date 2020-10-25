using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}


public class Player : MonoBehaviour
{

    private Rigidbody2D bodyPlayer;
    private Vector2 VelocityPlayer;
    private bool DamageEnemy = true;
    [SerializeField]
    private int Health;
    private float nextShoot = 0;

    public float Speed;
    public GameObject Bullet;
    public Boundary boundary;
    public Weapons weaponMoment;
    public int ammunition = 0;
    public Text txt_ammunitionCount;

    void Start()
    {
        bodyPlayer = GetComponent<Rigidbody2D>();
        txt_ammunitionCount.text = ammunition.ToString();
    }

    private void Update()
    {
        if (ammunition > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Time.time >= nextShoot)
                {
                    weaponMoment.Shoot();
                    nextShoot = Time.time + 1 / weaponMoment.FireRat;
                    ammunition--;
                    txt_ammunitionCount.text = ammunition.ToString();
                }
            }
        }


        rotation();
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),Mathf.Clamp(transform.position.y, boundary.yMin, boundary.yMax));
    }
    void FixedUpdate()
    {
        movement();
    }

    void movement()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        VelocityPlayer = moveInput.normalized * Speed;
        bodyPlayer.MovePosition(bodyPlayer.position + VelocityPlayer * Time.fixedDeltaTime);
    }

    void rotation()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);
    }
    IEnumerator DamageEnemyOff()
    {
        DamageEnemy = false;
        yield return new WaitForSeconds(1.5f);
        DamageEnemy = true;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            if (DamageEnemy)
            {
                StartCoroutine(DamageEnemyOff());
                Health--;
                Destroy(GameObject.Find("img_life").transform.GetChild(0).gameObject);
                if(Health < 1)
                {
                    GameControler.gamecontroller.gameOver();
                    Destroy(gameObject);
                }
            }
        }
        if (other.tag == "ammunition")
        {
            ammunition += 10;
            txt_ammunitionCount.text = ammunition.ToString();
            Destroy(other.gameObject);
        }
    }


}
