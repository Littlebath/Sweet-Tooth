﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_OreoChocolateBoss : MonoBehaviour
{
    [SerializeField] private OreoBossScriptableObject values;
    public float moreTurn;

    private bool isHurt;

    private Vector3 origin;
    private Vector3 deathOrigin;

    private bool isInSecondPhase;

    public Animator roomShake;

    public GameObject indicator;

    [Header("Boulder properties")]
    public Transform minX;
    public Transform maxX;
    public Transform spawnHeight;

    private Animator anim;
    //State Machine Behavior Properties


    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        values.Reset_Boss_Parameters();
        indicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Charging_Indicator();
    }

    void Charging_Indicator ()
    {
        //indicator.transform.rotation = Quaternion.Euler (0f, 0f, moreTurn);

        Vector3 direction = indicator.transform.position - FindObjectOfType<PlayerController>().transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion lookRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        indicator.transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 10f);
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public void Boulders_Fall ()
    {
        StartCoroutine(True_Boulders());
    }

    private IEnumerator True_Boulders ()
    {
        for (int i = 0; i < values.boulders.Length; i++)
        {
            Instantiate(values.boulders[i], spawnHeight.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(values.boulderFallInterval);
        }
    }

    public void Spread_Shot ()
    {
        GameObject bulletUp = Instantiate(values.bullet, transform.position, Quaternion.identity);
        bulletUp.GetComponent<Rigidbody2D>().velocity = Vector2.up * values.bulletSpeed;

        GameObject bulletLeft = Instantiate(values.bullet, transform.position, Quaternion.identity);
        bulletLeft.GetComponent<Rigidbody2D>().velocity = Vector2.left * values.bulletSpeed;

        GameObject bulletDown = Instantiate(values.bullet, transform.position, Quaternion.identity);
        bulletDown.GetComponent<Rigidbody2D>().velocity = Vector2.down * values.bulletSpeed;

        GameObject bulletRight = Instantiate(values.bullet, transform.position, Quaternion.identity);
        bulletRight.GetComponent<Rigidbody2D>().velocity = Vector2.right * values.bulletSpeed;
    }

    public void Camera_Shake ()
    {
        if (roomShake != null)
        {
            roomShake.SetTrigger("shake");
        }
    }

    public void Boss_Theme()
    {
        FindObjectOfType<Manager_AudioManager>().Play("Chocolate Boss");
    }

    public void Boss_Intro_Particles ()
    {
        Instantiate(values.introParticles, transform.position, Quaternion.identity);
    }

    public void Take_Damage(float damage)
    {
        if (!isHurt)
        {
            isHurt = true;

            values.currHealth -= damage;
            StartCoroutine(Flash());

            if (values.currHealth <= values.phaseTwoHealth)
            {
                if (!isInSecondPhase)
                {
                    isInSecondPhase = true;
                    Debug.Log("Run Phase 2");
                    gameObject.GetComponent<Animator>().SetTrigger("transform");
                    values.noOfDashes = 0;
                    //Anim trigger
                }
            }

            if (values.currHealth <= 0)
            {
                Boss_Dies();
            }
        }
    }

    public void Randomize_Movement ()
    {
        float x = deathOrigin.x + Random.Range(0f, 1f);
        float y = deathOrigin.y + Random.Range(0f, 1f);
        transform.position = new Vector2(x, y);
    }


    private IEnumerator Flash()
    {
        for (int i = 0; i < 5; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }

        isHurt = false;
    }

    private void Boss_Dies()
    {
        StopCoroutine(Flash());
        deathOrigin = transform.position;
        gameObject.GetComponent<Animator>().SetTrigger("death");
        Destroy(gameObject, 3f);
    }

    public void Knock_Back_Me(GameObject me)
    {
        Rigidbody2D enemy = me.GetComponent<Rigidbody2D>();

        if (enemy != null)
        {
            enemy.isKinematic = false;
            Vector2 difference = transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;
            difference = difference.normalized * values.thrust;
            enemy.AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine(KnockMe(enemy));
        }
    }

    private IEnumerator KnockMe(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(values.knockTime + 0.2f);
            enemy.velocity = Vector2.zero;
            enemy.isKinematic = true;
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
                difference = difference.normalized * values.thrust;
                enemy.velocity = difference;
                PlayerController.isPlayerHurt = true;
                StartCoroutine(KnockCo(enemy));
            }
        }
    }

      private IEnumerator KnockCo(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(values.knockTime);
            enemy.velocity = Vector2.zero;
            PlayerController.isPlayerHurt = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<PlayerController>().Hurt_Player(values.damage);
            Knock_Back_Player(collision);
            Knock_Back_Me(gameObject);
        }
    }

}
