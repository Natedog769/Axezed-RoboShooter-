using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeModeBehaviour : MonoBehaviour , ITakeDamage
{
    public float speed;
    public float rotSpeed;
    public int hitPoints;

    public float stoppingDistance;
    public float retreatDistance;
    enum EnemyState { dead = 0, alive = 1, attacking = 2 }
    EnemyState enemyState;
    GameObject target;
    
    void Start()
    {
        enemyState = EnemyState.alive;
        FindaThreat();
    }

    // Update is called once per frame
    void Update()
    {
       if (enemyState == EnemyState.alive)
        { MoveTowardsTargetThreat();
        Rotate();
        // AttackIfCloseToTarget();
        HPCheck();
        }
    }

    void FindaThreat()
    {
        target = FindObjectOfType<TopDownControlls>().gameObject;
    }

    void MoveTowardsTargetThreat()
    {

        if (Vector2.Distance(transform.position, target.transform.position) > stoppingDistance)
        {//move to player
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, target.transform.position) < stoppingDistance && Vector2.Distance(transform.position, target.transform.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, target.transform.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position, -speed * Time.deltaTime);
        }

    }
    void Rotate()
    {//this module will step thru the turret array and move them all
        
        Vector2 direction = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);
      
            transform.up = direction * rotSpeed;
        
    }
    void HPCheck()
    {
        if (enemyState == EnemyState.dead)
        {
            gameObject.tag =  "WalkableGround";
        }

    }


    public void SeperateBodyParts()
    {
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
