using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashOrb1 : MonoBehaviour
{
    [SerializeField] private Player_ScriptableObject psc;
    [SerializeField] private float respawnTime;

    private bool canDash;

    private Vector2 origin;

    private PlayerInput pi;
    private PlayerController pc;

    private Rigidbody2D playerRb2d;
    private Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        pi = FindObjectOfType<PlayerInput>();
        pc = FindObjectOfType<PlayerController>();

        origin = transform.position;

        playerRb2d = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerAnim = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canDash)
        {
            if (pi.dashButton)
            {
                pc.states = playerStates.Charging;
                StartCoroutine(AutoDash());
            }
        }
    }

    IEnumerator Shoot_Player()
    {
        pc.gameObject.transform.position = gameObject.transform.position;

        psc.energyCounter++;
        pi.verticalInput = 0;
        pi.horizontalInput = 0;
        playerRb2d.velocity = Vector2.zero;
        playerAnim.SetBool("isMoving", false);
        pc.enabled = false;
        yield return new WaitForSeconds(1f);
        canDash = true;
        yield return new WaitForSeconds(4f);
        canDash = false;
        StartCoroutine (DestroyingBubble());
        yield return null; 
    }

    IEnumerator AutoDash ()
    {
        canDash = false;
        Vector2 input = new Vector2(pi.horizontalInput, pi.verticalInput);
        playerRb2d.velocity = input * psc.dashSpeed;
        gameObject.GetComponent<Rigidbody2D>().velocity = input * psc.dashSpeed;
        yield return new WaitForSeconds(psc.dashTime);
        StartCoroutine(DestroyingBubble());
    }

    IEnumerator DestroyingBubble ()
    {
        pc.enabled = true;
        pi.enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(respawnTime);
        GameObject clone = Resources.Load("Prefabs/Environment/Dash Orb - Multiple") as GameObject;
        Instantiate(clone, origin, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (pc.isDashing)
            {
                //Debug.Log("Player in Orb");
                StartCoroutine(Shoot_Player());
            }

            else
            {
                Destroy(gameObject);
            }
        }
    }
}
