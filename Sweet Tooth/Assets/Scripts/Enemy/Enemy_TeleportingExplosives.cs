using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_TeleportingExplosives : Enemy
{
    [Header("Specific enemy stats")]
    public Transform target;
    public float chaseRadius;
    [SerializeField] private float timeBtwBombThrow;
    [SerializeField] private GameObject bomb;
    [SerializeField] private Transform [] points;
    private int pointer;

    private float TimeBtwBombThrowCounter;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<Save_ObjState>() != null)
        {
            Debug.Log("Delete me");

            if (gameObject.GetComponent<Save_ObjState>().obj != null)
            {
                if (gameObject.GetComponent<Save_ObjState>().obj.saveState == 1)
                {
                    Destroy(gameObject);
                }
            }
        }

        TimeBtwBombThrowCounter = timeBtwBombThrow;
        anim = gameObject.GetComponent<Animator>();
        oldColor = gameObject.GetComponent<SpriteRenderer>().color;
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
                Set_Anim_Float(target.position - transform.position);
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

    void Throw_Bomb_AI()
    {
        if (TimeBtwBombThrowCounter <= 0)
        {
            //Throw a bloody bomb
            StartCoroutine(Throw_Bomb_And_Teleport());

        }

        else if (TimeBtwBombThrowCounter > 0)
        {
            TimeBtwBombThrowCounter -= Time.deltaTime;
        }
    }

    IEnumerator Throw_Bomb_And_Teleport ()
    {
        Instantiate(bomb, transform.position, Quaternion.identity);
        TimeBtwBombThrowCounter = timeBtwBombThrow;
        yield return new WaitForSeconds(2f);

        if (pointer >= points.Length - 1)
        {
            pointer = 0;
        }

        else
        {
            pointer++;
        }

        transform.position = points[pointer].position;
        //Teleport
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


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
