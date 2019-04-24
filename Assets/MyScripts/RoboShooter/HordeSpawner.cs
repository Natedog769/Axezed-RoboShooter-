using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeSpawner : MonoBehaviour
{
    public int currentWave;
    public int numberOfEnemiesThisWave;
    public float waveWaitTime;

    public List<HordeModeBehaviour> enemiesOfThisWave = new List<HordeModeBehaviour>();
    public HordeModeBehaviour[] enemyTypeForWave;//ideally we will have 10 enemy waves that will repeat but grow in numbers
    public int[] numOfEnemiesToSpawnThisRound;

    void Start()
    {
        ResetTime()
    }

    // Update is called once per frame
    void Update()
    {
        WaveWaitTimer();
        
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


    void WaveWaitTimer()
    {
        if (waveWaitTime < 0)
        {
            ResetTime();
        }
        else waveWaitTime -= Time.deltaTime;
    }

    void ResetTime()
    {
        spawnTimer = timeBetweenWaves;

    }
}
