using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boss", menuName = "Boss/Oreo Chocolate Boss Values", order = 1)]

public class OreoBossScriptableObject : ScriptableObject
{
    [Header("Generic Boss Stats")]
    public float maxHealth;
    public float phaseTwoHealth;
    public float hurtAnimationDuration;
    public int damage;
    public GameObject player;
    public float chaseRadius;
    public GameObject bullet;

    [Header("Knockback stats")]
    public float thrust;
    public float knockTime;

    [Header("Phase 1 stats")]
    public float chaseDuration1;
    public float chaseSpeed1;
    public float dashBuildUpTime1;
    public float chargeSpeed1;
    public float dashCoolDown1;
    public int dashCount1;
    public float jumpBuildupTime1;
    public float jumpCooldownTime1;

    [Header("Phase 2 stats")]
    public float chaseDuration2;
    public float chaseSpeed2;
    public float dashBuildUpTime2;
    public float chargeSpeed2;
    public float dashCoolDown2;
    public int dashCount2;
    public float jumpBuildupTime2;
    public float jumpCooldownTime2;
    public float bulletSpeed;
    public float boulderFallSpeed;
    public GameObject[] boulders;
    public float boulderFallInterval;

    [Header("Animations and particles")]
    public GameObject introParticles;

    //Hidden variables
    [HideInInspector]
    public float currHealth;
    [HideInInspector]
    public bool bossIsActive;
    [HideInInspector]
    public int noOfDashes;



    public void Reset_Boss_Parameters()
    {
        currHealth = maxHealth;
        bossIsActive = false;
    }
}
