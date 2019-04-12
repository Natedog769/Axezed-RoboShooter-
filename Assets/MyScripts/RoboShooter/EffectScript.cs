using UnityEngine;
public class EffectScript : MonoBehaviour {
    
    public float lifeTime;
    public float shakeAmp;
    public float shakeFreq;

    public delegate void ShakeCamera(EffectScript effect);
    public static event ShakeCamera EffectShakesCamera;

    private void Start()
    {
        EffectShakesCamera(this);
    }

    void Update ()
    {        
		Destroy(gameObject, lifeTime);
	}
}
