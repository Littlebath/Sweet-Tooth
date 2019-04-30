using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons_Fireball : MonoBehaviour
{
    [SerializeField] private Player_ScriptableObject designerValues;
    [SerializeField] private GameObject inferno;
    [SerializeField] private GameObject effect;

    //Floats
    private float currentSpeed;

    //References
    private Rigidbody2D rb2d;

    //Scripts
    private PlayerController pc;
    private Player_Knockback pk;

    // Start is called before the first frame update
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        pk = FindObjectOfType<Player_Knockback>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();

        Direction_Of_Fireball();

        StartCoroutine(Spawn_Inferno());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Direction_Of_Fireball()
    {
        FireBall_Speed();

        if (pc.lastMove.y != 0)
        {
            if (pc.lastMove.y > 0)
            {
                Debug.Log("Face up");
                rb2d.velocity = Vector2.up * currentSpeed;
                transform.rotation = new Quaternion(0f, 0f, 45f, 0f);
            }

            else if (pc.lastMove.y < 0)
            {
                Debug.Log("Face down");
                rb2d.velocity = Vector2.down * currentSpeed;
                transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            }
        }

        else
        {
            if (pc.lastMove.x > 0)
            {
                Debug.Log("Face right");
                rb2d.velocity = Vector2.right * currentSpeed;
                transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            }

            else if (pc.lastMove.x < 0)
            {
                Debug.Log("Face left");
                rb2d.velocity = Vector2.left * currentSpeed;
                transform.rotation = new Quaternion(0f, 0f, 180f, 0f);
            }
        }

    }

    private void FireBall_Speed()
    {
        currentSpeed = designerValues.fireballSpeed;
    }

    private IEnumerator Spawn_Inferno ()
    {
        yield return new WaitForSeconds(designerValues.destroyTime);
        Instantiate(inferno, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Shield"))
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<ShieldEnemy_Shield>().Damage_Shield(designerValues.fireballDamage);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
           collision.gameObject.GetComponent<Enemy>().Take_Damage(designerValues.fireballDamage);
           GameObject blaze = Instantiate(inferno, transform.position, Quaternion.identity);
           Debug.Log("Hit enemy");
            //pk.Knock_Back(collision);
        }

        if (collision.gameObject.CompareTag("Gum"))
        {
            collision.gameObject.GetComponent<Environment_Gum>().Spawn_Sticky();
        }

        Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
