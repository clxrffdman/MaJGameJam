using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    [Header("Enemy Spawner State")]
    public bool canSpawn;
    public bool inRange;

    [Header("Enemy Spawner Stats")]
    public float baseSpawnTimer;
    public float spawnTimerVariation;
    public int spawnLimit;
    int totalSpawns;


    public float currentTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
        inRange = false;
        totalSpawns = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if(currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
        }

        if(canSpawn && totalSpawns >= spawnLimit)
        {
            canSpawn = false;
        }
    }

    void FixedUpdate()
    {
        if(canSpawn && inRange && currentTimer <= 0)
        {
            currentTimer = baseSpawnTimer + Random.Range(-spawnTimerVariation, spawnTimerVariation);
            Instantiate(enemy, transform.position, Quaternion.identity);
            totalSpawns++;
        }
    }
}
