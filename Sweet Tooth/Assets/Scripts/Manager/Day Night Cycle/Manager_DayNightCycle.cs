using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Light2D;

public class Manager_DayNightCycle : MonoBehaviour
{
    private LightSprite sun;

    [SerializeField] private DayNightCycle_ScriptableObject properties;

    [HideInInspector] public float timeMultiplier = 1f;

    float sunInitialIntensity;
    float startTime;

    private bool isBrighter = true;
    private bool checkingIfBrighter;
    private bool checkingIfDarker;

    private Color sunColor = Color.white;
    private float sunAlpha;

    float t;
    float timer;

    void Start()
    {
        sun = gameObject.GetComponent<LightSprite>();
        //properties.currentTimeOfDay = 0;
        sunInitialIntensity = sun.Color.a;

        startTime = Time.time;
    }

    void Update()
    {

        properties.secondsInFullDay = properties.minutesInFullDay * 60f;

        UpdateSun();
        UpdateColor();

        if (properties.currentTimeOfDay >= 1)
        {
            //currentTimeOfDay = 0;
            isBrighter = false;
            t = 0;
        }

        else if (properties.currentTimeOfDay <= 0)
        {
            isBrighter = true;
            t = 0;
        }

        if (isBrighter)
        {
            properties.currentTimeOfDay += (Time.deltaTime / properties.secondsInFullDay) * timeMultiplier;
        }

        else
        {
            properties.currentTimeOfDay -= (Time.deltaTime / properties.secondsInFullDay) * timeMultiplier;
        }
    }

    void UpdateSun()
    {
        float intensityMultiplier = properties.currentTimeOfDay;

        sunAlpha = sunInitialIntensity * intensityMultiplier;
        sunColor.a = sunAlpha;
        sun.Color = sunColor;

         //Mathf.Clamp(sunAlpha, minSunLight, maxSunLight);
        //Debug.Log(sun.Color.a);
    }

    void UpdateColor()
    {
        //Debug.Log(t);


        if (isBrighter)
        {
            sunColor = Color.Lerp(new Color(properties.Night.r, properties.Night.g, properties.Night.b, sunColor.a), new Color(properties.Noon.r, properties.Noon.g, properties.Noon.b, properties.Noon.a), t);
        }

        else
        {
            sunColor = Color.Lerp(new Color(properties.Noon.r, properties.Noon.g, properties.Noon.b, sun.Color.a), new Color(properties.Night.r, properties.Night.g, properties.Night.b, sunColor.a), t);
        }


        /*if (properties.currentTimeOfDay >= 0.48f && properties.currentTimeOfDay <= 0.52f)
        {
            t = 0;           
        }*/

        t += Time.deltaTime * properties.speed;
    }

}
