using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_Controller : MonoBehaviour
{
    /*[HideInInspector]*/ public float selector;

    private Animator anim;

    //Scripts
    private PlayerInput pi;
    private MainMenu mm;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        mm = FindObjectOfType<MainMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        Switch_Selector_Positions();
        Menu_Controller();
    }

    void Switch_Selector_Positions()
    {
        anim.SetFloat("Selector", selector);

    }

    void Menu_Controller ()
    {
        Menu_Limiter();

        Menu_Selection();
    }

    private void Menu_Selection()
    {

        if (Input.GetButtonDown("Interact"))
        {
            switch (selector.ToString())
            {
                case "0":
                    mm.StartTheGame();
                    break;

                case "-2":
                    mm.ExitGame();
                    break;
            }
        }
    }

    void Menu_Limiter ()
    {
        if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0)
        {
            if (selector < 0)
            {
                //Debug.Log("Move Up");
                selector += 2;
            }
        }

        else if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0)
        {
            if (selector > -2)
            {
                //Debug.Log("Move Down");
                selector -= 2;
            }
        }

    }
}
