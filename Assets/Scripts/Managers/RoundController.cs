using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundController : MonoBehaviour
{
    public TMP_Text roundCounter;

    public GameObject enemy;
    public GameObject spawner;
    public int maxZombies = 5;
    public int aliveZombies = 1;
    public int roundNumber;
    
    public static RoundController Instance { get; private set; }

    private void Awake()
    {
        // Ensure that only one instance of PlayerController exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void advanceRound()
    {
        roundNumber++;
        maxZombies += 5;
        roundCounter.text = "Round: " + roundNumber;
        
        spawnEnemy();
    }

    void spawnEnemy()
    {
        float xPos = 0;
        float zPos = 0;

        for (int i = 0; i < maxZombies; i++)
        {
            xPos = Random.Range(-5.0f,5.0f);
            zPos = Random.Range(-5.0f,5.0f);

            Vector3 spawnOffset = new Vector3(xPos, 0.0f,zPos);

            Instantiate(enemy, spawner.transform.position + spawnOffset, Quaternion.identity);     
            RoundController.Instance.aliveZombies += 1;
        }
    }

    void Start()
    {
        advanceRound();
        roundCounter.text = "Round: " + roundNumber;
    }

    void Update()
    {
        if (aliveZombies < 1)
        {
            advanceRound();
        }
    }
}
