using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {

    public enum StatePlayerColor { blue = 0, green = 1, red = 2 }
    public StatePlayerColor playerColorState;

    public MGBullet bullet;
    public Laser laser;
    public Missile missile1;
    public Rocket rocket;

    TopDownControlls thePlayer;



	void Start () {
        thePlayer = FindObjectOfType<TopDownControlls>();
	}
	
	// Update is called once per frame
	void Update () {

        CheckOnPlayersArmorStatus();


    }

    void CheckOnPlayersArmorStatus()
    {
        if (thePlayer.colorState == TopDownControlls.StateArmorColorSet.blue)
            playerColorState = StatePlayerColor.blue;
        if (thePlayer.colorState == TopDownControlls.StateArmorColorSet.green)
            playerColorState = StatePlayerColor.green;
        if (thePlayer.colorState == TopDownControlls.StateArmorColorSet.red)
            playerColorState = StatePlayerColor.red;
    }

    public void RHFire()
    {
        if (playerColorState == StatePlayerColor.green)
        {
            if (GetComponentInParent<TopDownControlls>().rHGunState == TopDownControlls.StateGun.lvl1)
                StartCoroutine(FireRocket());

            if (GetComponentInParent<TopDownControlls>().rHGunState == TopDownControlls.StateGun.lvl2)
            {
                StartCoroutine(FireMissile());
            }

            if (GetComponentInParent<TopDownControlls>().rHGunState == TopDownControlls.StateGun.lvl3)
            {
                StartCoroutine(FireBullets());
            }
        }
        else if (playerColorState == StatePlayerColor.red)
        {
            if (GetComponentInParent<TopDownControlls>().rHGunState == TopDownControlls.StateGun.lvl1)
                StartCoroutine(FireRocket());

            if (GetComponentInParent<TopDownControlls>().rHGunState == TopDownControlls.StateGun.lvl2)
            {
                    Instantiate(laser, transform.position, transform.rotation);
                }

            if (GetComponentInParent<TopDownControlls>().rHGunState == TopDownControlls.StateGun.lvl3)
            {
                StartCoroutine(FireBullets());
            }
        }
        else //blue unit
        {
            if (GetComponentInParent<TopDownControlls>().rHGunState == TopDownControlls.StateGun.lvl1)
                Instantiate(laser, transform.position, transform.rotation);

            if (GetComponentInParent<TopDownControlls>().rHGunState == TopDownControlls.StateGun.lvl2)
            {
                StartCoroutine(FireBullets());
            }

            if (GetComponentInParent<TopDownControlls>().rHGunState == TopDownControlls.StateGun.lvl3)
            {
                StartCoroutine(FireMissile());
            }
        }

    }

    public void LHFire()
    {
        if (playerColorState == StatePlayerColor.green)
        {
            if (GetComponentInParent<TopDownControlls>().lHGunState == TopDownControlls.StateGun.lvl1)
                StartCoroutine(FireRocket());

            if (GetComponentInParent<TopDownControlls>().lHGunState == TopDownControlls.StateGun.lvl2)
            {
                StartCoroutine(FireMissile());
            }

            if (GetComponentInParent<TopDownControlls>().lHGunState == TopDownControlls.StateGun.lvl3)
            {
                StartCoroutine(FireBullets());
            }
        }
        else if (playerColorState == StatePlayerColor.red)
        {
            if (GetComponentInParent<TopDownControlls>().lHGunState == TopDownControlls.StateGun.lvl1)
                StartCoroutine(FireRocket());

            if (GetComponentInParent<TopDownControlls>().lHGunState == TopDownControlls.StateGun.lvl2)
            {
                Instantiate(laser, transform.position, transform.rotation);
            }

            if (GetComponentInParent<TopDownControlls>().lHGunState == TopDownControlls.StateGun.lvl3)
            {
                StartCoroutine(FireBullets());
            }
        }
        else {//blue unit
            if (GetComponentInParent<TopDownControlls>().lHGunState == TopDownControlls.StateGun.lvl1)
                Instantiate(laser, transform.position, transform.rotation);

            if (GetComponentInParent<TopDownControlls>().lHGunState == TopDownControlls.StateGun.lvl2)
            {
                StartCoroutine(FireBullets());
            }

            if (GetComponentInParent<TopDownControlls>().lHGunState == TopDownControlls.StateGun.lvl3)
            {
                StartCoroutine(FireMissile());
            }
        }
    }
    
    IEnumerator FireBullets()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        yield return new WaitForSeconds(.1f);
        Instantiate(bullet, transform.position, transform.rotation);
        yield return new WaitForSeconds(.1f);
        Instantiate(bullet, transform.position, transform.rotation);
    }

    IEnumerator FireMissile()
    {
        Instantiate(missile1, transform.position, transform.rotation);
        yield return new WaitForSeconds(.01f);
        Instantiate(missile1, transform.position, transform.rotation);

    }

    IEnumerator FireRocket()
    {
        Instantiate(rocket, transform.position, transform.rotation);
        yield return new WaitForSeconds(.25f);
        Instantiate(rocket, transform.position, transform.rotation);
    }


}

//so i can go about this in a couple of ways we can make different projectile types such as laser and 