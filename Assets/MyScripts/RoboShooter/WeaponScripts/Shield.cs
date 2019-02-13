using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    public TopDownControlls player;
    public Sprite shieldSprite;
    public float lifeTime;
    public int maxHitPoints;
    public int hitPoints;

    public void Update()
    {//so every frame this will be the visual
        

        MonitorShieldStatus();
    }

    public void SetShieldActive()
    {//when the shield is activated the hit points are set to hte max 
        hitPoints = maxHitPoints;
        gameObject.GetComponent<SpriteRenderer>().sprite = shieldSprite;
    }

    public void MonitorShieldStatus()
    {
        if (hitPoints <= 0)
        {
            SetShieldInActive();
            player.DeActivateShield();
        }
    }

    public void SetShieldInActive()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
    }

    public void ShieldTakesDamage(int damageIn)
    {
        hitPoints -= damageIn;
    }

    


}
