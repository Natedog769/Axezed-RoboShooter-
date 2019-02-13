using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBehaviour : MonoBehaviour {

    public int damageOut;
    public float speed;
    public float lifeTime;
    public EffectScript hitEffect;
    public bool playersProjectile;

    void Start()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {
        lifeTime -= Time.deltaTime;

        transform.Translate(Vector2.up * speed * Time.deltaTime);
        if (lifeTime <= 0)
        {
            if (hitEffect != null)
                Instantiate(hitEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playersProjectile == true)
        {//if it is a player projectile it will ignore the player and not hit the play
            if (collision.tag != "Player" && collision.tag != "PlayerAmmo")
            {
                if (hitEffect != null)
                    Instantiate(hitEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            
        }
        else if (playersProjectile == false)
        {
            if (collision.tag != "Enemy" && collision.tag != "EnemyAmmo")
            {
                if (hitEffect != null)
                    Instantiate(hitEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

    }

}
