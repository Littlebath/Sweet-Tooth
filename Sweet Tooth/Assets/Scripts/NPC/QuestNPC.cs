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

    //NPC Quest ID
    public int ID;

    //Gameobjects
    private GameObject player;

    //Bools
    [HideInInspector] public bool isInRange;
    [HideInInspector] public static bool canMelee;

    //Scripts
    private PlayerInput pi;
    private Manager_Dialogue md;

    private int dialogueSelector;

    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(gameObject.name + " has " + allDialogues.transform.childCount + " dialogues to be said");
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

                        if (md.isSpeaking)
                        {
                            md.SkipDialogue();
                        }

                        else
                        {
                            md.DisplayNextSentence();
                        }

                        if (ID == 0)
                        {
                            if (md.anim.GetBool("isOpen") == false)
                            {
                                gameObject.GetComponent<ShopController>().OpenShop();
                            }
                        }

                        else if (ID == 8)
                        {
                            if (md.anim.GetBool("isOpen") == false)
                            {
                                gameObject.GetComponent<ShopController>().OpenShop();
                            }
                        }
                    }
                }

            }

        }
    }

    void Say_Dialogue()
    {
        FindObjectOfType<PlayerController>().enabled = false;

        //Quest Shop Keeper
        if (ID == 0)
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
        }

        //Jill dialogue
        else if (ID == 1)
        {
            //Debug.Log("Running quest");

            if (!questValues.questCompleted)
            {
                //Initation of finding jack
                if (questValues.pathA[0] == true || questValues.pathB[0] == false)
                {
                    Debug.Log("This condition is true");

                    if (questValues.pathA[1] == false)
                    {
                        dialogueSelector = 0;
                        questValues.pathA[0] = true;
                        questValues.pathA[1] = true;
                        questValues.pathB[0] = true;
                        questValues.ForceSerialization();
                        Debug.Log("Say your line");
                    }

                    //Hasn't found jack but repeats hint
                    else if (questValues.pathB[0] == true || questValues.pathA[1] == true)
                    {
                        if (questValues.pathA[3] == false)
                        {
                            dialogueSelector = 1;
                            Debug.Log("Say the other line");
                        }

                        //Finished quest dialogue
                        else if (questValues.pathA[3] == true || questValues.pathB[2] == true || questValues.pathC[1] == true)
                        {
                            dialogueSelector = 2;

                            //Place reward code here
                            FindObjectOfType<Player_Inventory>().currency += questValues.quest1Reward;

                            questValues.questCompleted = true;
                            questValues.objectives2[0] = true;
                            questValues.ForceSerialization();
                        }
                    }
                }

            }

            //Bark after finishing quest
            else
            {
                dialogueSelector = 3;
            }
        }

        //Eddie in the overworld
        else if (ID == 2)
        {
            if (questValues.pathA[2] == false || questValues.pathB[1] == false)
            {
                //Eddie lures player into the trap
                if (questValues.pathC[0] == false)
                {
                    dialogueSelector = 0;
                    questValues.pathC[0] = true;
                }

                //Eddie says another dialogue and tells player to go
                else if (questValues.pathC[0] == true)
                {
                    dialogueSelector = 1;
                }
            }
        }

        //Jack in the cell in underground
        else if (ID==3)
        {
            //Hasn't saved Jack yet
            if (questValues.pathC[1] == false)
            {
                //Speaking to jack and you spoke to his sister
                if (questValues.pathB[0] == true || questValues.pathA[1] == true)
                {
                    dialogueSelector = 0;
                }

                //Speaking to jack and haven't spoken to his sister
                else
                {
                    dialogueSelector = 1;
                }
            }

            //Saved Jack
            else
            {
                dialogueSelector = 2;
            }

        }

        //Talking to Jack after saving him
        else if (ID == 4)
        {
            if (questValues.quest2Completed == false)
            {
                if (questValues.objectives2[0] == false)
                {
                    if (questValues.objectives2[2] == false)
                    {
                        //Jack gives the information about Perry
                        if (questValues.objectives2[1] == false)
                        {
                            dialogueSelector = 0;
                            questValues.objectives2[1] = true;
                        }
                        //Jack repeats the information to the player
                        else
                        {
                            dialogueSelector = 1;
                        }
                    }

                    //Jack rewards the player
                    else
                    {
                        dialogueSelector = 2;
                        FindObjectOfType<Player_Inventory>().currency += questValues.quest2Reward;
                        questValues.quest2Completed = true;
                    }
                }
            }

            //Jack thanks the player again. Just a random bark
            else
            {
                dialogueSelector = 3;
            }
        }

        //Talking to perry after defeating the boss
        else if (ID == 5)
        {
            //Talking to perry the first time
            if (questValues.objectives2[2] == false)
            {
                dialogueSelector = 0;
                questValues.objectives2[2] = true;
            }

            //Talking to perry after helping him but he's still in area 1 zone 2
            else
            {
                dialogueSelector = 1;
                //Or play a cutscene of him leaving
            }
        }

        //Talking to the small langos
        else if (ID == 6)
        {
            //Haven't saved father Lagos yet
            if (questValues.objectives3A[2] == false)
            {
                //First time talking to langos
                if (questValues.objectives3A[0] == false)
                {
                    dialogueSelector = 0;
                    questValues.objectives3A[0] = true;
                }

                //Barks that they say after talking to em
                else
                {
                    dialogueSelector = 1;
                }
            }

            //Saved father lagos
            else
            {
                dialogueSelector = 2;
            }
        }

        //Father Lagos conversations
        else if (ID == 7)
        {
            //Spoke to the kids
            if (questValues.objectives3A[0] == true)
            {
                //First time speaking to player
                if (questValues.objectives3A[1] == false)
                {
                    dialogueSelector = 0;
                    questValues.objectives3A[1] = true;
                }

                //Reminds player that they need to find the key
                else
                {
                    dialogueSelector = 1;
                }
            }

            //Didn't speak to the kids
            else
            {
                //Talks to player for first time
                if (questValues.objectives3B[0] == false)
                {
                    dialogueSelector = 2;
                    questValues.objectives3B[0] = true;
                }

                //Reminds player on what to do
                else
                {
                    dialogueSelector = 3;
                }
            }
        }

        //Slave shopkeeper
        else if (ID == 8)
        {
            dialogueSelector = 0;
        }

        allDialogues.transform.GetChild(dialogueSelector).GetComponent<Dialogue_Trigger>().TriggerDialogue();
    }

    private void FacePlayer()
    {
        /*RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 1.5f);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 1.5f);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 1.5f);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, 1.5f);

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
        }*/

        Vector3 playerPos = FindObjectOfType<PlayerController>().transform.position;
        targetPos = playerPos - transform.position;
        TurnDirection(targetPos);
    }

    void TurnDirection(Vector3 target)
    {
        if (Mathf.Abs(target.x) > Mathf.Abs(target.y))
        {
            if (target.x > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = direction.facingRight;
                //timeBtwChangeDirectionCounter = timeBtwChangeDirection;
            }

            else if (target.x < 0)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = direction.facingLeft;
                //timeBtwChangeDirectionCounter = timeBtwChangeDirection;
            }
        }

        else if (Mathf.Abs(targetPos.x) < Mathf.Abs(targetPos.y))
        {
            if (target.y > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = direction.facingUp;
                //timeBtwChangeDirectionCounter = timeBtwChangeDirection;
            }

            else if (target.y < 0)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = direction.facingDown;
                //timeBtwChangeDirectionCounter = timeBtwChangeDirection;
            }
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
