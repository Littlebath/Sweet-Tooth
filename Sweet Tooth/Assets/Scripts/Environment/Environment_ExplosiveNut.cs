using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_ExplosiveNut : MonoBehaviour
{
    [SerializeField] private AnimationClip destroyTime;
    [SerializeField] private int damage;
    [SerializeField] private float explosionRange;
    [SerializeField] private LayerMask whatToHit;
    [SerializeField] private bool isEnemyBomb;
    private bool isExploding;


    private Vector2 origin;
    private Vector2 playerPos;
    private float animation;
    // Start is called before the first frame update
    void Start()
    {
        if (isEnemyBomb)
        {
            Destroy(gameObject.GetComponent<Item>());
            origin = gameObject.transform.position;
            playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            //gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = transform.position;

        if (isEnemyBomb)
        {
            animation += Time.deltaTime;

            animation = animation % 5;

            Vector3 tempPos = MathParabola.Parabola(origin, playerPos, 1f, animation / 1f);
            gameObject.GetComponent<Rigidbody2D>().MovePosition(tempPos);

            if (Vector2.Distance (transform.position, playerPos) <= 0.1f)
            {
                Debug.Log("Explode");

                if (!isExploding)
                {
                    StartCoroutine(Explode());
                }
            }
        }
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (isEnemyBomb)
        {
            if (collision.gameObject.name != "Full level" || collision.gameObject.name != gameObject.name)
            {
                if (!isExploding)
                {
                    StartCoroutine(Explode());
                    Debug.Log(collision.gameObject.name);
                }
            }
        }
    }

    public IEnumerator Explode ()
    {
        isExploding = true;
        gameObject.GetComponent<Animator>().SetBool("explode", true);
        //gameObject.transform.GetChild(0).gameObject.SetActive(true);

        Collider2D[] breakablesToDestroy = Physics2D.OverlapCircleAll(transform.position, explosionRange, whatToHit);

        for (int i = 0; i < breakablesToDestroy.Length; i++)
        {
            Vector2 direction = breakablesToDestroy[i].transform.position - transform.position;
            float distance = Vector2.Distance(transform.position, breakablesToDestroy[i].transform.position);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance);

            if (hit.collider == breakablesToDestroy[i])
            {
                if (breakablesToDestroy[i].gameObject.layer == 8)
                {
                    //Player
                    FindObjectOfType<PlayerController>().Hurt_Player(damage);
                    Knock_Back_Player(breakablesToDestroy[i]);
                }

                else if (breakablesToDestroy[i].gameObject.layer == 10)
                {
                    //Enemy
                    breakablesToDestroy[i].GetComponent<Enemy>().Take_Damage(damage);
                    FindObjectOfType<Player_Knockback>().Knock_Back(breakablesToDestroy[i]);
                }

                else if (breakablesToDestroy[i].gameObject.layer == 18)
                {
                    //Some Door
                    Destroy(breakablesToDestroy[i].gameObject);
                }
            }

        }

        isEnemyBomb = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        FindObjectOfType<PlayerController>().GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        FindObjectOfType<PlayerController>().GetComponent<Rigidbody2D>().isKinematic = false;
        yield return null;
    }


    public void Knock_Back_Player(Collider2D collision)
    {
        Rigidbody2D enemy = collision.gameObject.GetComponent<Rigidbody2D>();

        if (enemy != null)
        {
            if (enemy.GetComponent<PlayerController>().designerValues.health > 0)
            {
                //Debug.Log("Hit");
                enemy.isKinematic = true;
                Vector2 difference = enemy.transform.position - transform.position;
                //Debug.Log(difference);
                difference = difference.normalized * 2;
                enemy.velocity = difference;
                StartCoroutine(KnockCo(enemy));
            }
        }
    }

    private IEnumerator KnockCo(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(0.4f);
            enemy.velocity = Vector2.zero;
            enemy.isKinematic = false;
        }

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);

    }
}
