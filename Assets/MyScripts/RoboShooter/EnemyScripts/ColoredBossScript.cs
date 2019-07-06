//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredBossScript : MonoBehaviour, ITakeDamage {

   
    //the sprites this character uses
    public HealthBattery activeBattery;
    public Sprite[] weapons;
    public ProjectileBehaviour[] ammosets;
    public ArmorSets[] armorSelection;
    int powerLevel;
    public GameObject currentHead;
    public GameObject currentTorso;
    public GameObject currentRArm;
    public GameObject currentLArm;
    public GameObject rHand;
    public GameObject lHand;
    public GameObject deathExplosion;

    //AI variables
    Transform playerTarget;
    public float shootingRange;
    public float attackingDistance;
    public float stoppingDistance;
    public float retreatDistance;
    public float speed;
    float maxSpeed;
    public float rotSpeed;

    public Transform moveSpot;
    public float maxX;
    public float maxY;
    public float minX;
    public float minY;
    float distanceBetweenPlayerandBoss;

    public float maxWaitTime;
    float waitingTime;
    //end of AI variables

    //state of boss
    public enum StateBoss { dead = 0, alive = 1 };
    public StateBoss bossState;

    public enum StateArmor { light = 0, medium = 1, heavy = 2 }
    public StateArmor armorState;

    public enum StateRHGun { lvl1 = 0, lvl2 = 1, lvl3 = 2 }
    public StateRHGun rHGunState;

    public enum StateLHGun { lvl1 = 0, lvl2 = 1, lvl3 = 2 }
    public StateLHGun lHGunState;

    public delegate void ObjectiveComplete(ColoredBossScript target);
    public static event ObjectiveComplete CompleteObjective;

    // we will have a boss that will go down the levels of the armor and will take 3 dead batteries to kill about 30 hit points

    void Start () {
        //at the start boss will be at max levels
        bossState = StateBoss.alive;
        armorState = StateArmor.heavy;
        rHGunState = StateRHGun.lvl3;
        lHGunState = StateLHGun.lvl3;
        powerLevel = 2;
        playerTarget = FindObjectOfType<TopDownControlls>().gameObject.transform;

        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        maxSpeed = speed;

      

    }
	
	// Update is called once per frame
	void Update () {
        HealthCheck();
        ArmorCheck();
        WeaponCheck();
        AIControl();
        
	}

    void HealthCheck()
    {
        //this boss health check will see if the battery is below 0 if so then 
        //it checks the oower level to recharge the battery
        //then lowers the power level and weaponstates.
        if (activeBattery.batteryHP <= 0)
        {
            if (powerLevel > 0)
            {
                activeBattery.RechargeBattery();
                powerLevel--;
                rHGunState -= 1;
                lHGunState -= 1;

            }
            else
            {
                CompleteObjective(this);
                bossState = StateBoss.dead;
                Instantiate(deathExplosion, transform.position, transform.rotation);
                gameObject.SetActive(false);
            }
        }
        else
        {
            bossState = StateBoss.alive;

        }

    }

    void ArmorCheck()
    {
        int armorRate;
        armorRate = powerLevel;



        if (armorState == StateArmor.heavy)
        {
            currentHead.GetComponent<SpriteRenderer>().sprite = armorSelection[armorRate].head;
            currentTorso.GetComponent<SpriteRenderer>().sprite = armorSelection[armorRate].torso;
            currentRArm.GetComponent<SpriteRenderer>().sprite = armorSelection[armorRate].rightArm;
            currentLArm.GetComponent<SpriteRenderer>().sprite = armorSelection[armorRate].leftArm;
        }
        else if (armorState == StateArmor.medium)
        {
            currentHead.GetComponent<SpriteRenderer>().sprite = armorSelection[armorRate].head;
            currentTorso.GetComponent<SpriteRenderer>().sprite = armorSelection[armorRate].torso;
            currentRArm.GetComponent<SpriteRenderer>().sprite = armorSelection[armorRate].rightArm;
            currentLArm.GetComponent<SpriteRenderer>().sprite = armorSelection[armorRate].leftArm;
        }
        else if (armorState == StateArmor.light)
        {
            currentHead.GetComponent<SpriteRenderer>().sprite = armorSelection[armorRate].head;
            currentTorso.GetComponent<SpriteRenderer>().sprite = armorSelection[armorRate].torso;
            currentRArm.GetComponent<SpriteRenderer>().sprite = armorSelection[armorRate].rightArm;
            currentLArm.GetComponent<SpriteRenderer>().sprite = armorSelection[armorRate].leftArm;
        }

    }

    void WeaponCheck()
    {
        //this is where the state is tied to the sprite that is showen in the right hand
        if (rHGunState == StateRHGun.lvl1)
        {
            rHand.GetComponent<SpriteRenderer>().sprite = weapons[0];
            rHand.GetComponent<Turrets>().projectile = ammosets[0].gameObject;
        }
        else if (rHGunState == StateRHGun.lvl2)
        {
            rHand.GetComponent<SpriteRenderer>().sprite = weapons[1];
            rHand.GetComponent<Turrets>().projectile = ammosets[1].gameObject;
        }
        else if (rHGunState == StateRHGun.lvl3)
        {
            rHand.GetComponent<SpriteRenderer>().sprite = weapons[2];
            rHand.GetComponent<Turrets>().projectile = ammosets[2].gameObject;
        }


        if (lHGunState == StateLHGun.lvl1)
        {
            lHand.GetComponent<SpriteRenderer>().sprite = weapons[0];
            lHand.GetComponent<Turrets>().projectile = ammosets[0].gameObject;
        }
        else if (lHGunState == StateLHGun.lvl2)
        {
            lHand.GetComponent<SpriteRenderer>().sprite = weapons[1];
            lHand.GetComponent<Turrets>().projectile = ammosets[1].gameObject;
        }
        else if (lHGunState == StateLHGun.lvl3)
        {
            lHand.GetComponent<SpriteRenderer>().sprite = weapons[2];
            lHand.GetComponent<Turrets>().projectile = ammosets[2].gameObject;
        }
        else lHand.GetComponent<SpriteRenderer>().sprite = null;


    }

    void AIControl()
    {
        distanceBetweenPlayerandBoss = Vector2.Distance(transform.position, playerTarget.position);

        if (powerLevel == 2)
        {
            EngagePlayer();
        }
        else
        {
            StrafeAndFire();
        }

    }

    void AimAtPlayerTarget()
    {
        Vector2 direction = new Vector2(playerTarget.position.x - transform.position.x, playerTarget.position.y - transform.position.y);
        transform.up = direction / rotSpeed;

        

        if (distanceBetweenPlayerandBoss < shootingRange)
        {//if it is shorter than the shooting range it fires both cannon
            rHand.GetComponent<Turrets>().ShootCannons();
            lHand.GetComponent<Turrets>().ShootCannons();
        }
        else if (distanceBetweenPlayerandBoss > shootingRange)
        {//if not then it doesnt do anything
        }

    }

    void StrafeAndFire()
    {
        AimAtPlayerTarget();
        transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpot.position) < .2f)
        {
            if (waitingTime <= 0)
            {
                waitingTime = maxWaitTime;
                moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                speed = Random.Range(maxSpeed / 2, maxSpeed * 2);
            }
            else waitingTime -= Time.deltaTime;
        }
    }


    //movement sector
    void EngagePlayer()
    {
        AimAtPlayerTarget();

        if (distanceBetweenPlayerandBoss > stoppingDistance && distanceBetweenPlayerandBoss < attackingDistance)
        {//move to player if the distance between this pos and the player is greater than the stoppungditance the it will movetowards player
            transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, speed * .5f * Time.deltaTime);
     
        }
        else if (distanceBetweenPlayerandBoss < stoppingDistance && distanceBetweenPlayerandBoss > retreatDistance)
        {// if the distance is less than the stopping distance and greater than the retreat distance then the transform is inplace.
            transform.position = this.transform.position;
        }
        else if (distanceBetweenPlayerandBoss < retreatDistance)
        {//if the distance is less than the retreat then it runs away. at negative speed
            transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, -speed/2 * Time.deltaTime);
          
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerAmmo")
            TakeDamage(other.gameObject.GetComponent<ProjectileBehaviour>().damageOut);


    }

    public void TakeDamage(int damage)
    {
        activeBattery.batteryHP -= damage;
    }
}
