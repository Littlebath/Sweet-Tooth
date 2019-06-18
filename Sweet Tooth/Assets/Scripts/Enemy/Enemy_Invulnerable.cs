using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Invulnerable : Enemy
{
    [Header("Specific Stats")]
    public Transform[] points;
    public int pointIndicator;
    public float timeTillCharge;
    public float chargeDuration;
    public float stunTimeDuration;

    private Vector3 playerPos;
    private Vector3 dir;

    private float timeTillChargeCounter;
    private float chargeDurationCounter;
    private float stunTimeCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Patrolling ()
    {
        Debug.Log(Vector3.Distance(gameObject.transform.position, points[pointIndicator].position));

        if (Vector3.Distance(gameObject.transform.position, points[pointIndicator].position) <= 0.2f)
        {
            //Switch targets
            if (pointIndicator < points.Length - 1)
            {
                pointIndicator += 1;
            }

            else
            {
                pointIndicator = 0;
            }
        }

        else
        {
            //Patrolling
            Vector3 tempPos = Vector3.MoveTowards(gameObject.transform.position, points[pointIndicator].position, moveSpeed);
            GetComponent<Rigidbody2D>().MovePosition(tempPos);
        }
    }

    void ChargeUpDuration ()
    {
        if (chargeDurationCounter <= 0)
        {
            gameObject.GetComponent<Animator>().SetTrigger("charge");
            Debug.Log("Begin Charge");
            chargeDurationCounter = chargeDuration;
        }

        else
        {
            chargeDurationCounter -= Time.deltaTime;
        }
    }

    void Charging ()
    {

    }

    void Stunned ()
    {

    }

    void Recovery ()
    {

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
            }

            else if (dir.x < 0)
            {
                Set_Anim_Float(Vector2.left);
            }
        }

        else if (Mathf.Abs(dir.x) < Mathf.Abs(dir.y))
        {
            if (dir.y > 0)
            {
                Set_Anim_Float(Vector2.up);
            }

            else if (dir.y < 0)
            {
                Set_Anim_Float(Vector2.down);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Animator>().SetTrigger("endRecover");

            if (collision.gameObject.GetComponent<PlayerController>().states == playerStates.None)
            {
                FindObjectOfType<PlayerController>().Hurt_Player(baseAttack);
                Knock_Back_Player(collision);
                Knock_Back_Me(gameObject);
            }

            else if (collision.gameObject.GetComponent<PlayerController>().states == playerStates.Charging)
            {
                Take_Damage(2);
                FindObjectOfType<Player_Knockback>().Knock_Back(gameObject.GetComponent<Collider2D>());
            }
        }
        else
        {
            gameObject.GetComponent<Animator>().SetTrigger("stopCharging");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is in");
            gameObject.GetComponent<Animator>().SetBool("isFollowingPlayer", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is out");
            gameObject.GetComponent<Animator>().SetBool("isFollowingPlayer", false);
        }
    }

}
