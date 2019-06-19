using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Player/Designer Values", order = 1)]

public class Player_ScriptableObject : ScriptableObject
{
    [Space]
    [Header("Player properties")]
    [Space]
    [Header("These change at run time even after play mode is over")]
    [Space (25)]

    //Designer Values
    [Header("Movement speed")]
    [Tooltip("How fast the player moves normally")]
    public float moveSpeed = 4.0f;

    [Header("Player health")]
    [Tooltip("How much health the player has")]
    public float maxHealth = 10;
    public float health = 10;
    public float maxArmor = 5;
    public float armor = 5;
    public float armorMultiplier;

    [Header("Abilities Unlocked")]
    public bool hasDash;
    public bool hasFireball;

    [Header("Player dash properties")]
    [Tooltip("How fast the player moves when dashing")]
    public float dashSpeed = 15f;
    [Tooltip("How long the player dashes for")]
    public float dashTime = 0.5f;
    [Tooltip("Energy needed for special attacks")]
    public int maxEnergy;
    [HideInInspector] public int energyCounter = 50;
    public float energyRegenerateTime;
    public int energyRegenerateAmount;

    [Header("Melee properties")]
    [Tooltip("How big is the melee collision box. It is a red circle in scene view")]
    public float meleeRange;
    [Tooltip("Damage of each melee strike")]
    public float meleeDamage;
    [Tooltip("How big the radius is when the player spins. Blue circle in scene view")]
    public float spinRange;
    [Tooltip("How big the radius is for the enenmy to be knocked up. Green circle in scene view")]
    public float knockUpRange;

    [Header("Boomerang properties")]
    [Tooltip("How much damage the boomerang does on impact")]
    public int boomerangDamage;
    [HideInInspector] public GameObject boomerang;
    [Tooltip("How fast the boomerang travels")]
    public float boomerangSpeed;
    [Tooltip("How far the boomerang travels")]
    public float boomerangDistance;
    [Tooltip("How fast the powered boomerang travels")]
    public float boomerangPowerSpeed;
    [Tooltip("How far the pwoered boomerang travels")]
    public float boomerangPowerDistance;
    [Tooltip("How quickly the boomerang rotates. Animation wise")]
    public float boomerangTurnSpeed;

    [Header("Fireball Properties")]
    public GameObject fireball;
    public float fireballSpeed;
    public int fireballDamage;
    public int fireballEnergyUse;
    public float destroyTime;
    public float fireballFireRate;
    public float burnDamageOverTime;
    public float burnTimeOnEnemy;
    public int fireballRecovery;

    [Header("Ground stomp properties")]
    public float chargeUpTime;
    public float areaOfEffectRange;
    public float groundPoundDamage;
    public float increaseKnockBack;
    public int energyConsumptionPound;

    [Header("Layer properties")]
    public LayerMask whatIsEnemy;
    public LayerMask whatIsBreakables;
    public LayerMask whatIsGum;
    public LayerMask whatIsRasgulla;
    public LayerMask whatIsNuts;
    public LayerMask whatIsCinnamon;

    [Header("Animation time durations")]
    public AnimationClip meleeTime;
    public AnimationClip throwTime;
    public AnimationClip knockUpTime;
    public AnimationClip groundPoundTime;

    [Header("Particle Effects")]
    public GameObject dust;
    public GameObject sparkle;
    public GameObject groundStompParticles;
    public GameObject smokeEffect;

    public void ResetValues ()
    {
        health = maxHealth;
        energyCounter = maxEnergy;
    }
}

