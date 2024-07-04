using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightFlickering : MonoBehaviour
{
    private new Light light;
    public int minIntensity = 6, maxIntensity = 15;
    private int graceEndPeriod = -1, flickerEndTime =-1;
    bool isFlickering = false ;
    float targetIntensity = 5;

    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlickering)
        {
            targetIntensity = (float)Random.Range(minIntensity, maxIntensity);
            
        }
        light.intensity = Mathf.Lerp(light.intensity, targetIntensity, 0.5f);

        if (Time.time > flickerEndTime)
        {
            isFlickering = false;
            flickerEndTime = -1;
        }

        if (graceEndPeriod == -1)
        {
            graceEndPeriod = ComputeNewEndPeriod(2,25);
        }

        if ((int)Time.time > graceEndPeriod)
        {
            isFlickering=true;
            flickerEndTime = ComputeNewEndPeriod(1, 5);
            graceEndPeriod=-1;
        }
       

    }

    private void Flicker()
    {
        float targetIntensity = (float)Random.Range(minIntensity, maxIntensity);
        light.intensity = Mathf.Lerp(light.intensity, targetIntensity, 0.5f);

        if(Time.time > flickerEndTime)
        {
            isFlickering=false;
            flickerEndTime = -1;
        }
    }

    private int ComputeNewEndPeriod(int min, int max)
    {
        return (int)Time.time + Random.Range(min, max);
    }
}
