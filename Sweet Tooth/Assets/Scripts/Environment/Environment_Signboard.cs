using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_Signboard : MonoBehaviour
{
    public GameObject UI;

    public GameObject indicator;

    public GameObject enemies;

    private bool isInRange;
    private bool isInMap;

    // Start is called before the first frame update
    void Start()
    {
        indicator.SetActive(false);
        UI.SetActive(false);
        //Camera.main.cullingMask = 1 << LayerMask.NameToLayer("Enemies");
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            Debug.Log("In range");

            if (FindObjectOfType<PlayerInput>().meleeButton)
            {
                StartCoroutine(See_SignBoard());
            }

            else if (FindObjectOfType<PlayerInput>().attackButton)
            {
                StartCoroutine(Exit_SignBoard());
            }
        }
    }

    IEnumerator See_SignBoard ()
    {
        if (!isInMap)
        {
            //Camera.main.cullingMask = 1 << 0;
            if (enemies != null)
            {
                enemies.SetActive(false);
            }

            UI.SetActive(true);
            Debug.Log("Open");
            FindObjectOfType<PlayerController>().rb2d.velocity = Vector2.zero;
            FindObjectOfType<PlayerController>().isMoving = false;
            FindObjectOfType<PlayerInput>().horizontalInput = 0f;
            FindObjectOfType<PlayerInput>().verticalInput = 0f;
            FindObjectOfType<PlayerController>().enabled = false;
            FindObjectOfType<Player_Inventory>().enabled = false;
            FindObjectOfType<PlayerController>().GetComponent<Animator>().SetBool("isMoving", false);
            isInMap = true;
            yield return null;
        }
    }

    IEnumerator Exit_SignBoard()
    {
        if (isInMap)
        {
            //Camera.main.cullingMask = 10 << 1;
            if (enemies != null)
            {
                enemies.SetActive(true);
            }

            UI.SetActive(false);
            FindObjectOfType<PlayerController>().enabled = true;
            FindObjectOfType<PlayerController>().GetComponent<Animator>().SetBool("isMoving", false);
            FindObjectOfType<Player_Inventory>().enabled = true;
            isInMap = false;
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("In trigger");
            isInRange = true;
            indicator.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Out of trigger");
            isInRange = false;
            indicator.SetActive(false);
        }
    }
}
