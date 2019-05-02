using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_ChocolateBoss : MonoBehaviour
{
    [SerializeField] private ChocolateBossScriptableObject values;

    private bool isInvinsible;
    private bool isHurt;

    private Vector3 origin;

    // Start is called before the first frame update
    void Start()
    {
        values.Reset_Boss_Parameters();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Reset_Position ()
    {
        transform.position = origin;
    }

    public void Take_Damage (float damage)
    {
        if (!isHurt)
        {
            isHurt = true;

            if (!isInvinsible)
            {
                values.currHealth -= damage;
                StartCoroutine(Flash());

                if (values.currHealth <= values.phaseTwoHealth)
                {
                    Phase_Two_Init();
                }

                if (values.currHealth <= 0)
                {
                    Boss_Dies();
                }
            }
        }

    }

    private void Death_Particles ()
    {

    }

    private void Create_Intro_Particles ()
    {
        Instantiate(values.introPartices, transform.position, Quaternion.identity);
    }

    public IEnumerator Attack_Ground_One ()
    {
        Vector3 playerPos = FindObjectOfType<PlayerController>().transform.position;
        Instantiate(values.spike, playerPos, Quaternion.identity);
        yield return new WaitForSeconds(values.spikeAttack.averageDuration);
        gameObject.GetComponent<Animator>().SetTrigger("punch1");
    }

    public IEnumerator Attack_Ground_Two()
    {
        Vector3 playerPos = FindObjectOfType<PlayerController>().transform.position;
        Instantiate(values.spike, playerPos, Quaternion.identity);
        yield return new WaitForSeconds(values.spikeAttack.averageDuration);
        gameObject.GetComponent<Animator>().SetTrigger("punch2");
    }

    public void Boss_Theme ()
    {
        FindObjectOfType<Manager_AudioManager>().Play("Chocolate Boss");
    }

    private IEnumerator Flash ()
    {
        Debug.Log("Hurt");

        for (int i = 0; i < 5; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds (0.1f);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }

        isHurt = false;
    }

    private void Phase_Two_Init()
    {
        Debug.Log("Activate phase 2 behaviors");
        gameObject.GetComponent<Animator>().SetTrigger("phase2");
    }

    private void Boss_Dies()
    {
        Debug.Log("Boss is dead");
        StopCoroutine(Flash());
        gameObject.GetComponent<Animator>().SetTrigger("isDead");
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<PlayerController>().Hurt_Player(values.damage);
        }
    }
}
