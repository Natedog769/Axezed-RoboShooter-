﻿//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : EnemyBehavior, ITakeDamage {



    public bool smallTank;
    public bool mediumTank;
    public bool largeTank;
    public GameObject deathExplosion;
    //enum EnemyState {dead = 0, alive = 1, attacking = 2}
    //EnemyState enemyState;
    //event stuff

    public Transform[] patrolStops;
    int randomSpot;
    public float patrolStopWaitTime;
    float waitTime;

    //move to player variables
   // public float speed;
  //  public float turretRotSpeed;
    public float attackingDistance;
    public float stoppingDistance;
    public float retreatDistance;

    //shooting variables
    //Transform player;
    public GameObject[] turret;

	public override void Start ()
    {
        base.Start();
        //partrol

        if (patrolStops != null)
            randomSpot = Random.Range(0, patrolStops.Length);
        
	}
	
	// Update is called once per frame
	void Update () {

        AIactions();
        HPCheck();
	}

    public void AIactions()
    {
        if (mediumTank == true || smallTank == true)
        {//small and med tanks will patrol and then purue the player once in attacking range.
            if (Vector2.Distance(transform.position, thePlayer.transform.position) > attackingDistance)
            {
                PatrolingMovement();
            }
            else
            {
                MoveTowardsPlayer();
                ShootTurrets();
                RotateTurret();
            }
        }
        else if (largeTank == true)
        {//if its a large tank then it will patrol and shoot only

            if (Vector2.Distance(transform.position, thePlayer.transform.position) > attackingDistance)
            {
                PatrolingMovement();
            }
            else
            {

                PatrolingMovement();
                ShootTurrets();
                RotateTurret();
            }
        }
    }

    public void ShootTurrets()
    {

        for (int io = 0; io < turret.Length; io++)
        {
            if (turret[io].GetComponent<Turrets>() != null)
            {
                turret[io].GetComponent<Turrets>().ShootCannons();
            }
        }
    }

    public override void HPCheck()
    {
        if (enemyState == EnemyState.dead)
        {
            if (deathExplosion != null)
            {
                Instantiate(deathExplosion, transform.position, transform.rotation);
            }
        }

        base.HPCheck();

    }

    void RotateTurret()
    {//this module will step thru the turret array and move them all
        for (int i = 0; i < turret.Length; i++)
        {
            Vector2 direction = new Vector2(thePlayer.transform.position.x - turret[i].transform.position.x, thePlayer.transform.position.y - turret[i].transform.position.y);
            if (smallTank == true)
                transform.up = direction / rotSpeed;
            else turret[i].transform.up = direction * rotSpeed;
        }
    }

   
    

    void PatrolingMovement()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolStops[randomSpot].position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, patrolStops[randomSpot].position) < .5f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, patrolStops.Length);
                waitTime = patrolStopWaitTime;
            }
            else waitTime -= Time.deltaTime;
        }

    }

    public void MoveTowardsPlayer()
    {

        if (Vector2.Distance(transform.position, thePlayer.transform.position) > stoppingDistance)
        {//move to player
            transform.position = Vector2.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, thePlayer.transform.position) < stoppingDistance && Vector2.Distance(transform.position, thePlayer.transform.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, thePlayer.transform.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, thePlayer.transform.position, -moveSpeed * Time.deltaTime);
        }

    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
            enemyState = EnemyState.dead;
        else enemyState = EnemyState.alive;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAmmo"))
        {
            FindObjectOfType<AudioManager>().Play("Damage1");
            TakeDamage(other.gameObject.GetComponent<ProjectileBehaviour>().damageOut);
        }

    }
}
