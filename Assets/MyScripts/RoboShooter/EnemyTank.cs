//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : MonoBehaviour, ITakeDamage {



    public bool smallTank;
    public bool mediumTank;
    public bool largeTank;
    public int xPValue;
    public int hitPoints;
    public GameObject deathExplosion;
    enum EnemyState {dead = 0, alive = 1, attacking = 2}
    EnemyState enemyState;
    //event stuff
    public delegate void EnemyDied(EnemyTank enemy);
    public static event EnemyDied EnemyDiedEvent;

    public Transform[] patrolStops;
    int randomSpot;
    public float patrolStopWaitTime;
    float waitTime;

    //move to player variables
    public float speed;
    public float turretRotSpeed;
    public float attackingDistance;
    public float stoppingDistance;
    public float retreatDistance;

    //shooting variables
    Transform player;
    public GameObject[] turret;

	void Start () {
        player = FindObjectOfType<TopDownControlls>().gameObject.transform;
        
        enemyState = EnemyState.alive;
        //partrol

        if (patrolStops != null)
            randomSpot = Random.Range(0, patrolStops.Length);
        
	}
	
	// Update is called once per frame
	void Update () {
        if (mediumTank == true || smallTank == true)
        {//small and med tanks will patrol and then purue the player once in attacking range.
            if (Vector2.Distance(transform.position, player.position) > attackingDistance)
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
        else if(largeTank == true)
        {//if its a large tank then it will patrol and shoot only

            if (Vector2.Distance(transform.position, player.position) > attackingDistance)
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
        
        HPCheck();
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

    void HPCheck()
    {
        if (enemyState == EnemyState.dead)
        {
            if (deathExplosion != null)
            {
                Instantiate(deathExplosion, transform.position, transform.rotation);
            }

            EnemyDiedEvent(this);
            Destroy(gameObject);
        }
            
    }

    void RotateTurret()
    {//this module will step thru the turret array and move them all
        for (int i = 0; i < turret.Length; i++)
        {
            Vector2 direction = new Vector2(player.position.x - turret[i].transform.position.x, player.position.y - turret[i].transform.position.y);
            if (smallTank == true)
                transform.up = direction / turretRotSpeed;
            else turret[i].transform.up = direction * turretRotSpeed;
        }
    }

   
    

    void PatrolingMovement()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolStops[randomSpot].position, speed * Time.deltaTime);

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

    void MoveTowardsPlayer()
    {

        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {//move to player
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
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
            TakeDamage(other.gameObject.GetComponent<ProjectileBehaviour>().damageOut);


    }
}
