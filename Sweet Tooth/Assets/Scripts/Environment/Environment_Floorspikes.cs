using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_Floorspikes : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float spikeAppearTime;
    [SerializeField] private AnimationClip strikeDisappearTime;

    [Header("Knock back stats")]
    public float thrust;
    public float knockTime;

    private bool isSpiking;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    private IEnumerator KnockCo(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            PlayerController.isPlayerHurt = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isSpiking)
            {
                StartCoroutine(Spike_Appear());
            }
        }
    }

    private IEnumerator Spike_Appear ()
    {
        isSpiking = true;
        yield return new WaitForSeconds(spikeAppearTime);
        gameObject.GetComponent<Animator>().SetTrigger("rise");
    }

    public IEnumerator Spikes_Disappeare ()
    {
        isSpiking = false;
        gameObject.GetComponent<Animator>().SetTrigger("fall");
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag ("Player"))
        {
            FindObjectOfType<PlayerController>().Hurt_Player(damage);
            Knock_Back_Player(collision);
        }
    }
}
