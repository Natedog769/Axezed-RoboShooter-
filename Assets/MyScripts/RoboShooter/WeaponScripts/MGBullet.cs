using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGBullet : ProjectileBehaviour {
    //when this time is up it turns a new angle
    public float bulletHangTime;

    float turnAmount;


    void Start()
    {

        turnAmount = Random.Range(-1, 1);


        transform.Rotate(0, 0, turnAmount);



        StartCoroutine(BulletSwayMovement(bulletHangTime));
    }

    IEnumerator BulletSwayMovement(float secondsToTurn)
    {
        turnAmount = Random.Range(-2, 2);
        yield return new WaitForSeconds(secondsToTurn);
        transform.Rotate(0, 0, turnAmount);
        turnAmount = Random.Range(-3, 3);
        yield return new WaitForSeconds(secondsToTurn);
        transform.Rotate(0, 0, turnAmount);
        turnAmount = Random.Range(-4, 4);
        yield return new WaitForSeconds(secondsToTurn);
        transform.Rotate(0, 0, turnAmount);
        turnAmount = Random.Range(-5, 5);
        yield return new WaitForSeconds(secondsToTurn);
        turnAmount = Random.Range(-6, 6);
    }

}
