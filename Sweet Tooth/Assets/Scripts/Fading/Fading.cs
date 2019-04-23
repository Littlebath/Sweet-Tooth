using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour
{
    private Animator anim;

	// Use this for initialization
	void Start ()
    {
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void FadeOut ()
    {
        anim.SetTrigger("end");
    }

    public void FadeIn ()
    {
        anim.SetTrigger("start");
    }

    public void ResetTriggers ()
    {
        anim.ResetTrigger("end");
    }
}
