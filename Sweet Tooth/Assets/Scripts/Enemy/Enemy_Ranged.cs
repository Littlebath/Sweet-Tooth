using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootDir
{
    up,
    left,
    down,
    right, 
    turret
}


public class Enemy_Ranged : Enemy
{
    [Header("Ranged Enemy Variables")]
    public ShootDir shootDir;
    [SerializeField] float bulletSpeed;
    [SerializeField] float timeBtwShot;
    [SerializeField] float radius;
    Vector3 spawnPoint;
    Vector3 origin;
    float timeBtwShotCounter; 
    [SerializeField] private GameObject projectile;

    public GameObject direction;

    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        anim = gameObject.GetComponent<Animator>();
        Face_Direction(new Vector2(0F, 0F));
        timeBtwShotCounter = timeBtwShot;
        oldColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == EnemyState.idle)
        {
            if (Vector3.Distance(FindObjectOfType<PlayerController>().transform.position, transform.position) <= radius)
            {
                if (shootDir == ShootDir.turret)
                {
                    Turret_AI();
                }

                else
                {
                   //Nothing happens
                }
            }

            if (shootDir != ShootDir.turret)
            {
                Shooting_AI();
            }
        }
    }

    void Shooting_AI ()
    {
        if (timeBtwShotCounter <= 0)
        {
            //Debug.Log("Shoot bullet");
            GameObject bullet = Instantiate(projectile, spawnPoint, Quaternion.identity);
            gameObject.GetComponent<Animator>().SetTrigger("squish");
            bullet.GetComponent<EnemyRanged_Bullet>().damage = baseAttack;
            Vector2 speed = new Vector2 (0f, 0f);
            Face_Direction(speed);
            //Debug.Log(speed);
            bullet.GetComponent<Rigidbody2D>().velocity = Face_Direction (speed);
            //Face_Direction(bullet.GetComponent<Rigidbody2D>().velocity);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            timeBtwShotCounter = timeBtwShot;
        }

        else
        {
            timeBtwShotCounter -= Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.yellow, Color.white, timeBtwShotCounter);
        }

    }

    Vector2 Face_Direction (Vector2 velocity)
    {
        if (shootDir == ShootDir.up)
        {
            //Debug.Log("Shoot up");
            velocity = Vector2.up * bulletSpeed;
            spawnPoint = transform.position + Vector3.up;  
        }

        else if (shootDir == ShootDir.left)
        {
            //Debug.Log("Shoot left");
            velocity = Vector2.left * bulletSpeed;
            spawnPoint = transform.position + Vector3.left;
        }

        else if (shootDir == ShootDir.down)
        {
            //Debug.Log("Shoot down");
            velocity = Vector2.down * bulletSpeed;
            spawnPoint = transform.position + Vector3.down;
        }

        else if (shootDir == ShootDir.right)
        {
            //Debug.Log("Shoot right");
            velocity = Vector2.right * bulletSpeed;
            spawnPoint = transform.position + Vector3.right;
        }

        ChangeAnim(velocity);
        return velocity;
    }

    IEnumerator Squash ()
    {
        Vector3 originalSize = new Vector3(1f, 1f, 1f);
        Vector3 smallerSize = new Vector3(1.25f, 0.9f, 1f);
        transform.localScale = Vector3.SmoothDamp(transform.localScale, smallerSize, ref velocity, 0.2f);
        yield return new WaitForSeconds(0.2f);
        transform.localScale = Vector3.SmoothDamp(transform.localScale, originalSize, ref velocity, 0.2f);
    }

    void Turret_AI ()
    {
        if (timeBtwShotCounter <= 0)
        {
            Debug.Log("Shoot bullet");
            Vector3 aim = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
            spawnPoint = transform.position + aim.normalized;
            GameObject bullet = Instantiate(projectile, spawnPoint, Quaternion.identity);
            bullet.GetComponent<EnemyRanged_Bullet>().damage = baseAttack;
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed * aim.normalized;
            //Face_Direction(bullet.GetComponent<Rigidbody2D>().velocity);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            direction.SetActive(false);

            timeBtwShotCounter = timeBtwShot;
        }

        else
        {
            timeBtwShotCounter -= Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.red, Color.white, timeBtwShotCounter);


            Vector3 aim = direction.transform.position - FindObjectOfType<PlayerController>().transform.position;
            Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

            float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
            Quaternion lookRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            direction.transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 10f);
            direction.transform.localScale = Vector3.Lerp(new Vector3(1f, 1f), Vector3.zero, timeBtwShotCounter / timeBtwShot);
            direction.SetActive(true);
        }

        ChangeAnim(GameObject.FindGameObjectWithTag("Player").transform.position - transform.position);
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
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
