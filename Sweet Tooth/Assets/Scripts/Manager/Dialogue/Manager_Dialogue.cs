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

    public float typingSpeed;

    public Text nameText;
    public TextMeshProUGUI dialogueText;

    public Image portrait;

    public Animator anim;

    [HideInInspector] public bool isTalking;
    [HideInInspector] public bool isSpeaking;

    private List<DialogueScriptableObject> dialogues;

    private Queue<string> sentences;

    private DialogueScriptableObject currentDialogue;

    private int dialogueCounter = 0;

    private string sentence;

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

    private void Update()
    {
        if (dialogueText.text == sentence)
        {
            Debug.Log("Dialogue done");
            isSpeaking = false;
            Debug.Log(isSpeaking);
            SkipDialogue();
        }
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
        //pc.isMoving = false;
        //pc.enabled = false;


        //Debug.Log (currentDialogue.name);

        sentences.Clear();


        foreach (string sentence in currentDialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds (typingSpeed);
        }
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
                dialogueCounter++;
                //Debug.Log("Spoken dialogue" + dialogueCounter);
                StartDialogue(dialogues);
            }

            else
            {
                EndDialogue();
                return;
            }
        }

        else
        {
            if (!isSpeaking)
            {
                isSpeaking = true;
                sentence = sentences.Dequeue();

                pc.isMoving = false;
                nameText.text = currentDialogue.name;
                portrait.sprite = currentDialogue.portrait;

                StopAllCoroutines();
                StartCoroutine(TypeSentence(sentence));
            }

            else
            {
                if (dialogueText.text == sentence)
                {
                    Debug.Log("Dialogue done");
                    isSpeaking = false;
                    DisplayNextSentence();
                }
            }
        }
    }

    public void SkipDialogue ()
    {
        StopAllCoroutines();
        dialogueText.text = sentence;
        isSpeaking = false;
    }

    public void EndDialogue ()
    {
        //Debug.Log("End of conversation");
        pc.enabled = true;
        isTalking = false;
        anim.SetBool("isOpen", false);
        dialogueCounter = 0;
        Time.timeScale = 1f;
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
