using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrets : MonoBehaviour {


    public Transform[] barrels;
    public GameObject projectile;
    float timeBetweenShots;
    public float startTimeBetweenShots;
    


    void Start () {
        timeBetweenShots = startTimeBetweenShots;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShootCannons()
    {
        
        if (timeBetweenShots <= 0)
        {
            for (int i = 0; i < barrels.Length; i++)
            {
                Instantiate(projectile, barrels[i].position, barrels[i].rotation);
            }
            timeBetweenShots = startTimeBetweenShots;
        }
        else timeBetweenShots -= Time.deltaTime;

    }
}
