using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBattery : MonoBehaviour {

    public Image imageForHealth;
    public int batteryHP;
    public Text numberOfRecharges;


	// Use this for initialization
	void Start () {
        batteryHP = 10;
	}
	
	// Update is called once per frame
	void Update () {
        imageForHealth.fillAmount = batteryHP * .1f;
	}

   public void RechargeBattery()
    {
        batteryHP = 10;
    }

}
