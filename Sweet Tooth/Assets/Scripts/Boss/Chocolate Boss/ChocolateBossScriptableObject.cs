using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boss", menuName = "Boss/Chocolate Boss Values", order = 1)]

public class ChocolateBossScriptableObject : ScriptableObject
{
    [Header("Generic Boss Stats")]
    public float maxHealth;
    public float phaseTwoHealth;
    public int damage;
    public GameObject player;

    [Header("Phase One stats")]
    public float chaseDuration;
    public float chaseSpeed;
    public float chargeSpeed;
    public float coolDownTime;

    [Header("Phase Two stats")]
    public int spikeDamage;
    public GameObject spike;
    public float coolDownTime2;


    [Header("Animations and particles")]
    public GameObject introPartices;
    public GameObject deathParticles;
    public AnimationClip spikeAttack;


    [HideInInspector]
    public float currHealth;




    public void Reset_Boss_Parameters ()
    {
        currHealth = maxHealth;
    }
}
