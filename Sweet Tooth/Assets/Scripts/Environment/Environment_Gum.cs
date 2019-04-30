using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_Gum : MonoBehaviour
{
    [SerializeField] private GameObject meltedGum;

    private PlayerController pc;

    private bool isStuck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pc == null)
        {
            pc = FindObjectOfType<PlayerController>();
        }

        if (pc.isSpinning)
        {
            pc = FindObjectOfType<PlayerController>();
            pc.currentMoveSpeed = 0f;
            Debug.Log("Stick");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (pc != null)
            {
                if (pc.isDashing)
                {
                    Debug.Log("Dash Past");
                }

                else
                {
                    Debug.Log("Get stuck");
                    pc.isSpinning = true;
                    StartCoroutine(Stick_Player());
                }

            }
        }

        else if (collision.gameObject.CompareTag("Enemy"))
        {
            //Sticky enemy
            StartCoroutine(Stick_Enemy(collision));
        }
    }

    public void Spawn_Sticky ()
    {
        Instantiate(meltedGum, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator Stick_Player ()
    {
        yield return new WaitForSeconds (3f);
        pc.isSpinning = false;
    }

    private IEnumerator Stick_Enemy (Collider2D other)
    {
        float currentSpeed = other.gameObject.GetComponent<Enemy>().moveSpeed;
        other.gameObject.GetComponent<Enemy>().moveSpeed = 0f;
        yield return new WaitForSeconds(3f);
        other.gameObject.GetComponent<Enemy>().moveSpeed = currentSpeed;
    }
}
