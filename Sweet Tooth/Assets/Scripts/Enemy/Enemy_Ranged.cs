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
    Vector3 spawnPoint;
    Vector3 origin;
    float timeBtwShotCounter; 
    [SerializeField] private GameObject projectile;

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
            if (shootDir != ShootDir.turret)
            {
                Shooting_AI();
            }

            else
            {
                Turret_AI();
            }
        }
    }

    void Shooting_AI ()
    {
        if (timeBtwShotCounter <= 0)
        {
            //Debug.Log("Shoot bullet");
            GameObject bullet = Instantiate(projectile, spawnPoint, Quaternion.identity);
            bullet.GetComponent<EnemyRanged_Bullet>().damage = baseAttack;
            Vector2 speed = new Vector2 (0f, 0f);
            Face_Direction(speed);
            //Debug.Log(speed);
            bullet.GetComponent<Rigidbody2D>().velocity = Face_Direction (speed);
            //Face_Direction(bullet.GetComponent<Rigidbody2D>().velocity);

            timeBtwShotCounter = timeBtwShot;
        }

        else
        {
            timeBtwShotCounter -= Time.deltaTime;
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

    void Turret_AI ()
    {
        if (timeBtwShotCounter <= 0)
        {
            Debug.Log("Shoot bullet");
            Vector3 direction = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
            spawnPoint = transform.position + direction.normalized;
            GameObject bullet = Instantiate(projectile, spawnPoint, Quaternion.identity);
            bullet.GetComponent<EnemyRanged_Bullet>().damage = baseAttack;
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed * direction.normalized;
            //Face_Direction(bullet.GetComponent<Rigidbody2D>().velocity);

            timeBtwShotCounter = timeBtwShot;
        }

        else
        {
            timeBtwShotCounter -= Time.deltaTime;
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

}
