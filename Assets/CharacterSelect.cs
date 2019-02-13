using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterSelect : MonoBehaviour {

    //ui control will happen here instead of the player script
  

    public bool bluePlayer;
    public bool redPlayer;
    public bool greenPlayer;



    public GameObject bluePlayerObject;
    public GameObject redPlayerObject;
    public GameObject greenPlayerObject;


	void Awake () {

        


    }
	
	// Update is called once per frame
	void Update () {
        if (bluePlayer == true)
        {
            bluePlayerObject.gameObject.SetActive(true);
        }
        else if (redPlayer == true)
        {
            redPlayerObject.gameObject.SetActive(true);
        }
        else if (greenPlayer == true)
        {
            greenPlayerObject.gameObject.SetActive(true);
        }
        else
        {
            bluePlayerObject.gameObject.SetActive(false);
            redPlayerObject.gameObject.SetActive(false);
            greenPlayerObject.gameObject.SetActive(false);
        }
    }




}
