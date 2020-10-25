using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControler : MonoBehaviour
{
    public static GameControler gamecontroller;


    [SerializeField]
    private float spawnRadius = 7, time = 1.5f;
    private int score;
  
    public GameObject[] enemies;
    public GameObject[] Weapons;
    public GameObject box;
    public GameObject txt_GameOver;
    public Boundary boundary;

    public Text txt_Record;
    public Text txt_score;
    public bool GameOver = false;


    void Start()
    {
        gamecontroller = this;
        StartCoroutine(SpwanEnemies());
        StartCoroutine(SpwanWeapons());
    }

    private void Update()
    {
        if (GameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator SpwanWeapons()
    {
        yield return new WaitForSeconds(3);
        Vector2 spawnWeapon = new Vector2(Random.Range(boundary.xMin, boundary.xMax), Random.Range(boundary.yMin, boundary.yMax));
        Instantiate(box, spawnWeapon, Quaternion.identity);
        Instantiate(Weapons[Random.Range(0, Weapons.Length)], spawnWeapon, Quaternion.identity);
        StartCoroutine(SpwanWeapons());
    }

    IEnumerator SpwanEnemies()
    {
        Vector2 spawnPosition = GameObject.Find("Player").transform.position;
        spawnPosition += Random.insideUnitCircle.normalized * spawnRadius;

        Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPosition, Quaternion.identity);

        yield return new WaitForSeconds(time);
        StartCoroutine(SpwanEnemies());
    }
   
    public void SetScore(int points)
    {
        score += points;
        txt_score.text = score.ToString();
    }

    public void gameOver()
    {
        GameOver = true;
        txt_GameOver.SetActive(true);

        if (PlayerPrefs.GetInt("record") < score)
            PlayerPrefs.SetInt("record", score);

        txt_Record.text = "Record: " + PlayerPrefs.GetInt("record");
    }
}
