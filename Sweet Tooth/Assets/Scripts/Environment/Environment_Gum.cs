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
            }
        }
    }

    IEnumerator Disable_Input ()
    {
        FindObjectOfType<PlayerController>().enabled = false;
        yield return new WaitForSeconds (0.1f);
        FindObjectOfType<PlayerController>().enabled = true;
    }
}
