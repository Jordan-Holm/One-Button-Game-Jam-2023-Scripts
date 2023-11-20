using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject[] pickUpPrefabs;

    public float spawnMin;
    public float spawnMax;

    public float spawnPosX = 8.5f;
    public float spawnPosY = -0f;

    private float targetTime;
    // Start is called before the first frame update
    void Start()
    {
        targetTime = 5;
    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;
        if (targetTime <= 0 && gameManager.gameIsRunning == true)
        {
            SpawnPickUp();
            targetTime = Random.Range(spawnMin, spawnMax);
        }
    }

    private void SpawnPickUp()
    {
        int randomPrefab = GetRandomPrefab(pickUpPrefabs);
        Instantiate(pickUpPrefabs[randomPrefab], pickUpPrefabs[randomPrefab].transform.position, pickUpPrefabs[randomPrefab].transform.rotation);
    }

    private int GetRandomPrefab(GameObject[] prefabs)
    {
        int randomPrefab = Random.Range(0, prefabs.Length);
        return randomPrefab;
    }
}
