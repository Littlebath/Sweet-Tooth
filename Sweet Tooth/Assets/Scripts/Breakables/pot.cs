using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pot : MonoBehaviour
{

    private Animator anim;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();

        if (gameObject.GetComponent<Save_ObjState>() != null)
        {
            if (gameObject.GetComponent<Save_ObjState>().obj.saveState == 1)
            {
                Destroy(gameObject);
            }

        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Smash()
    {
        anim.SetBool("smash", true);
        StartCoroutine(breakCo());
    }

    IEnumerator breakCo()
    {
        if (gameObject.GetComponent<Save_ObjState>() != null)
        {
            gameObject.GetComponent<Save_ObjState>().obj.saveState = 1;
        }

        yield return new WaitForSeconds(.3f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boomerang"))
        {
            Smash();
        }
    }
}
