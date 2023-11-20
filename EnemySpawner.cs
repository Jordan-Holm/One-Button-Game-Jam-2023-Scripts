using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject banditPrefab;

    public float spawnMin;
    public float spawnMax;

    private float spawnPosX = 8f;
    private float spawnPosY = -3.08f;

    private float targetTime;
    void Start()
    {
        targetTime = 2;
    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;
        if (targetTime <= 0 && gameManager.gameIsRunning == true)
        {
            SpawnEnemy();
            targetTime = Random.Range(spawnMin, spawnMax);
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPos = new Vector3(spawnPosX, spawnPosY);

        Instantiate(banditPrefab, spawnPos, Quaternion.identity);
    }
}
