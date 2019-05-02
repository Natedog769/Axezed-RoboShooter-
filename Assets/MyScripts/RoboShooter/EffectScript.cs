using UnityEngine;
public class EffectScript : MonoBehaviour {
    
    public float lifeTime;
    public float shakeAmp;
    public float shakeFreq;
    public string audioClipName;

    public delegate void ShakeCamera(EffectScript effect);
    public static event ShakeCamera EffectShakesCamera;

    private void Start()
    {
        //at the start of the effect it will play its sound and shake the camera
        EffectShakesCamera(this);
        if (audioClipName.Length != 0)
            FindObjectOfType<AudioManager>().Play(audioClipName);
    }

    void Update ()
    {        

		Destroy(gameObject, lifeTime);
	}
}
