using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    public GameObject[] tanksToSpawn;
    public float timeBetweenSpawns;
    float timer;
    int numberofSpawns;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (timer >= 0)
        {
            for (int i = 0; i < tanksToSpawn.Length; i++)
            {

                Instantiate(tanksToSpawn[i], transform.position, transform.rotation);
                numberofSpawns++;
                TimerReset();
            
           
            }
        } else timer -= Time.deltaTime;
	}

    void TimerReset()
    {
        timer = timeBetweenSpawns;

        if (numberofSpawns == 5 || numberofSpawns == 7 || numberofSpawns == 10)
        {
            timeBetweenSpawns--;
        }

    }
}
