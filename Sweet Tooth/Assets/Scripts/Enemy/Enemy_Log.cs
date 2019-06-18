using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Log : Enemy
{
    [Header("Specific enemy stats")]
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public float timerJump;

    private Vector2 origin;
    private Vector2 playerPos;

    public float timeBtwChangeDirection;
    [HideInInspector] public float timeBtwChangeDirectionCounter;

    [HideInInspector] public Vector3 SuperPos;

    private float timer;

    bool isInAir;
    private float animation;
    // Start is called before the first frame update
    void Start()
    {
        timeBtwChangeDirectionCounter = timeBtwChangeDirection;
        timer = timerJump;
        anim = gameObject.GetComponent<Animator>();
        oldColor = gameObject.GetComponent<SpriteRenderer>().color;

        origin = transform.position;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
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
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            //Debug.Log(timer);
            Vector2 direction = target.transform.position - transform.position;
            float distance = Vector2.Distance(transform.position, target.transform.position);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance);
            Debug.DrawRay(transform.position, direction, Color.red);
            //Debug.Log(hit.collider.name);

            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                //Jumping in 
                if (isInAir)
                {
                    if (Vector2.Distance(transform.position, playerPos) <= 0.1f)
                    {
                        isInAir = false;
                        Debug.Log("Landed");
                    }

                    else
                    {
                        animation += Time.deltaTime;

                        animation = animation % 5;

                        Vector3 truePos = MathParabola.Parabola(origin, playerPos, 1f, animation / 1f);
                        gameObject.GetComponent<Rigidbody2D>().MovePosition(truePos);
                        //Debug.Log(truePos);
                        Debug.Log("In the air");
                    }
                }

                //Waiting on ground
                else
                {
                    if (timer <= 0)
                    {
                        origin = gameObject.transform.position;
                        timer = timerJump;
                        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
                        //Debug.Log(playerPos);
                        Debug.Log("Lift off");
                        isInAir = true;
                    }

                    else
                    {
                        animation = 0;
                        timer -= Time.deltaTime;
                        Debug.Log("Waiting");
                    }
                }

                Vector3 tempPos = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                SuperPos = tempPos - transform.position;
                StartCoroutine(ChangeAnim(SuperPos));
                //gameObject.GetComponent<Rigidbody2D>().MovePosition(tempPos);
                Change_State(EnemyState.walk);
                anim.SetBool("isAwake", true);

                if (shield != null)
                {
                    shield.SetActive(true);
                }
            }

        }

        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            //Debug.Log("Go to sleep");
            anim.SetBool("isAwake", false);

            if (shield != null)
            {
                shield.SetActive(false);
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
        if (timeBtwChangeDirectionCounter <= 0)
        {
            anim.SetFloat("moveX", setVector.x);
            anim.SetFloat("moveY", setVector.y);
        }

        else
        {
            timeBtwChangeDirectionCounter -= Time.deltaTime;
        }
    }

    private IEnumerator ChangeAnim (Vector3 dir)
    {
        if (Mathf.Abs (dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0)
            {
                yield return new WaitForSeconds(timeBtwChangeDirection);
                Set_Anim_Float(Vector2.right);
                //timeBtwChangeDirectionCounter = timeBtwChangeDirection;
            }

            else if (dir.x < 0)
            {
                yield return new WaitForSeconds(timeBtwChangeDirection);
                Set_Anim_Float(Vector2.left);
                //timeBtwChangeDirectionCounter = timeBtwChangeDirection;
            }
        }

        else if (Mathf.Abs(dir.x) < Mathf.Abs(dir.y))
        {
            if (dir.y > 0)
            {
                yield return new WaitForSeconds(timeBtwChangeDirection);
                Set_Anim_Float(Vector2.up);
                //timeBtwChangeDirectionCounter = timeBtwChangeDirection;
            }

            else if (dir.y < 0)
            {
                yield return new WaitForSeconds(timeBtwChangeDirection);
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
                isInAir = false;
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
