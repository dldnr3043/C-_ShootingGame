using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    private float curSpawnDelay;

    public GameObject player;

    // UI
    public Text scoreText;
    public Image[] lifeImages;
    public Image[] boomImages;
    public GameObject gameOverSet;

    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();

            Player playerLogic1 = player.GetComponent<Player>();

            if (playerLogic1.power < 2)
                maxSpawnDelay = UnityEngine.Random.Range(2f, 3f);
            else if(playerLogic1.power < 3)
            {
                maxSpawnDelay = UnityEngine.Random.Range(1f, 2f);
            }
            else
                maxSpawnDelay = UnityEngine.Random.Range(0.5f, 0.8f);
            curSpawnDelay = 0;
        }


        // UI Score Update
        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
    }

    void SpawnEnemy()
    {
        int ranEnemy = UnityEngine.Random.Range(0, 3);
        int ranPoint = UnityEngine.Random.Range(0, 5);
        GameObject enemy = Instantiate(enemyObjs[ranEnemy],
                                        spawnPoints[ranPoint].position,
                                        spawnPoints[ranPoint].rotation);
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();

        enemyLogic.player = player;
    }

    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }

    public void UpdateLifeIcon(int life)
    {
        // UI Life Init Disable
        for (int i = 0; i < 3; i++)
        {
            lifeImages[i].color = new Color(1, 1, 1, 0);
        }

        // UI Life Init Active
        for (int i=0;i<life;i++)
        {
            lifeImages[i].color = new Color(1, 1, 1, 1);
        }
    }

    public void UpdateBoomIcon(int boom)
    {
        // UI Boom Init Disable
        for (int i = 0; i < 3; i++)
        {
            boomImages[i].color = new Color(1, 1, 1, 0);
        }

        // UI Boom Init Active
        for (int i = 0; i < boom; i++)
        {
            boomImages[i].color = new Color(1, 1, 1, 1);
        }
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);
    }

    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 3.5f;
        player.SetActive(true);

        Player playerLogic = player.GetComponent<Player>();
        playerLogic.isHit = false;
    }
}