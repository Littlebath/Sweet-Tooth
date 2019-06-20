﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pot : MonoBehaviour
{
    public GameObject effect;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }

    public void Smash()
    {
        anim.SetBool("smash", true);
        StartCoroutine(breakCo());
        Instantiate(effect, transform.position, Quaternion.identity);
    }

    IEnumerator breakCo()
    {
        if (gameObject.GetComponent<Save_ObjState>() != null)
        {
            gameObject.GetComponent<Save_ObjState>().obj.saveState = 1;
            gameObject.GetComponent<Save_ObjState>().obj.ForceSerialization();
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
