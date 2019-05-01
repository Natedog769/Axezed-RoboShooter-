using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager singleton;

    public TopDownControlls thePlayer;
    public int numberOfPlayerBatteries = 0;
    public int numOfBossesBeat = 0;


    public enum StatePlayerColor { blue = 0, green = 1, red = 2 }
    public StatePlayerColor playerColorState;


    public bool dualWieldWeapons;

    public enum StateGun { lvl1 = 0, lvl2 = 1, lvl3 = 2 }
    public StateGun playerRHGunState;
    public StateGun playerLHGunState;

    public enum StateArmor { light = 0, medium = 1, heavy = 2 }
    public StateArmor playerArmorState;


    // Use this for initialization
    void Awake () {

        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            DestroyImmediate(gameObject);
            return;
        }

        //thePlayer = FindObjectOfType<TopDownControlls>();


        FindObjectOfType<AudioManager>().Play("Music1");
    }

    private void OnRenderObject()
    {
        thePlayer = FindObjectOfType<TopDownControlls>();
    }
   



    // Update is called once per frame
    void Update () {
        //everyframe the game manager keeps track of the players status it is setting the current state to its variables
        if (thePlayer != null)
        {
            CheckOnPlayersWeaponStatus();
            CheckOnPlayersArmorStatus();
            CheckPlayersBatteryCount();
        }
    }

    void CheckPlayersBatteryCount()
    {
        numberOfPlayerBatteries = thePlayer.powerLevel;
        numOfBossesBeat = thePlayer.numbOfBossDef;
    }

    void CheckOnPlayersArmorStatus()
    {
        if (thePlayer.colorState == TopDownControlls.StateArmorColorSet.blue)
            playerColorState = StatePlayerColor.blue;
        if (thePlayer.colorState == TopDownControlls.StateArmorColorSet.green)
            playerColorState = StatePlayerColor.green;
        if (thePlayer.colorState == TopDownControlls.StateArmorColorSet.red)
            playerColorState = StatePlayerColor.red;

        if (thePlayer.armorState == TopDownControlls.StateArmor.light)
            playerArmorState = StateArmor.light;
        if (thePlayer.armorState == TopDownControlls.StateArmor.medium)
            playerArmorState = StateArmor.medium;
        if (thePlayer.armorState == TopDownControlls.StateArmor.heavy)
            playerArmorState = StateArmor.heavy;
    }

    void CheckOnPlayersWeaponStatus()
    {
        if (thePlayer.rHGunState == TopDownControlls.StateGun.lvl1)
            playerRHGunState = StateGun.lvl1;
        if (thePlayer.rHGunState == TopDownControlls.StateGun.lvl2)
            playerRHGunState = StateGun.lvl2;
        if (thePlayer.rHGunState == TopDownControlls.StateGun.lvl3)
            playerRHGunState = StateGun.lvl3;

        if (thePlayer.leftHandIsEquipped == true)
        {
            dualWieldWeapons = true;
            if (thePlayer.lHGunState == TopDownControlls.StateGun.lvl1)
                playerLHGunState = StateGun.lvl1;
            if (thePlayer.lHGunState == TopDownControlls.StateGun.lvl2)
                playerLHGunState = StateGun.lvl2;
            if (thePlayer.lHGunState == TopDownControlls.StateGun.lvl3)
                playerLHGunState = StateGun.lvl3;
            
        }
        else if (thePlayer.leftHandIsEquipped == false)
        {
            dualWieldWeapons = false;
        }
    }
}
