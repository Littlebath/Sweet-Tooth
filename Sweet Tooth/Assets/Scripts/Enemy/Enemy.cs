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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Take_Damage (float damage)
    {
        //Debug.Log("Hurt enemy");
        anim.SetTrigger("isHurt");
        health -= damage;

        if (health <= 0)
        {
            gameObject.SetActive(false);
            GameObject effect = Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);

            if (gameObject.GetComponent<Item_DropScript>() != null)
            {
                gameObject.GetComponent<Item_DropScript>().Spawn_Item();
            }

            if (shield != null)
            {
                Destroy(shield);
            }
        }

        if (gameObject.GetComponent<Enemy_Dormant>() != null)
        {
            gameObject.GetComponent<Enemy_Dormant>().isAggressive = true;
        }
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

