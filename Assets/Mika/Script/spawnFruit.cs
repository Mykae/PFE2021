using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnFruit : MonoBehaviour
{
    public Transform[] spawnFruits;
    public GameObject[] fruits;
    private float timeBetweenSpawns;
    public float tempsSpawn;
    public GameObject player;

    public RandomSound soundManager;
    //int soundToPlay = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenSpawns <= 0)
        {
            Transform randomSpawnPoint = spawnFruits[Random.Range(0, spawnFruits.Length)];
            GameObject randomFruit = fruits[Random.Range(0, fruits.Length)];

            GameObject SpawnedFruit = Instantiate(randomFruit, randomSpawnPoint.position, Quaternion.identity);

            SpawnedFruit.tag = "SpawnedFruit";
            soundManager.PlayRandomSound();

            timeBetweenSpawns = tempsSpawn;
        }
        else
        {
            timeBetweenSpawns -= Time.deltaTime;
        }
    }
}
