using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Trigger : MonoBehaviour
{
    //public Dialogue dialogue;
    public List<DialogueScriptableObject> dialogue;

    private PlayerInput pi;
    private Manager_Dialogue md;
    private GameObject player;

    private bool isSpeaking;
    [SerializeField] private bool cutsceneDialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<Manager_Dialogue>().StartDialogue(dialogue);
    }

    private void OnEnable()
    {
        if (cutsceneDialogue)
        {
            TriggerDialogue();
            isSpeaking = true;
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


    private void Update()
    {
        Find_Objects();

        if (isSpeaking)
        {
            if (FindObjectOfType<PlayerInput>().interactButton)
            {
                if (FindObjectOfType<PlayerController>().isMoving == false)
                {
                    if (!md.isTalking)
                    {
                        FindObjectOfType<PlayerController>().enabled = false;
                        FindObjectOfType<PlayerController>().isMelee = false;
                        Debug.Log("Start dialogue");
                        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        gameObject.GetComponent<Dialogue_Trigger>().TriggerDialogue();
                        //Debug.Log("Talk");
                    }

                    else
                    {
                        if (md.isSpeaking)
                        {
                            md.SkipDialogue();
                            //FindObjectOfType<PlayerController>().enabled = false;
                        }

                        else
                        {
                            md.DisplayNextSentence();

                            if (md.anim.GetBool("isOpen") == false)
                            {
                                FindObjectOfType<Manager_Timeline>().ReverState();
                                FindObjectOfType<Manager_Timeline>().FixAnim();
                                FindObjectOfType<PlayerController>().enabled = true;
                                gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }
}


