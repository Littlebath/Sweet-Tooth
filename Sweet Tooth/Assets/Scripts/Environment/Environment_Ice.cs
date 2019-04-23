using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_Ice : MonoBehaviour
{
    [SerializeField] private bool isOnIce;
    [SerializeField] private bool isSliding;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnIce)
        {
            PlayerInput pi = FindObjectOfType<PlayerInput>();

            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity == Vector2.zero)
            {
                isSliding = false;
            }

            else
            {
                isSliding = true;
            }
        }

        else
        {
            isSliding = false;
        }

        if (isSliding)
        {
            FindObjectOfType<PlayerController>().enabled = false;
        }

        else
        {
            FindObjectOfType<PlayerController>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isOnIce = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isOnIce = false;
        }
    }
}
