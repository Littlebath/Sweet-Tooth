using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Dormant : Enemy
{
    [Header("Specific enemy stats")]
    public Transform target;
    public float chaseRadius;
    public float attackRadius = 0.4f;
    public float explodeTime;
    public GameObject bigBomb;

    public bool isStandingBomb;
    public bool explodeOnDeath;

    [HideInInspector] public bool isAggressive;
    private float bombHealth;
    private float explodeTimeCounter;

    private bool beginFlash;
    private bool isFaster;
    // Start is called before the first frame update
    void Start()
    {
        explodeTimeCounter = explodeTime;
        anim = gameObject.GetComponent<Animator>();
        oldColor = gameObject.GetComponent<SpriteRenderer>().color;
        bombHealth = health - 2;
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

            if (health <= bombHealth)
            {
                ActivateBomb();
            }
        }
    }

    void ActivateBomb ()
    {
        Debug.Log("Start ticking");

        if (explodeTimeCounter <= 0)
        {
            Detonate();
        }

        else
        {
            explodeTimeCounter -= Time.deltaTime;
            StartCoroutine(Flash());
            StartCoroutine(IncreaseSpeed());

            if (isStandingBomb)
            {
                moveSpeed = 0;
            }
        }
    }

    private IEnumerator IncreaseSpeed ()
    {
        if (!isFaster)
        {
            isFaster = true;
            moveSpeed += 1;
            yield return null;
        }
    }

    public void Detonate ()
    {
        gameObject.GetComponent<Enemy_BloodImpact>().SpawnBlood();
        Debug.Log("Kaboom");
        explodeTimeCounter = explodeTime;
        GameObject bomb = Instantiate(bigBomb, transform.position, Quaternion.identity);
        StartCoroutine(bomb.GetComponent<Environment_ExplosiveNut>().Explode());
        Destroy(bomb, 0.5f);
        gameObject.SetActive(false);
    }

    private IEnumerator Flash ()
    {
        if (!beginFlash)
        {
            beginFlash = true;
            Debug.Log("start");

            for (int i = 0; i < 5; i++)
            {
                Debug.Log("Tick one");
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                yield return new WaitForSeconds(0.2f);
                transform.localScale = new Vector3(1f, 1f, 1f);
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                yield return new WaitForSeconds(0.2f);
            }


            for (int i = 0; i < 10; i++)
            {
                Debug.Log("Tick two");
                transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                yield return new WaitForSeconds(0.1f);
                transform.localScale = new Vector3(1f, 1f, 1f);
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    void Check_Distance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
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
                        Vector3 tempPos = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                        ChangeAnim(tempPos - transform.position);
                        gameObject.GetComponent<Rigidbody2D>().MovePosition(tempPos);
                        Change_State(EnemyState.walk);
                        anim.SetBool("isAwake", true);
                    }
                }

            }

            else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
            {
                //Debug.Log("Go to sleep");
                anim.SetBool("isAwake", false);
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
                Knock_Back_Me(gameObject);
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
