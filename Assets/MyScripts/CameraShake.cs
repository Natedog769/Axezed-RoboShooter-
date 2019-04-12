using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{

    public float ShakeDuration = 0.0f;          // Time the Camera Shake effect will last
    public float ShakeAmplitude = 0f;         // Cinemachine Noise Profile Parameter
    public float ShakeFrequency = 0f;         // Cinemachine Noise Profile Parameter

    private float ShakeElapsedTime = 0f;
    [Space]
    public float maxShakeDuration = 5;
    public float maxShakeAmp = 5;
    public float maxShakeFreq = 5;

    // Cinemachine Shake
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    

    // Use this for initialization
    void Start()
    {
        EffectScript.EffectShakesCamera += OnEffectShake;

        // Get Virtual Camera Noise Profile
        if (VirtualCamera != null)
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    void OnEffectShake(EffectScript firedEffect)
    {

        ShakeDuration = firedEffect.lifeTime;

        if (ShakeAmplitude < maxShakeAmp)
            ShakeAmplitude += firedEffect.shakeAmp;
        if(ShakeFrequency < maxShakeFreq)
            ShakeFrequency += firedEffect.shakeFreq;

        ShakeElapsedTime = ShakeDuration;
    }
    // Update is called once per frame
    void Update()
    {
        

        // If the Cinemachine componet is not set, avoid update
        if (VirtualCamera != null && virtualCameraNoise != null)
        {
            // If Camera Shake effect is still playing
            if (ShakeElapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                // Update Shake Timer
                ShakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                ShakeElapsedTime = 0f;

                ShakeFrequency = 0;
                ShakeDuration = 0;
                ShakeAmplitude = 0;

            }
        }
    }

    

}
