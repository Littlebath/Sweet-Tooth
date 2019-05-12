using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_QuickCharge : Enemy
{
    private Transform target;

    [SerializeField] private float chaseRadius;

    public GameObject indicator;

    [SerializeField] private float timeBtwCharge;
    float timeBtwChargeCounter;

    Vector2 origin;

    private bool isCharging;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        timeBtwChargeCounter = timeBtwCharge;
        oldColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        origin = transform.position;

        if (isCharging)
        {
            indicator.SetActive(false);
        }

        else
        {
            indicator.SetActive(true);
        }


        if (FindObjectOfType<PlayerController>() != null)
        {
            target = FindObjectOfType<PlayerController>().gameObject.transform;

            Charging_Indicator();

            if (target != null)
            {
                Check_Distance();
            }
        }

        if (gameObject.GetComponent<Save_ObjState>() != null)
        {
            if (gameObject.GetComponent<Save_ObjState>().obj.saveState == 1)
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }

    void Check_Distance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Charging_AI();
                Change_State(EnemyState.walk);
                anim.SetBool("isAwake", true);
            }
        }

        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            //Debug.Log("Go to sleep");
            anim.SetBool("isAwake", false);
            indicator.SetActive(false);
        }
    }

    void Charging_Indicator()
    {
        //indicator.transform.rotation = Quaternion.Euler (0f, 0f, moreTurn);

        Vector3 direction = indicator.transform.position - FindObjectOfType<PlayerController>().transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion lookRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        indicator.transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 10f);
    }

    private void Charging_AI()
    {
        if (timeBtwChargeCounter <= 0)
        {
            //Start to Charge
            Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector2 direction = playerPos - origin;
            gameObject.GetComponent<Rigidbody2D>().velocity = direction.normalized * moveSpeed;
            StartCoroutine(Charge_Duration());
            timeBtwChargeCounter = timeBtwCharge;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            indicator.SetActive(false);
        }

        else
        {
            timeBtwChargeCounter -= Time.deltaTime;
        }
    }

    private IEnumerator Charge_Duration ()
    {
        isCharging = true;
        yield return new WaitForSeconds(timeBtwCharge/2);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        isCharging = false;
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

        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }

}
