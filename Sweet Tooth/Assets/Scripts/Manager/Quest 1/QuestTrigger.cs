using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{ 
    //Trigger ID
    public int ID;
    public GameObject allDialogues;
    public Quest1ScriptableObject questValues;

    //Boolean for dialogue
    private bool activateTrigger;

    private int dialogueSelector;

    //Gameobjects
    private GameObject player;

    //Scripts
    private PlayerInput pi;
    private Manager_Dialogue md;

    public LoadNewArea  exitPointFalcon;
    public LoadNewArea exitPointBlack;

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
                        //gameObject.GetComponent<Dialogue_Trigger>().TriggerDialogue();
                        //Debug.Log("Talk");
                    }

                    else
                    {

                        if (md.isSpeaking)
                        {
                            md.SkipDialogue();
                        }

                        else
                        {
                            md.DisplayNextSentence();
                        }

                        if (md.anim.GetBool("isOpen") == false)
                        {
                            gameObject.GetComponent<Save_ObjState>().obj.saveState = 1;
                            gameObject.GetComponent<Save_ObjState>().obj.ForceSerialization();
                            gameObject.SetActive(false);
                        }
                    }
                }

            }

        }
    }


    private void SaveFeature()
    {
        if (gameObject.GetComponent<Save_ObjState>() != null)
        {
            if (gameObject.GetComponent<Save_ObjState>().obj.saveState == 1)
            {
                if (ID == 0)
                {
                    exitPointFalcon.gameObject.SetActive(false);
                }

                else if (ID == 1)
                {
                    exitPointBlack.gameObject.SetActive(false);
                }
                FindObjectOfType<Quest_1_Manager>().caveEntrance.SetActive(false);
                FindObjectOfType<Quest_1_Manager>().trapDoorEntrance.SetActive(false);
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
                //Debug.Log("Talk");
            }
        }
    }

    void Say_Dialogue()
    {
        //Underground Part A Cave entrance
        player.GetComponent<Animator>().SetBool("isMoving", false);

        if (ID == 0)
        {
            if (questValues.pathC[0] == false)
            {
                dialogueSelector = 0;
            }

            else
            {
                dialogueSelector = 1;
            }
            //Block Cave entrance
            exitPointFalcon.gameObject.SetActive(false);
            FindObjectOfType<Quest_1_Manager>().trapDoorEntrance.SetActive(false);

        }

        //Underground Part A Trapdoor entrance
        else if (ID == 1)
        {
            if (questValues.pathC[0] == false)
            {
                dialogueSelector = 0;
            }

            else
            {
                dialogueSelector = 1;
            }
            //Block Trap door entrance
            exitPointBlack.gameObject.SetActive(false);
            FindObjectOfType<Quest_1_Manager>().caveEntrance.SetActive(false);
        }

        //Saving jack trigger
        else if (ID == 2)
        {
            dialogueSelector = 0;
            questValues.pathA[3] = true;
            questValues.pathB[2] = true;
            questValues.pathC[1] = true;
        }

        //Finding Percy not defeated mini boss
        else if (ID == 3)
        {
            dialogueSelector = 0;
            //questValues.objectives2[2] = true;
        }

        else if (ID == 4)
        {
            //Spoke to kids
            if (questValues.objectives3A[0] == true)
            {
                dialogueSelector = 0;
                questValues.objectives3A[2] = true;
            }

            //Didn't speak to the kids
            else
            {
                dialogueSelector = 1;
                questValues.objectives3A[2] = true;
            }

            FindObjectOfType<Player_Inventory>().currency += questValues.quest3Reward;
            //Play cutscene of father lagos leaving
        }

        allDialogues.transform.GetChild(dialogueSelector).GetComponent<Dialogue_Trigger>().TriggerDialogue();
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
}
