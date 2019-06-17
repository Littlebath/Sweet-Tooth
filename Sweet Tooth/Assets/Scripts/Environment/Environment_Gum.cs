using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_Gum : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (FindObjectOfType<PlayerController>().isDashing)
            {
                Debug.Log("Break jelly");
                Destroy(gameObject);
            }

            else
            {
                StartCoroutine(Disable_Input());
                StartCoroutine(Pulse());
            }
        }
    }

    private IEnumerator Pulse()
    {
        Vector3 originalSize = gameObject.transform.localScale;
        Vector3 playerPos = FindObjectOfType<PlayerController>().transform.position;
        Vector3 originPos = gameObject.transform.position;
        Vector3 direction = playerPos - originPos;
        Vector3 expansion = direction.normalized * 0.2f;

        Debug.Log(direction);

        gameObject.GetComponent<Transform>().localScale = originalSize + expansion;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<Transform>().localScale = originalSize;
    }

    IEnumerator Disable_Input ()
    {
        FindObjectOfType<PlayerController>().enabled = false;
        yield return new WaitForSeconds (0.1f);
        FindObjectOfType<PlayerController>().enabled = true;
    }
}
