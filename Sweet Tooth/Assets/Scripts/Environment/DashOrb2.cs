using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashOrb2 : MonoBehaviour
{
    [SerializeField] private Player_ScriptableObject psc;
    [SerializeField] private float respawnTime;

    private PlayerInput pi;
    private PlayerController pc;

    private Rigidbody2D playerRb2d;

    // Start is called before the first frame update
    void Start()
    {
        pi = FindObjectOfType<PlayerInput>();
        pc = FindObjectOfType<PlayerController>();

        playerRb2d = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Shoot_Player ()
    {
        pc.gameObject.transform.position = gameObject.transform.position;
        Vector2 origin = gameObject.transform.position;
        psc.energyCounter++;
        Vector2 input = new Vector2 (pi.horizontalInput, pi.verticalInput);
        pi.horizontalInput = 0f;
        pi.verticalInput = 0f;
        pi.enabled = false;
        pi.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        //Debug.Log("First time " + playerRb2d.velocity);
        yield return new WaitForSeconds(1f);
        pc.enabled = false;
        playerRb2d.velocity = input * psc.dashSpeed;
        gameObject.GetComponent<Rigidbody2D>().velocity = input * psc.dashSpeed;
        yield return new WaitForSeconds(psc.dashTime);
        //Debug.Log("Second time " + playerRb2d.velocity);
        pi.enabled = true;
        pi.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        pc.enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(respawnTime);
        GameObject clone = Resources.Load("Prefabs/Environment/Dash Orb - Single") as GameObject;
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
        }
    }

}
