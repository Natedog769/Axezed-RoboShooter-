﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour {

    public BoxCollider2D playerMoveBorder;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Players")
        {
            //move camera
            
        }
    }
}
