using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeSpawner : MonoBehaviour
{
    public TopDownControlls thePlayer;
    public float maxDistanceFromPlayer;
    float distanceToPlayer;

    int currentWave = 0;

    //timer
    public float maxTimeBetweenWaves;
    float waveTimer;

    public List<AlienScript> enemiesOfThisWave = new List<AlienScript>();
    public AlienScript[] enemyTypeForWave;//ideally we will have 10 enemy waves that will repeat but grow in numbers
    public int[] numOfEnemiesToSpawnThisRound;

    void Start()
    {
        thePlayer = GetPlayer();
        ResetTime();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = GetDistanceFrom(thePlayer.gameObject);

        if (distanceToPlayer < maxDistanceFromPlayer)
        {
            SpawnEnemies();
        }
        
    }

    public TopDownControlls GetPlayer()
    {
        TopDownControlls player = FindObjectOfType<TopDownControlls>();
        return player;
    }

    public float GetDistanceFrom(GameObject target)
    {
        float distance = Vector2.Distance(this.gameObject.transform.position, target.transform.position);
        return distance;
    }

    public void SpawnEnemies()
    {
        if (waveTimer <= 0)
        {
            SpawnEnemy(currentWave);
            if (currentWave < 9)
            {
                currentWave++;
            }
            else currentWave = 0;
            waveTimer = maxTimeBetweenWaves;
        }
        else waveTimer -= Time.deltaTime;
    }
    

    float ResetTime()
    {
        return Random.Range(3,6);

    }

    void SpawnEnemy(int currentWave)
    {
        if (currentWave % 2 == 0)
        {// if the currentWave is even
            for (int i = 0; i < numOfEnemiesToSpawnThisRound[currentWave]; i++)
            {
                Instantiate(enemyTypeForWave[0], transform.position, transform.rotation);
            }
        }
        else if (currentWave < 6)
        {
            for (int i = 0; i < numOfEnemiesToSpawnThisRound[currentWave]; i++)
            {
                Instantiate(enemyTypeForWave[1], transform.position, transform.rotation);
            }
        }
        else if (currentWave > 6)
        {
            for (int i = 0; i < numOfEnemiesToSpawnThisRound[currentWave]; i++)
            {
                Instantiate(enemyTypeForWave[2], transform.position, transform.rotation);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D checkArea)
    {
        if (checkArea.CompareTag("Player"))
        {
        }
    }
}
