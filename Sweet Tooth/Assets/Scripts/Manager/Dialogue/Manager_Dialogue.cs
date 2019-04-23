using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Manager_Dialogue : MonoBehaviour
{
    public static Manager_Dialogue instance;

    public Text nameText;
    public TextMeshProUGUI dialogueText;

    public Image portrait;

    public Animator anim;

    [HideInInspector] public bool isTalking;

    private List<DialogueScriptableObject> dialogues;

    private Queue<string> sentences;

    private DialogueScriptableObject currentDialogue;

    private int dialogueCounter = 0;

    //Bools
    private static bool isDialogueManagerExisting;

    //Scripts
    private PlayerInput pi;
    private PlayerController pc;

	// Use this for initialization
	void Start ()
    {
        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            Destroy_Duplicates();
        }

        else
        {
            Destroy(gameObject);
        }

        sentences = new Queue<string>();
        pc = FindObjectOfType<PlayerController>();
        pi = FindObjectOfType<PlayerInput>();
	}

    private void Destroy_Duplicates()
    {
        if (!isDialogueManagerExisting)
        {
            isDialogueManagerExisting = true;
            DontDestroyOnLoad(transform.gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

    }

    public void StartDialogue (List<DialogueScriptableObject> dialogue)
    {
        isTalking = true;

        anim.SetBool("isOpen", true);

        dialogues = dialogue;

        currentDialogue = dialogue[dialogueCounter];

        nameText.text = currentDialogue.name;

        portrait.sprite = currentDialogue.portrait;

        pi.horizontalInput = 0f;
        pi.verticalInput = 0f;
        pc.isMoving = false;
        pc.enabled = false;


        //Debug.Log (currentDialogue.name);

        sentences.Clear();


        foreach (string sentence in currentDialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {/*
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }*/

        pc.isMoving = false;

        if (sentences.Count == 0)
        {
            if (currentDialogue.followDialogue)
            {
                StartDialogue(dialogues);
                dialogueCounter++;
            }

            else
            {
                EndDialogue();
                return;
            }

        }

        else
        {
            string sentence = sentences.Dequeue();
            pc.isMoving = false;
            nameText.text = currentDialogue.name;
            dialogueText.text = sentence;
            portrait.sprite = currentDialogue.portrait;
        }

    }

    void EndDialogue ()
    {
        //Debug.Log("End of conversation");
        pc.enabled = true;
        isTalking = false;
        anim.SetBool("isOpen", false);
        dialogueCounter = 0;
    }

    void DontDestroy()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
            return;
        }
    }

}
