using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_QuickCharge : Enemy
{
    public bool isArmorEnemy;
    public GameObject chargeEffect;

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
        if (isArmorEnemy)
        {
            gameObject.tag = "Invulnerable";
        }

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
            indicator.transform.localScale = Vector3.zero;
            indicator.SetActive(false);
            chargeEffect.SetActive(false);
        }

        else
        {
            indicator.SetActive(true);
            chargeEffect.SetActive(true);
        }

        if (isArmorEnemy)
        {
            if (gameObject.CompareTag("enemy"))
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }

            else if (gameObject.CompareTag("Invulnerable"))
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            }
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
        Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2 animation = playerPos - origin;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion lookRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        indicator.transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 10f);
    }

    private void Charging_AI()
    {
        if (timeBtwChargeCounter <= 0)
        {
            if (isArmorEnemy)
            {
                gameObject.tag = "Invulnerable";
            }

            Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector2 direction = playerPos - origin;
            gameObject.GetComponent<Rigidbody2D>().velocity = direction.normalized * moveSpeed;
            StartCoroutine(Charge_Duration());
            timeBtwChargeCounter = timeBtwCharge;
            //gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            indicator.SetActive(false);

            if (gameObject.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
            {
                Set_Anim_Float(direction);
            }
        }

        else
        {
            float value = timeBtwCharge;
            Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector2 direction = playerPos - origin;
            timeBtwChargeCounter -= Time.deltaTime;

            float timer = timeBtwChargeCounter / timeBtwCharge;

            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.blue, Color.white, timer);
            indicator.transform.localScale = Vector3.Lerp(new Vector3(4f, 3f), Vector3.zero, timer);


            if (gameObject.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
            {
                Set_Anim_Float(direction);
            }
        }
    }

    private IEnumerator Charge_Duration ()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        isCharging = true;
        yield return new WaitForSeconds(timeBtwCharge/2);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        isCharging = false;

        if (isArmorEnemy)
        {
            gameObject.tag = "enemy";
        }
    }

    public IEnumerator Squish()
    {
        float t = 0;
        Vector3 orignalSize = transform.localScale;
        Vector3 newSize = new Vector3(0.8f, 1.3f, 1f);

        for (int i = 0; i < 1 / 0.3; i++)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, newSize, t);
            yield return new WaitForSeconds(0.05f);
            t += 0.5f;
        }

        yield return new WaitForEndOfFrame();

        t = 0;

        for (int i = 0; i < 1 / 0.3f; i++)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, orignalSize, t);
            yield return new WaitForSeconds(0.05f);
            t += 0.5f;
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
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        StartCoroutine(Squish());

        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerController>().states == playerStates.None)
            {
                if (isArmorEnemy)
                {
                    gameObject.tag = "Invulnerable";
                }

                FindObjectOfType<PlayerController>().Hurt_Player(baseAttack);
                Knock_Back_Player(collision);
                StopCoroutine(Charge_Duration());
            }

            else if (collision.gameObject.GetComponent<PlayerController>().states == playerStates.Charging)
            {
                Take_Damage(2);
                FindObjectOfType<Player_Knockback>().Knock_Back(gameObject.GetComponent<Collider2D>());

            }
        }

        else if (collision.gameObject.name != "Full Level")
        {
            if (isArmorEnemy)
            {
                gameObject.tag = "enemy";
            }

            StopCoroutine(Charge_Duration());
            Knock_Back_Me(gameObject);
        }
    }

    public void Knock_Away(GameObject me, Collision2D coll)
    {
        Rigidbody2D enemy = me.GetComponent<Rigidbody2D>();
        Debug.Log("Do knockback");

        if (enemy != null)
        {
            if (enemy.GetComponent<Enemy>().health > 0)
            {
                enemy.GetComponent<Enemy>().currentState = EnemyState.stagger;
                enemy.isKinematic = false;
                enemy.velocity = Vector2.zero;
                Vector2 difference = transform.position - coll.transform.position;
                difference = difference.normalized * thrust;
                Debug.Log(difference);
                enemy.velocity = difference;
                StartCoroutine(Knock(enemy));
            }
        }
    }

    private IEnumerator Knock(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            enemy.isKinematic = true;
            enemy.GetComponent<Enemy>().currentState = EnemyState.idle;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }

}
