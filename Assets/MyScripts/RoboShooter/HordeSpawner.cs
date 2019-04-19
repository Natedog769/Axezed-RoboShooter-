using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeSpawner : MonoBehaviour
{

    public float timeBetweenWaves;
    public float spawnTimer =0f;
    public int waveNumber;
    public HordeModeBehaviour[] enemyTypeForWave;//ideally we will have 10 enemy waves that will repeat but grow in numbers
    public int[] numOfEnemiesToSpawnThisRound;

    void Start()
    {
        ResetTime();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnWave();
        
    }


    public void SpawnWave()
    {
        if (spawnTimer <= 0)
        {
            SpawnEnemy();
            ResetTime();
            waveNumber++;
        }
        else spawnTimer -= Time.deltaTime;
    }

    public void SpawnEnemy()
    {
        for (int i = 0; i < numOfEnemiesToSpawnThisRound[waveNumber]; i++)
        {
            Vector3 transform = new Vector3(gameObject.transform.position.x + i, gameObject.transform.position.y, gameObject.transform.position.z);
            Instantiate(enemyTypeForWave[waveNumber], transform, gameObject.transform.rotation);
        }
            
    }




    void ResetTime()
    {
        spawnTimer = timeBetweenWaves;

    }
}
