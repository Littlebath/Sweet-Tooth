﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}


public class Enemy : MonoBehaviour
{
    [Header("Generic enemy stats")]
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public EnemyState currentState;
    public GameObject enemyDeathEffect;

    [Header("Knock back stats")]
    public float thrust;
    public float knockTime;

    [HideInInspector] public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Take_Damage (float damage)
    {
        //Debug.Log("Hurt enemy");
        anim.SetTrigger("isHurt");
        health -= damage;

        if (health <= 0)
        {
            gameObject.SetActive(false);
            GameObject effect = Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }

        if (gameObject.GetComponent<Enemy_Dormant>() != null)
        {
            gameObject.GetComponent<Enemy_Dormant>().isAggressive = true;
        }
    }

    public void Knock_Back_Player(Collision2D collision)
    {
        Rigidbody2D enemy = collision.gameObject.GetComponent<Rigidbody2D>();

        if (enemy != null)
        {
            if (enemy.GetComponent<PlayerController>().designerValues.health > 0)
            {
                //Debug.Log("Hit");
                enemy.isKinematic = true;
                Vector2 difference = enemy.transform.position - transform.position;
                //Debug.Log(difference);
                difference = difference.normalized * thrust;
                enemy.velocity = difference;
                StartCoroutine(KnockCo(enemy));
            }
        }
    }

    private IEnumerator KnockCo(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            enemy.isKinematic = false;
        }

    }
}
