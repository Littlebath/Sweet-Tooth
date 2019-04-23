using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Light2D;

[CreateAssetMenu(fileName = "DayNight Cycle", menuName = "Daynight Cycle/Properties", order = 1)]
public class DayNightCycle_ScriptableObject : ScriptableObject
{
    [Space]
    [Header("Day night cycle attributes")]
    [Space]
    [Header("Changes made to the world's lighting")]
    [Space(10)]

    [Header("Colors of the sky")]
    public Color Night;
    public Color Noon;

    [Header("Transition from different times of day")]
    public float speed = 1.0f;

    [HideInInspector] public float secondsInFullDay;
    [Header("Minutes in real life equals one day in game")]
    public float minutesInFullDay = 1f;

    [Header("In Game Clock. DON'T TOUCH!")]
    [Range(0, 1)] public float currentTimeOfDay = 0;
}
