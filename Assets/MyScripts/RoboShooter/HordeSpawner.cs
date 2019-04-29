using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeSpawner : MonoBehaviour
{
    public int currentWave;
    public int numberOfEnemiesThisWave;
    public float spawnTimer;
    public float spawnWaitTime;
    public float waveTimer;
    public float waveWaitTime;



    public List<HordeModeBehaviour> enemiesOfThisWave = new List<HordeModeBehaviour>();
    public HordeModeBehaviour[] enemyTypeForWave;//ideally we will have 10 enemy waves that will repeat but grow in numbers
    public int[] numOfEnemiesToSpawnThisRound;

    void Start()
    {
        ResetTime();
    }

    // Update is called once per frame
    void Update()
    {
       // WaveWaitTimer();
        
    }


    public void SpawnWave()
    {
     //   if (spawnTimer <= 0)
        {
          //  SpawnEnemy();
         //   ResetTime();
           // waveNumber++;
        }
    //    else spawnTimer -= Time.deltaTime;
    }

    public void SpawnLevel2Alien()
    {
        if (waveTimer <= 0)
        {
            //for (int i = 0; i < barrels.Length; i++)
            // {
            Instantiate(enemyTypeForWave[1], transform.position, transform.rotation);
            //}
            waveTimer = ResetTime();
        }
        else waveTimer -= Time.deltaTime;
    }
    

    float ResetTime()
    {
        return Random.Range(3,6);

    }

    void SpawnAlien()
    {
        if (spawnTimer <= 0)
        {
            //for (int i = 0; i < barrels.Length; i++)
           // {
                Instantiate(enemyTypeForWave[0], transform.position, transform.rotation);
            //}
            spawnTimer = spawnWaitTime;
        }
        else spawnTimer -= Time.deltaTime;

    }

    private void OnTriggerStay2D(Collider2D checkArea)
    {
        if (checkArea.CompareTag("Player"))
        {
            SpawnAlien();
        }
    }
}
