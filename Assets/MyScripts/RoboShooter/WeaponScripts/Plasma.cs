using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma : ProjectileBehaviour {

	// Use this for initialization
	void Start () {
        transform.Rotate(0, 0, Random.Range(-180, 180));
	}
	
}
