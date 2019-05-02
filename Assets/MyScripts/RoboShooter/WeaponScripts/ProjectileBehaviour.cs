using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBehaviour : MonoBehaviour {

    public string audioClipName;
    public int damageOut;
    public float speed;
    public float lifeTime;
    public EffectScript hitEffect;
    public bool playersProjectile;

    void Awake()
    {
        if (audioClipName.Length != 0)
            FindObjectOfType<AudioManager>().Play(audioClipName);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        if (lifeTime <= 0)
        {
            if (hitEffect != null)
                Instantiate(hitEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
        else lifeTime -= Time.deltaTime;

    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (playersProjectile == true)
        {//if it is a player projectile it will ignore the player and not hit the play
            if (collision.CompareTag("Player") || collision.CompareTag("PlayerAmmo") || collision.CompareTag("WalkableGround"))
            {

            }
            else
            {
                if (hitEffect != null)
                    Instantiate(hitEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            
        }
        else if (playersProjectile == false)
        {
            if (!collision.CompareTag("Enemy") && !collision.CompareTag("EnemyAmmo") && !collision.CompareTag("WalkableGround"))
            {
                if (hitEffect != null)
                    Instantiate(hitEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

    }

}
