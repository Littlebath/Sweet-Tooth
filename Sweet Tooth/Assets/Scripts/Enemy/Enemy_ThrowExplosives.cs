﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ThrowExplosives : Enemy
{
    [Header("Specific enemy stats")]
    public Transform target;
    public float chaseRadius;
    [SerializeField] private float timeBtwBombThrow;
    [SerializeField] private GameObject bomb;

    public float timeBtwCycle;
    public float numberOfBombs;

    private float TimeBtwBombThrowCounter;
    private float timeBtwThrowsCounter;
    private Vector3 origin;
    private Vector2 StickMe;

    public bool isBoss;

    // Start is called before the first frame update
    void Start()
    {
        TimeBtwBombThrowCounter = timeBtwBombThrow;
        anim = gameObject.GetComponent<Animator>();
        oldColor = gameObject.GetComponent<SpriteRenderer>().color;
        StickMe = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PlayerController>() != null)
        {
            target = FindObjectOfType<PlayerController>().gameObject.transform;
            origin = transform.position;
            

            if (target != null)
            {
                Check_Distance();
                Set_Anim_Float(target.position - origin);
            }
        }
    }


    void Check_Distance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Change_State(EnemyState.walk);
                //Throwing Bombs stuff
                Throw_Bomb_AI();
                anim.SetBool("isAwake", true);
            }
        }

        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            //Debug.Log("Go to sleep");
            anim.SetBool("isAwake", false);
        }
    }

    void Throw_Bomb_AI ()
    {
        if (!isBoss)
        {        
            if (TimeBtwBombThrowCounter <= 0)
            {
                //Throw a bloody bomb
                Instantiate(bomb, transform.position, Quaternion.identity);
                TimeBtwBombThrowCounter = timeBtwBombThrow;
            }

            else
            {
                TimeBtwBombThrowCounter -= Time.deltaTime;
            }
        }

        else
        {
            if (TimeBtwBombThrowCounter <= 0)
            {
                StartCoroutine(ThrowManyBombs());
            }

            else
            {
                TimeBtwBombThrowCounter -= Time.deltaTime;
                gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.red, Color.white, TimeBtwBombThrowCounter);
            }
        }

    }

    private IEnumerator ThrowManyBombs ()
    {
        TimeBtwBombThrowCounter = timeBtwBombThrow + (timeBtwBombThrow * numberOfBombs);
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;

        for (int i = 0; i < numberOfBombs; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            Instantiate(bomb, transform.position, Quaternion.identity);
            StartCoroutine(Squish());
            Debug.Log("Thrown a bomb");
            yield return new WaitForSeconds(timeBtwBombThrow);
        }

        yield return new WaitForEndOfFrame();
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }

    public IEnumerator Squish()
    {
        float t = 0;
        Vector3 orignalSize = transform.localScale;
        Vector3 newSize = new Vector3(0.6f, 1f, 1f);

        for (int i = 0; i < 1 / 0.3; i++)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, newSize, t);
            yield return new WaitForSeconds(0.05f);
            t += 0.3f;
        }

        yield return new WaitForEndOfFrame();

        t = 0;

        for (int i = 0; i < 1 / 0.3f; i++)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, orignalSize, t);
            yield return new WaitForSeconds(0.05f);
            t += 0.3f;
        }
    }

    private void Change_State(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    private void Set_Anim_Float(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    private void ChangeAnim(Vector3 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0)
            {
                Set_Anim_Float(Vector2.right);
                //timeBtwChangeDirectionCounter = timeBtwChangeDirection;
            }

            else if (dir.x < 0)
            {
                Set_Anim_Float(Vector2.left);
                //timeBtwChangeDirectionCounter = timeBtwChangeDirection;
            }
        }

        else if (Mathf.Abs(dir.x) < Mathf.Abs(dir.y))
        {
            if (dir.y > 0)
            {
                Set_Anim_Float(Vector2.up);
                //timeBtwChangeDirectionCounter = timeBtwChangeDirection;
            }

            else if (dir.y < 0)
            {
                Set_Anim_Float(Vector2.down);
                //timeBtwChangeDirectionCounter = timeBtwChangeDirection;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerController>().states == playerStates.None)
            {
                FindObjectOfType<PlayerController>().Hurt_Player(baseAttack);
                Knock_Back_Player(collision);
            }

            else if (collision.gameObject.GetComponent<PlayerController>().states == playerStates.Charging)
            {
                Take_Damage(2);
                FindObjectOfType<Player_Knockback>().Knock_Back(gameObject.GetComponent<Collider2D>());
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
