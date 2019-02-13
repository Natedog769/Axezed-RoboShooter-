using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : ProjectileBehaviour {

    public float rocketSeconds;
    float randomMovement;

	void Start () {
        randomMovement = Random.Range(-25, 25);

        transform.Rotate(0, 0, randomMovement);

        StartCoroutine(RocketMovement(rocketSeconds));
    }
	
    IEnumerator RocketMovement(float secondsToTurn)
    {
        randomMovement = Random.Range(-45, 45);
        yield return new WaitForSeconds(secondsToTurn);
        transform.Rotate(0, 0, randomMovement);
        randomMovement = Random.Range(-45, 45);
        yield return new WaitForSeconds(secondsToTurn);
        transform.Rotate(0, 0, randomMovement);
        randomMovement = Random.Range(-45, 45);
        yield return new WaitForSeconds(secondsToTurn);
        transform.Rotate(0, 0, randomMovement);
    }

	// Update is called once per frame
	public override void Update () {
        base.Update();
    }
}
