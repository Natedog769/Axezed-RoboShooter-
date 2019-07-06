using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienScript : EnemyBehavior , ITakeDamage
{
    //enum EnemyState { dead = 0, alive = 1, attacking = 2 }
    //EnemyState enemyState;


    //public int hitPoints;
    //public float moveSpeed;
    //public float rotSpeed;
    [Space]
    [Header("Alien Fields")]
    public string deathScream;

    #region Track Player variables
    public float detectionRange;
    public bool playerInAttackRange;
    
    public float distanceToPlayer;
    #endregion

    #region patrolling movement variables

    public Vector3 patrolStop;
    Vector3 attackPosition;

    public float patrolStopTime;
    float patrolWaitTimer;

    public int patrolXFactor;
    public int patrolYFactor;
    
    #endregion

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        HPCheck();
        //this will track and monitor the player and its distance from the player
        distanceToPlayer = GetDistanceFromPlayer();
        if (distanceToPlayer <= detectionRange)
        {
            playerInAttackRange = true;
        }
        else if (distanceToPlayer > detectionRange)
        {
            playerInAttackRange = false;
        }
        //end the player in range tracking

        if (playerInAttackRange)
        {
            AttackPlayer();
        }
        else
        {
            PatrolingMovement();
        }

    }

    public override void HPCheck()
    {
        if (enemyState == EnemyState.dead)
        {
            FindObjectOfType<AudioManager>().Play(deathScream);
        }

        if (distanceToPlayer > 100)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

        base.HPCheck();

    }


    void AttackPlayer()
    {
        Rotate(thePlayer.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);
    }

    void PatrolingMovement()
    {
        if (patrolWaitTimer <= 0)
        {
            patrolStop = GetNewPatrolSpot();
            patrolWaitTimer = patrolStopTime;
        }
        else
        {
            Rotate(patrolStop);
            transform.position = Vector3.MoveTowards(transform.position, patrolStop, moveSpeed * Time.deltaTime);
            patrolWaitTimer -= Time.deltaTime;
        }
    }
    
    void Rotate(Vector3 target)
    {

        Vector2 direction = new Vector2(target.x - transform.position.x, target.y - transform.position.y);

        transform.up = direction * rotSpeed;

    }

    Vector3 GetNewPatrolSpot()
    {
        int xPos = Random.Range(-patrolXFactor, patrolXFactor);
        int yPos = Random.Range(-patrolYFactor, patrolYFactor);
        Vector3 randomPatrolSpot = new Vector3(transform.position.x + xPos , transform.position.y + yPos, 0);
        return randomPatrolSpot;
    }
    

    float GetDistanceFromPlayer()
    {
        float distance = Vector3.Distance(transform.position, thePlayer.transform.position);



        return distance;
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
