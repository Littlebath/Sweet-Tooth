using System.Collections;
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

    public GameObject shield;

    [HideInInspector] public Animator anim;

    private GameObject effect;

    private bool isHurt;
    public Color oldColor;

    // Start is called before the first frame update
    void Start()
    {

        if (gameObject.GetComponent<Save_ObjState>() != null)
        {
            if (gameObject.GetComponent<Save_ObjState>().obj != null)
            {
                if (gameObject.GetComponent<Save_ObjState>().obj.saveState == 1)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Make_Slow ()
    {
        moveSpeed -= 1;
        gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        yield return null;
    }

    public IEnumerator Make_Normal ()
    {
        moveSpeed += 1;
        gameObject.GetComponent<SpriteRenderer>().color = oldColor;
        yield return null;
    }

    public void Take_Damage (float damage)
    {
        if (!isHurt)
        {
            StartCoroutine(Flash());
            health -= damage;

            if (health <= 0)
            {
                gameObject.SetActive(false);
                effect = Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);

                if (gameObject.GetComponent<Save_ObjState>() != null)
                {
                    if (gameObject.GetComponent<Save_ObjState>().obj != null)
                    {
                        gameObject.GetComponent<Save_ObjState>().obj.saveState = 1;
                        gameObject.GetComponent<Save_ObjState>().obj.ForceSerialization();
                    }
                }

                FindObjectOfType<PlayerController>().GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                PlayerController.isPlayerHurt = false;
                Destroy(effect, 1f);
                Destroy(transform.parent.gameObject, 1f);

                if (gameObject.GetComponent<Item_DropScript>() != null)
                {
                    gameObject.GetComponent<Item_DropScript>().Spawn_Item();
                }

                if (shield != null)
                {
                    Destroy(shield);
                }
            }

            else
            {
                StartCoroutine(KnockCo(GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>()));
            }

            if (gameObject.GetComponent<Enemy_Dormant>() != null)
            {
                gameObject.GetComponent<Enemy_Dormant>().isAggressive = true;
            }
        }
    }

    private IEnumerator Flash ()
    {
        isHurt = true;

        for (int i = 0; i < 1 * 2; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<SpriteRenderer>().color = oldColor;
            yield return new WaitForSeconds(0.1f);
        }

        isHurt = false;
    }

    public void Knock_Back_Player(Collision2D collision)
    {
        Rigidbody2D enemy = collision.gameObject.GetComponent<Rigidbody2D>();

        if (enemy != null)
        {
            if (enemy.GetComponent<PlayerController>().designerValues.health > 0)
            {
                //Debug.Log("Hit");
                //enemy.isKinematic = true;
                Vector3 difference = enemy.transform.position - transform.position;
                //Debug.Log(difference);
                difference = difference.normalized * thrust;
                enemy.velocity = difference;
                PlayerController.isPlayerHurt = true;
                StartCoroutine(KnockCo(enemy));
            }
        }
    }

    public void Knock_Back_Me(GameObject me)
    {
        Rigidbody2D enemy = me.GetComponent<Rigidbody2D>();

        if (enemy != null)
        {
            if (enemy.GetComponent<Enemy>().health > 0)
            {
                enemy.GetComponent<Enemy>().currentState = EnemyState.stagger;
                enemy.isKinematic = false;
                Vector2 difference = transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;
                difference = difference.normalized * thrust;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockMe(enemy));
            }
        }
    }

    private IEnumerator KnockMe(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            yield return new WaitForSeconds(0.2f);
            enemy.isKinematic = true;
            enemy.GetComponent<Enemy>().currentState = EnemyState.idle;
        }
    }


    private IEnumerator KnockCo(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            PlayerController.isPlayerHurt = false;
        }
    }
}

