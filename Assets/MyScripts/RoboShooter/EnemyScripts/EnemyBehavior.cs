using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehavior : MonoBehaviour
{
    [Header("Enemy Fields")]
    public int hitPoints;
    public int xpValue;
    [Space]
    public float moveSpeed;
    public float rotSpeed;

    public TopDownControlls thePlayer;
    
    public delegate void EnemyDied(EnemyBehavior enemy);
    public static event EnemyDied EnemyDiedEvent;

    [HideInInspector]
    public enum EnemyState { dead = 0, alive = 1, attacking = 2 }
    public EnemyState enemyState;

    // Start is called before the first frame update
    public virtual void Start()
    {
        thePlayer = FindPlayer();

        enemyState = EnemyState.alive;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState == EnemyState.dead)
        {
            EnemyDiedEvent(this);
            Destroy(gameObject);
        }
    }

    public virtual void HPCheck()
    {
        if (enemyState == EnemyState.dead)
        {
            EnemyDiedEvent(this);
            Destroy(gameObject);
        }

    }

    TopDownControlls FindPlayer()
    {
        TopDownControlls player = FindObjectOfType<TopDownControlls>();
        return player;
    }

}
