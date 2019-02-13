using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canister : ProjectileBehaviour {

    public Plasma itemToClusterExplode;
    public int numberOfCluster;

	void Start () {
		
	}
	
	// Update is called once per frame
	public override void Update () {
        //base.Update();

        lifeTime -= Time.deltaTime;

        transform.Translate(Vector2.up * speed * Time.deltaTime);
        if (lifeTime <= 0)
        {
            if (itemToClusterExplode != null)
                for (int i = 0; i < numberOfCluster; i++)
                {
                    Instantiate(itemToClusterExplode, transform.position, transform.rotation);
                }
            Destroy(gameObject);
        }



    }
}
