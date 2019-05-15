using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestNPC : MonoBehaviour
{
    //Scriptable Objects
    [SerializeField] private NPCFaceScriptableObject direction;
    [SerializeField] private Quest1ScriptableObject questValues;

    [SerializeField] private Vector2 size;
    [SerializeField] private float distance;

    [SerializeField] private GameObject allDialogues;

    //Gameobjects
    private GameObject player;

    //Bools
    [HideInInspector] public bool isInRange;
    [HideInInspector] public static bool canMelee;

    //Scripts
    private PlayerInput pi;
    private Manager_Dialogue md;

    private int dialogueSelector;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Find_Objects();

        if (isInRange)
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
                        Say_Dialogue();
                        FacePlayer();
                        //Debug.Log("Talk");
                    }

                    else
                    {
                        md.DisplayNextSentence();
                    }
                }

            }

        }
    }

    void Say_Dialogue()
    {
        if (questValues.pathA[0] == false)
        {
            dialogueSelector = 0;
            questValues.pathA[0] = true;
            questValues.ForceSerialization();
        }

        else if (questValues.pathA[0] == true || questValues.pathA[1] == true || questValues.pathA[2] == true)
        {
            dialogueSelector = 1;
        }

        allDialogues.transform.GetChild(dialogueSelector).GetComponent<Dialogue_Trigger>().TriggerDialogue();
    }

    private void FacePlayer()
    {
        /*RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 1.5f);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 1.5f);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 1.5f);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, 1.5f);*/


        Collider2D hitLeft = Physics2D.OverlapBox(transform.position + Vector3.left * distance, size, 0f);
        Collider2D hitDown = Physics2D.OverlapBox(transform.position + Vector3.down * distance, size, 0f);
        Collider2D hitRight = Physics2D.OverlapBox(transform.position + Vector3.right * distance, size, 0f);
        Collider2D hitUp = Physics2D.OverlapBox(transform.position + Vector3.up * distance, size, 0f);

        //Debug.DrawRay(transform.position, Vector2.left, Color.red);

        if (hitLeft == true)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = direction.facingLeft;
            Debug.Log("Turn left");
        }

        else if (hitDown == true)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = direction.facingDown;
            Debug.Log("Turn down");
        }

        else if (hitRight == true)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = direction.facingRight;
            Debug.Log("Turn right");
        }

        else if (hitUp == true)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = direction.facingUp;
            Debug.Log("Turn up");
        }

    }

    /* private void OnCollisionEnter2D(Collision2D collision)
     {
         if (collision.gameObject.CompareTag("Player"))
         {
             Debug.Log("Player is in");
             isInRange = true;
             canMelee = true;
         }
     }

     private void OnCollisionExit2D(Collision2D collision)
     {
         if (collision.gameObject.CompareTag("Player"))
         {
             //Debug.Log("Player is out");
             isInRange = false;
             canMelee = false;
         }
     }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is in");
            isInRange = true;
            canMelee = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player is out");
            isInRange = false;
            canMelee = false;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(transform.position + new Vector3(-0.5f, 0f, 0f), 0.5f);
        Gizmos.DrawWireCube(transform.position + Vector3.left * distance, size);
        Gizmos.DrawWireCube(transform.position + Vector3.up * distance, size);
        Gizmos.DrawWireCube(transform.position + Vector3.right * distance, size);
        Gizmos.DrawWireCube(transform.position + Vector3.down * distance, size);
    }
}
