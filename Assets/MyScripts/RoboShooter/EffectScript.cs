using UnityEngine;
public class EffectScript : MonoBehaviour {
    public float lifeTime;
	void Update () {
		Destroy(gameObject, lifeTime);
	}
}
