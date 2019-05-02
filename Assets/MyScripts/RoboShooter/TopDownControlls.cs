using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopDownControlls : MonoBehaviour, ITakeDamage {
    //this is the Natedog 2d TopDownControls lots of public varibles and methods and states,
    
    //public Joystick joystick //this is for touchcontrols
    //lets start with the 3 hidden variables
    //these are objects that the player finds when it awakes and start. if there is no GM or UI or healthbattery the controlls dont work
    UIEventManager UI;
    GameManager GM { get { return GameManager.singleton; } }
    HealthBattery activeBattery;
    
    //these are speed variables they are set in the inspector
    public float moveSpeed;
    public int[] moveSpeedOptions;
    public float rotSpeed;
    //this is public for debug but really should be private
    public int xPEarned;

    [Space]
    public Sprite[] weapons;
    public ArmorSets[] blueArmorSelection;
    public ArmorSets[] greenArmorSelection;
    public ArmorSets[] redArmorSelection;
    public int powerLevel; //related to the number of batteries or lives
    [Space]
    /*these gameobjects are the sprites for the players bobdyparts this allows
    this allows for swappable sprites withe armorsets. Each armorset has a corresponding sprite bodypart
     the hands are for the guns are by default the sprites are empty so if there is any indicator that code is not right is when the weapon sprites are not showing up*/
    public GameObject currentHead;
    public GameObject currentTorso;
    public GameObject currentRArm;
    public GameObject currentLArm;
    public GameObject rHand;
    public GameObject lHand;
    //this is the gameobject with the effect script and animation of an explosion
    public EffectScript deathExplosion;
    public EffectScript rechargeEffect;
    public Shield activeShieldObject;

    public bool shieldIsActive = false;
    public bool leftHandIsEquipped = false;

    //boss variables
    //bool bossDefeated = false;
    bool greenBossHasBeenDefeated = false;
    bool redBossHasBeenDefeated = false;
    [HideInInspector]
    public int numbOfBossDef = 0;
    //these 2 variables will need to match to win
    public int numberofBossesOnThisLevelToKill;
    int numberOfBossesKilledToWin;
    //Animator anim;


    public enum StateArmorColorSet { blue = 0, green = 1, red = 2}
    public StateArmorColorSet colorState;

    public enum StateGame { lose = 0, win = 1, play = 2, pause = 3, bossWin = 4}
    public StateGame gameState;

    public enum StatePlayer { dead = 0, alive =1 };  // as far as i can tell this state has no effect on anything. it gets changed but it doesnt do anything
    public StatePlayer playerState;                   //the game state handles all the UI stuff

    public enum StateArmor { light = 0, medium = 1, heavy = 2}
    public StateArmor armorState;

    public enum StateGun { lvl1 = 0, lvl2 = 1, lvl3 = 2}
    public StateGun rHGunState;
    public StateGun lHGunState;
  

    void GMModule()
    {
        powerLevel = GM.numberOfPlayerBatteries;
        numbOfBossDef = GM.numOfBossesBeat;
        //tracks the state of the guns were last in
        if (GM.playerRHGunState == GameManager.StateGun.lvl1)
            rHGunState = StateGun.lvl1;
        if (GM.playerRHGunState == GameManager.StateGun.lvl2)
            rHGunState = StateGun.lvl2;
        if (GM.playerRHGunState == GameManager.StateGun.lvl3)
            rHGunState = StateGun.lvl3;
        if (GM.dualWieldWeapons == true)
        {//it checks to see if it was dual weild and to see what kind of thelvl it was at too
            leftHandIsEquipped = true;
            if (GM.playerLHGunState == GameManager.StateGun.lvl1)
                lHGunState = StateGun.lvl1;
            if (GM.playerLHGunState == GameManager.StateGun.lvl2)
                lHGunState = StateGun.lvl2;
            if (GM.playerLHGunState == GameManager.StateGun.lvl3)
                lHGunState = StateGun.lvl3;
        }
        //tracks the players color
        if (GM.playerColorState == GameManager.StatePlayerColor.blue)
            colorState = StateArmorColorSet.blue;
        if (GM.playerColorState == GameManager.StatePlayerColor.green)
            colorState = StateArmorColorSet.green;
        if (GM.playerColorState == GameManager.StatePlayerColor.red)
            colorState = StateArmorColorSet.red;
        //tracks the level of armor, light, medium and heavy.
        if (GM.playerArmorState == GameManager.StateArmor.light)
            armorState = StateArmor.light;
        if (GM.playerArmorState == GameManager.StateArmor.medium)
            armorState = StateArmor.medium;
        if (GM.playerArmorState == GameManager.StateArmor.heavy)
            armorState = StateArmor.heavy;
    }

    void Start () {
        GMModule();
        //anim = GetComponent<Animator>();
        playerState = StatePlayer.alive;
        //at the 

        Time.timeScale = 1f;
        gameState = StateGame.play;

        //these are the events the player is subcribed to
        //dont forget these if nothing is subscribed then it throws a null reference error
        EnemyTank.EnemyDiedEvent += OnEnemyDied;
        ObjectiveScript.CompleteObjectiveEvent += OnObjectComplete;
        ColoredBossScript.CompleteObjective += OnBossDefeat;

        UIModule();
        
        
	}

    void UIModule()
    {
        UI = FindObjectOfType<UIEventManager>();
        activeBattery = UI.playersBattery;
        UI.playScreen.gameObject.SetActive(true);
    }


    void OnBossDefeat (ColoredBossScript bossDeth)
    {//when a boss is defeated the player will add to the number of bossdef to unlock their armor
        //also brings up bosswin screen instead of the usal level complete screen
        numbOfBossDef++;
        numberOfBossesKilledToWin++;
    }

    void OnObjectComplete(ObjectiveScript targetThatFinished)
    {//when something with an objectivescript says that the target is finished it changes to win state
        //this is used with the touch the exit or goal
        gameState = StateGame.win;
    }

    void OnEnemyDied(EnemyTank enemyThatDied)
    {
        xPEarned += enemyThatDied.xPValue;
    }
	
    //update for all the frames it will be doing this
	void Update () {
        GameCheck();
        HealthCheck();
        WeaponCheck();
        ArmorCheck();
        XPCheck();
	}// end update
    //this just changes the active UI canvas which should now be done in the ui script
    //t
    void GameCheck()
    {
        if (gameState == StateGame.play)
        {
            UI.playScreen.gameObject.SetActive(true);
            UI.winScreen.gameObject.SetActive(false);
            UI.endScreen.gameObject.SetActive(false);
            UI.bossColorScreen.gameObject.SetActive(false);
            
            

            PlayerMovement();
            PlayerShoot();

        }
        else if (gameState == StateGame.pause)
        {
            //Time.timeScale = 0f;
        }
        else if (gameState == StateGame.bossWin)
        {
            UI.bossColorScreen.gameObject.SetActive(true);

            if (numbOfBossDef == 1)
                greenBossHasBeenDefeated = true;
            else if (numbOfBossDef > 1)
            {
                redBossHasBeenDefeated = true;
                greenBossHasBeenDefeated = true;

            }
        }
        else if (gameState == StateGame.win)
        {
            UI.winScreen.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (gameState == StateGame.lose)
        {
            UI.endScreen.gameObject.SetActive(true);
            UI.playScreen.gameObject.SetActive(false);
            UI.winScreen.gameObject.SetActive(false);           
            UI.bossColorScreen.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    void XPCheck()
    {
        //this is a private module the player will keep track of the xp for every level
        //this stat is not saved and does not carry over to the next levels for some form of challenge
        if (xPEarned >= 1000)
        {//once it hits 1000 it will add 1 to the powerlevel and the armor state up one level the reset back to 0
            powerLevel++;
            xPEarned = 0;
        }
        //this check is here to see if bosses are dead and will end the game then. the check for it to be bigger than 0 is so 0 isnt a true statement
        if(numberOfBossesKilledToWin == numberofBossesOnThisLevelToKill && numberofBossesOnThisLevelToKill > 0)
        {
            gameState = StateGame.bossWin;
        }

    }


    void PlayerMovement()
    {//these are the controls very basic
        //i added a strafe axis in the input system

        float horiz = Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed;
        float vert = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        float horzStrafe = Input.GetAxis("Strafe") * Time.deltaTime * moveSpeed * 1.5f;

        transform.Translate(new Vector2(horzStrafe, vert));
        transform.Rotate(new Vector3(0, 0, horiz));
    }
    
    //void PlayerTouchControls()
    //{ // to use with touch joypad ui elements
    //   //// float horizTurn;
    //    float vertMove;
    //    float horizMove;

    //    //transform.Translate(new Vector2(horzStrafe, vert));
    //    //transform.Rotate(new Vector3(0, 0, horiz));


    //    if (joystick.Horizontal >= .2)
    //    {
    //        horizMove = moveSpeed * Time.deltaTime;

    //    }
    //    else if (joystick.Horizontal <= -.2)
    //    {
    //        horizMove = -moveSpeed * Time.deltaTime;
    //    }
    //    else horizMove = 0;

    //    if (joystick.Vertical >= .2)
    //    {
    //        vertMove = moveSpeed * Time.deltaTime;

    //    }
    //    else if (joystick.Vertical <= -.2)
    //    {
    //        vertMove = -moveSpeed * Time.deltaTime;
    //    }
    //    else horizMove = 0;

    //}



    void HealthCheck()
    {//this is the health module. it takes the activebattery object and checks its HP count
        if (activeBattery.batteryHP <= 0)
        {//if it hits 0 it checks the powerlevel to see if it can recharge the battery
            if (powerLevel > 0)
            {//if its greater than 0 then it activates the activebattery built in method to recharge it at the cost of one power level.
                activeBattery.RechargeBattery();
                powerLevel--;
                Instantiate(rechargeEffect, transform.position, transform.rotation);
            }
            else
            {// if it cannot recharge [the powerlevel is at zero] then the game is lost

                gameState = StateGame.lose;
                playerState = StatePlayer.dead;
                Instantiate(deathExplosion, transform.position, transform.rotation);
                
            }
        }
        else
        {//if the health is not lower than 0 then the player state is alive adn the number of recharges is displayed
            playerState = StatePlayer.alive;
            activeBattery.numberOfRecharges.text = "X" + powerLevel.ToString();

        }

    }

    void PlayerShoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            rHand.GetComponent<WeaponScript>().RHFire();
            if (leftHandIsEquipped == true)
                lHand.GetComponent<WeaponScript>().LHFire();

            //anim.SetBool("isShooting", true);
        }
        //else anim.SetBool("isShooting", false);
    }

    int ArmorCheck()
    {//the armor check method has a private variable t
        int armorRate;
        armorRate = 0;

        if (colorState == StateArmorColorSet.blue)
        {
            if (armorState == StateArmor.heavy)
            {
                armorRate = 2;
                currentHead.GetComponent<SpriteRenderer>().sprite = blueArmorSelection[armorRate].head;
                currentTorso.GetComponent<SpriteRenderer>().sprite = blueArmorSelection[armorRate].torso;
                currentRArm.GetComponent<SpriteRenderer>().sprite = blueArmorSelection[armorRate].rightArm;
                currentLArm.GetComponent<SpriteRenderer>().sprite = blueArmorSelection[armorRate].leftArm;
            }
            else if (armorState == StateArmor.medium)
            {
                armorRate = 1;
                currentHead.GetComponent<SpriteRenderer>().sprite = blueArmorSelection[armorRate].head;
                currentTorso.GetComponent<SpriteRenderer>().sprite = blueArmorSelection[armorRate].torso;
                currentRArm.GetComponent<SpriteRenderer>().sprite = blueArmorSelection[armorRate].rightArm;
                currentLArm.GetComponent<SpriteRenderer>().sprite = blueArmorSelection[armorRate].leftArm;
            }
            else if (armorState == StateArmor.light)
            {
                armorRate = 0;
                currentHead.GetComponent<SpriteRenderer>().sprite = blueArmorSelection[armorRate].head;
                currentTorso.GetComponent<SpriteRenderer>().sprite = blueArmorSelection[armorRate].torso;
                currentRArm.GetComponent<SpriteRenderer>().sprite = blueArmorSelection[armorRate].rightArm;
                currentLArm.GetComponent<SpriteRenderer>().sprite = blueArmorSelection[armorRate].leftArm;
            }
        }

        if (colorState == StateArmorColorSet.green)
        {
            if (armorState == StateArmor.heavy)
            {
                armorRate = 2;
                currentHead.GetComponent<SpriteRenderer>().sprite = greenArmorSelection[armorRate].head;
                currentTorso.GetComponent<SpriteRenderer>().sprite = greenArmorSelection[armorRate].torso;
                currentRArm.GetComponent<SpriteRenderer>().sprite = greenArmorSelection[armorRate].rightArm;
                currentLArm.GetComponent<SpriteRenderer>().sprite = greenArmorSelection[armorRate].leftArm;
            }
            else if (armorState == StateArmor.medium)
            {
                armorRate = 1;
                currentHead.GetComponent<SpriteRenderer>().sprite = greenArmorSelection[armorRate].head;
                currentTorso.GetComponent<SpriteRenderer>().sprite = greenArmorSelection[armorRate].torso;
                currentRArm.GetComponent<SpriteRenderer>().sprite = greenArmorSelection[armorRate].rightArm;
                currentLArm.GetComponent<SpriteRenderer>().sprite = greenArmorSelection[armorRate].leftArm;
            }
            else if (armorState == StateArmor.light)
            {
                armorRate = 0;
                currentHead.GetComponent<SpriteRenderer>().sprite = greenArmorSelection[armorRate].head;
                currentTorso.GetComponent<SpriteRenderer>().sprite = greenArmorSelection[armorRate].torso;
                currentRArm.GetComponent<SpriteRenderer>().sprite = greenArmorSelection[armorRate].rightArm;
                currentLArm.GetComponent<SpriteRenderer>().sprite = greenArmorSelection[armorRate].leftArm;
            }
        }

        if (colorState == StateArmorColorSet.red)
        {
            if (armorState == StateArmor.heavy)
            {
                armorRate = 2;
                currentHead.GetComponent<SpriteRenderer>().sprite = redArmorSelection[armorRate].head;
                currentTorso.GetComponent<SpriteRenderer>().sprite = redArmorSelection[armorRate].torso;
                currentRArm.GetComponent<SpriteRenderer>().sprite = redArmorSelection[armorRate].rightArm;
                currentLArm.GetComponent<SpriteRenderer>().sprite = redArmorSelection[armorRate].leftArm;
            }
            else if (armorState == StateArmor.medium)
            {
                armorRate = 1;
                currentHead.GetComponent<SpriteRenderer>().sprite = redArmorSelection[armorRate].head;
                currentTorso.GetComponent<SpriteRenderer>().sprite = redArmorSelection[armorRate].torso;
                currentRArm.GetComponent<SpriteRenderer>().sprite = redArmorSelection[armorRate].rightArm;
                currentLArm.GetComponent<SpriteRenderer>().sprite = redArmorSelection[armorRate].leftArm;
            }
            else if (armorState == StateArmor.light)
            {
                armorRate = 0;
                currentHead.GetComponent<SpriteRenderer>().sprite = redArmorSelection[armorRate].head;
                currentTorso.GetComponent<SpriteRenderer>().sprite = redArmorSelection[armorRate].torso;
                currentRArm.GetComponent<SpriteRenderer>().sprite = redArmorSelection[armorRate].rightArm;
                currentLArm.GetComponent<SpriteRenderer>().sprite = redArmorSelection[armorRate].leftArm;
            }
        }

        return armorRate;
    }//end armor check

    void WeaponCheck()
    {
        //this is where the state is tied to the sprite that is showen in the right hand
        if (colorState == StateArmorColorSet.blue)
        {
            if (rHGunState == StateGun.lvl1)
            {
                rHand.GetComponent<SpriteRenderer>().sprite = weapons[0];
            }
            else if (rHGunState == StateGun.lvl2)
            {
                rHand.GetComponent<SpriteRenderer>().sprite = weapons[1];
            }
            else if (rHGunState == StateGun.lvl3)
            {
                rHand.GetComponent<SpriteRenderer>().sprite = weapons[2];
            }

            if (leftHandIsEquipped == true)
            {
                if (lHGunState == StateGun.lvl1)
                {
                    lHand.GetComponent<SpriteRenderer>().sprite = weapons[0];
                }
                else if (lHGunState == StateGun.lvl2)
                {
                    lHand.GetComponent<SpriteRenderer>().sprite = weapons[1];
                }
                else if (lHGunState == StateGun.lvl3)
                {
                    lHand.GetComponent<SpriteRenderer>().sprite = weapons[2];
                }
            }
            else lHand.GetComponent<SpriteRenderer>().sprite = null;
        }

        if (colorState == StateArmorColorSet.green)
        {
            if (rHGunState == StateGun.lvl1)
            {
                rHand.GetComponent<SpriteRenderer>().sprite = weapons[3];
            }
            else if (rHGunState == StateGun.lvl2)
            {
                rHand.GetComponent<SpriteRenderer>().sprite = weapons[4];
            }
            else if (rHGunState == StateGun.lvl3)
            {
                rHand.GetComponent<SpriteRenderer>().sprite = weapons[5];
            }

            if (leftHandIsEquipped == true)
            {
                if (lHGunState == StateGun.lvl1)
                {
                    lHand.GetComponent<SpriteRenderer>().sprite = weapons[3];
                }
                else if (lHGunState == StateGun.lvl2)
                {
                    lHand.GetComponent<SpriteRenderer>().sprite = weapons[4];
                }
                else if (lHGunState == StateGun.lvl3)
                {
                    lHand.GetComponent<SpriteRenderer>().sprite = weapons[5];
                }
            }
            else lHand.GetComponent<SpriteRenderer>().sprite = null;
        }

        if (colorState == StateArmorColorSet.red)
        {
            if (rHGunState == StateGun.lvl1)
            {
                rHand.GetComponent<SpriteRenderer>().sprite = weapons[6];
            }
            else if (rHGunState == StateGun.lvl2)
            {
                rHand.GetComponent<SpriteRenderer>().sprite = weapons[7];
            }
            else if (rHGunState == StateGun.lvl3)
            {
                rHand.GetComponent<SpriteRenderer>().sprite = weapons[8];
            }

            if (leftHandIsEquipped == true)
            {
                if (lHGunState == StateGun.lvl1)
                {
                    lHand.GetComponent<SpriteRenderer>().sprite = weapons[6];
                }
                else if (lHGunState == StateGun.lvl2)
                {
                    lHand.GetComponent<SpriteRenderer>().sprite = weapons[7];
                }
                else if (lHGunState == StateGun.lvl3)
                {
                    lHand.GetComponent<SpriteRenderer>().sprite = weapons[8];
                }
            }
            else lHand.GetComponent<SpriteRenderer>().sprite = null;
        }
    }

    //------------------------------------------------------changing color section-------------------------------------------------------------------------
    //these public methods are for the UI boss win screen buttons are directly tired to them.
    //these 2 functions are made to change the the color of the armorset the player is using
    //it checks to see if the right boss was defeated in a simple bool if not you cannot access that state of color
    public void ChangeToColorBlue()
    {
        Debug.Log("Changing to bluee");
        colorState = StateArmorColorSet.blue;
    }
      
    public void ChangeToColorGREEN()
    {
        if (greenBossHasBeenDefeated || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Endless"))
        {
            Debug.Log("Changing to GREEN");
            colorState = StateArmorColorSet.green;
        }
        else
        {
            UI.greenButtonLabel.text = "Need To Defeat The Green Boss";
        }
    }
    
    public void ChangeToColorRED()
    {
        if (redBossHasBeenDefeated || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Endless"))
        {
            Debug.Log("Changing to red");
            colorState = StateArmorColorSet.red;
        }else
        {
            UI.redButtonLabel.text = "Need To Defeat The Red Boss";
        }
    }
    //------------------------------------------------- end changing color section---------------------------------------------------------------------------

    private void OnTriggerEnter2D(Collider2D other)
    {//when the players trigger is entered the it will take the name
        Debug.Log("I am touching " + other.gameObject.name);
        //and cash it in a varibale
        GameObject hitItem = other.gameObject;

        if (hitItem.CompareTag("EnemyAmmo"))
        {//if that item is enemyammo then it will take the projectilebehaviours damage out vairable and takedamage with that amount
            ProjectileBehaviour hit = hitItem.GetComponent<ProjectileBehaviour>();
            if (hit != null)
                TakeDamage(hit.damageOut);

        }
        else if (hitItem.CompareTag("Enemy"))
        {//if the object is just an enemy itself them the player loses their second weapon
            if (leftHandIsEquipped == true) {
                leftHandIsEquipped = false;
                return;
            }// but if the player doesn have the left hand equipped then it will reduce the gunstate to 1
            else if (rHGunState > 0) {
                rHGunState = StateGun.lvl1 ;
                return;
            }
            else if (armorState > 0)
            {
                armorState = StateArmor.light;
            }

        }
        else PickUpCheck(hitItem);
    }

    public void TakeDamage(int damageForThePlayer)
    {//this is how the player takes damage it takes in the int of damage from the projectile and applies it to the battery
        if (shieldIsActive)
            activeShieldObject.ShieldTakesDamage(damageForThePlayer);
        else//shield is deavticated the new armor check returns an it and will reduce the damage by that internal power level ooh so sexy
        {
            FindObjectOfType<AudioManager>().Play("Damage2");
            activeBattery.batteryHP -= (damageForThePlayer - ArmorCheck());
        }
        /*the use of the batterying being an indepent object and the player no longer has to track a number for health genius!*/
    }

    public void ActivateShield()
    {/* 
        so the shield will work like this
        this module sets this bool true
        the shield is active
        if the shield is active the player applies the taken damage to the shield instead of the health batter

         */
        shieldIsActive = true;
        activeShieldObject.SetShieldActive();
        // the method above activates the shield to turn on the sprite 
    }

    public void DeActivateShield()
    {
        shieldIsActive = false;
    }

    public void ChangeArmorStateTo(int armorLevel)
    {
        if (armorLevel == 0) {
            armorState = StateArmor.light;
            moveSpeed = moveSpeedOptions[0];
        }
        if (armorLevel == 1)
        {
            armorState = StateArmor.medium;
            moveSpeed = moveSpeedOptions[1];
        }
        if (armorLevel == 2)
        {
            armorState = StateArmor.heavy;
            moveSpeed = moveSpeedOptions[2];
        }
    }

    public void PickUpCheck(GameObject pickUp)
    {
        /*so this module will take in a gameobject that should be pass in the trigger method
         * it will compareTag and change the state of that affected object i
         * if the left hand is not equipped nothing will happen when the player picks up a left handed weapon
        */
        if (pickUp.CompareTag("DualWield")) leftHandIsEquipped = true;
        if (pickUp.CompareTag("ShieldPickUp")) ActivateShield();
       
        //basic level upgrades for each hand 3 lvls
        if (pickUp.CompareTag("GunPickUP1")) 
        {
            rHGunState = StateGun.lvl1;
        }
        if (pickUp.CompareTag("LGunPickUp1"))
        {
            lHGunState = StateGun.lvl1;
        }

        if (pickUp.CompareTag("GunPickUP2"))
        {
            rHGunState = StateGun.lvl2;
        }
        if (pickUp.CompareTag("LGunPickUp2"))
        {
            lHGunState = StateGun.lvl2;
        }
        if (pickUp.CompareTag("GunPickUP3"))
        {
            rHGunState = StateGun.lvl3;
            
        }
        if (pickUp.CompareTag("LGunPickUp3"))
        {
            lHGunState = StateGun.lvl3;
        }

        if (pickUp.CompareTag("ArmorStation1"))
        {
            ChangeArmorStateTo(0);
        }

        if (pickUp.CompareTag("ArmorStation2"))
        {//level 2 check
            ChangeArmorStateTo(1);
        }

        if (pickUp.CompareTag("ArmorStation3"))
        {//level 3 check
            ChangeArmorStateTo(2);
        }
      

    }
}
