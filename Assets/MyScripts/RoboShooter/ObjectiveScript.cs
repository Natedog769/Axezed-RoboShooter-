using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveScript : MonoBehaviour {
    
    public bool destroyToWin;
    public bool touchToWin;


    public delegate void ObjectiveComplete(ObjectiveScript target);
    public static event ObjectiveComplete CompleteObjectiveEvent;

    private void Update()
    {
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (touchToWin == true && collision.tag == "Player")
            CompleteObjectiveEvent(this);
    }
}
