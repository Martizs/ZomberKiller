using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField]
    float flashLightDecay = .1f;

    [SerializeField]
    float angleDecay = .5f;

    [SerializeField]
    float minimumAngle = 25f;

    Light myLight;
    float initAngle;
    float initIntensity;

    private void Start()
    {
        myLight = GetComponent<Light>();
        initAngle = myLight.spotAngle;
        initIntensity = myLight.intensity;
    }

    private void Update()
    {
        DecreaseLightAngle();
        DecreaseLightIntensity();
    }

    public float GetLightIntensity()
    {
        return myLight.intensity;
    }

    public void RestoreLightAngle()
    {
        myLight.spotAngle = initAngle;
    }

    public void RestoreLightIntensity()
    {
        myLight.intensity = initIntensity;
    }

    void DecreaseLightAngle()
    {
        if (myLight.spotAngle > minimumAngle)
        {
            myLight.spotAngle -= angleDecay * Time.deltaTime;
        }
    }

    void DecreaseLightIntensity()
    {
        myLight.intensity -= flashLightDecay * Time.deltaTime;
    }
}
