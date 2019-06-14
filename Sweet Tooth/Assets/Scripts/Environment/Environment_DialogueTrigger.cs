using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_DialogueTrigger : MonoBehaviour
{

    //Trigger ID
    public GameObject allDialogues;

    public GameObject Pom;


    //Boolean for dialogue
    private bool activateTrigger;

    private int dialogueSelector;

    //Gameobjects
    private GameObject player;

    //Scripts
    private PlayerInput pi;
    private Manager_Dialogue md;

    //bools
    public bool pomTrigger;

    // Start is called before the first frame update
    void Start()
    {
        SaveFeature();
    }

    // Update is called once per frame
    void Update()
    {
        Find_Objects();

        if (activateTrigger)
        {
            if (pi.interactButton)
            {
                Debug.Log("Button pressed");

                if (FindObjectOfType<PlayerController>().isMoving == false)
                {
                    if (!md.isTalking)
                    {
                        FindObjectOfType<PlayerController>().isMelee = false;
                        Debug.Log("Start dialogue");
                        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        FindObjectOfType<PlayerController>().enabled = false;
                        //gameObject.GetComponent<Dialogue_Trigger>().TriggerDialogue();
                        //Debug.Log("Talk");
                    }

                    else
                    {

                        if (md.isSpeaking)
                        {
                            md.SkipDialogue();
                            FindObjectOfType<PlayerController>().enabled = false;
                        }

                        else
                        {
                            md.DisplayNextSentence();
                            FindObjectOfType<PlayerController>().enabled = false;
                        }

                        if (md.anim.GetBool("isOpen") == false)
                        {
                            gameObject.GetComponent<Save_ObjState>().obj.saveState = 1;
                            gameObject.GetComponent<Save_ObjState>().obj.ForceSerialization();
                            FindObjectOfType<PlayerController>().enabled = true;
                            gameObject.SetActive(false);
                        }
                    }
                }

            }

        }
    }

    void Find_Objects()
    {
        if (pi == null)
        {
            pi = FindObjectOfType<PlayerInput>();
        }

        if (md == null)
        {
            md = FindObjectOfType<Manager_Dialogue>();
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }


    private void SaveFeature()
    {
        if (gameObject.GetComponent<Save_ObjState>() != null)
        {
            if (gameObject.GetComponent<Save_ObjState>().obj.saveState == 1)
            {
                gameObject.SetActive(false);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            activateTrigger = true;

            if (!md.isTalking)
            {
                FindObjectOfType<PlayerController>().isMelee = false;
                Debug.Log("Start dialogue");
                GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                Say_Dialogue();

                if (pomTrigger)
                {
                    gameObject.GetComponent<NPC>().enabled = false;
                }
                //Debug.Log("Talk");
            }
        }
    }

    void Say_Dialogue()
    {
        //Underground Part A Cave entrance
        player.GetComponent<Animator>().SetBool("isMoving", false);
        dialogueSelector = 0;
        allDialogues.transform.GetChild(dialogueSelector).GetComponent<Dialogue_Trigger>().TriggerDialogue();
    }
}
