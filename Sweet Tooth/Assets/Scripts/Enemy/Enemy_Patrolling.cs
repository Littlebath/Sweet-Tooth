using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyBehavior
{
    Patrolling,
    Chasing,
}

public class Enemy_Patrolling : Enemy
{
    [Header("Patrolling Enemy Variables")]
    [SerializeField] private Transform[] points;
    [SerializeField] private int indicator;
    public enemyBehavior EnemyBehavior;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PlayerController>() != null)
        {
            target = FindObjectOfType<PlayerController>().gameObject.transform;
        }

        EnemyAI();
    }

    void EnemyAI ()
    {
        if (currentState == EnemyState.idle)
        {
            if (EnemyBehavior == enemyBehavior.Patrolling)
            {
                Patrolling_Behavior();
            }

            else if (EnemyBehavior == enemyBehavior.Chasing)
            {
                //Chase the player
                if (target != null)
                {
                    Check_Distance();
                }
            }
        }
    }
    void Check_Distance()
    {
        Vector2 direction = target.transform.position - transform.position;
        float distance = Vector2.Distance(transform.position, target.transform.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance);
        Debug.DrawRay(transform.position, direction, Color.red);
        //Debug.Log(hit.collider.name);

        if (hit == true)
        {
            if (hit.collider.name == target.gameObject.name)
            {
                if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
                {
                    Vector3 currPos = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                    ChangeAnim(currPos - transform.position);
                    gameObject.GetComponent<Rigidbody2D>().MovePosition(currPos);
                    Change_State(EnemyState.idle);
                    //Debug.Log(currPos);

                    if (shield != null)
                    {
                        shield.SetActive(true);
                    }
                }
            }

            else
            {
                Debug.Log("Player missing");
            }
        }
    }

    void Patrolling_Behavior()
    {
        Mathf.Clamp(indicator, 0, points.Length - 1);
        Change_State(EnemyState.idle);
        Vector3 tempPos = Vector3.MoveTowards(transform.position, points[indicator].position, moveSpeed * Time.deltaTime);
        ChangeAnim(tempPos - transform.position);
        gameObject.GetComponent<Rigidbody2D>().MovePosition(tempPos);
        Switch_Target();
    }




    void Switch_Target ()
    {
        if (transform.position == points[indicator].position)
        {
            if (indicator >= points.Length -1)
            {
                indicator = 0;
            }

            else
            {
                indicator++;
            }
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
}
