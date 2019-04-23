using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Knock_Back (Collider2D collision)
    {
        Rigidbody2D enemy = collision.gameObject.GetComponent<Rigidbody2D>();

        if (enemy != null)
        {
            if (enemy.GetComponent<Enemy>().health > 0)
            {
                enemy.GetComponent<Enemy>().currentState = EnemyState.stagger;
                enemy.GetComponent<Animator>().SetBool("isHurt", true);
                enemy.isKinematic = false;
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * thrust;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockCo(enemy));
            }
        }
    }

    private IEnumerator KnockCo(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            enemy.isKinematic = true;
            enemy.GetComponent<Enemy>().currentState = EnemyState.idle;
            enemy.GetComponent<Animator>().SetBool("isHurt", false);
        }

    }
}
