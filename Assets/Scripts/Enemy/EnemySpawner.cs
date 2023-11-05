using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject spawner;
    public float spawnDelay;

    void Start()
    {
        InvokeRepeating("SpawnEnemy",0f,spawnDelay);
    }

    void SpawnEnemy() 
    {     
        if (RoundController.Instance.aliveZombies < RoundController.Instance.maxZombies)
        {
            Instantiate(enemy, spawner.transform.position, Quaternion.identity);     
            RoundController.Instance.aliveZombies += 1;
        }

        
    }
}
