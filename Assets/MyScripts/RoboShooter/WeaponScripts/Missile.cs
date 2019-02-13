using System.Collections;
//using System;
using UnityEngine;

public class Missile : ProjectileBehaviour {

    public float missileTime;

    float turnAmount;


	void Start () {

        turnAmount = Random.Range(-1, 1);
       
        
        transform.Rotate(0, 0, turnAmount);



        StartCoroutine(MissileMovement(missileTime));
	}

    IEnumerator MissileMovement(float secondsToTurn)
    {
        turnAmount = Random.Range(-10, 10);
        yield return new WaitForSeconds(secondsToTurn);
        transform.Rotate(0, 0, turnAmount);
        turnAmount = Random.Range(-15, 15);
        yield return new WaitForSeconds(secondsToTurn);
        transform.Rotate(0, 0, turnAmount);
        turnAmount = Random.Range(-15, 15);
        yield return new WaitForSeconds(secondsToTurn);
        transform.Rotate(0, 0, turnAmount);
        turnAmount = Random.Range(-15, 15);
        yield return new WaitForSeconds(secondsToTurn);
        turnAmount = Random.Range(-25, 25);
    }

}
