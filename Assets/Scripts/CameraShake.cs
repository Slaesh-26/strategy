using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeTime = 0.5f;
    [SerializeField] private float shakeAmount = 0.7f; 
    [SerializeField] private float decreaseFactor = 1.0f;

    private float shakeDuration = 0f;
    private Vector3 originalPos;

    private bool isShaking;

    public void Shake()
    {
        shakeDuration = shakeTime;
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = originalPos;
        }
    }
}
