using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ThrowExplosives : Enemy
{
    [Header("Specific enemy stats")]
    public Transform target;
    public float chaseRadius;
    [SerializeField] private float timeBtwBombThrow;
    [SerializeField] private GameObject bomb;

    private float TimeBtwBombThrowCounter;

    // Start is called before the first frame update
    void Start()
    {
        TimeBtwBombThrowCounter = timeBtwBombThrow;
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PlayerController>() != null)
        {
            target = FindObjectOfType<PlayerController>().gameObject.transform;

            if (target != null)
            {
                Check_Distance();
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

    private void Change_State(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
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
