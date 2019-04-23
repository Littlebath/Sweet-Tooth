using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_Chest : MonoBehaviour
{
    [SerializeField] private ChestScriptableObject chestProperties;

    private PlayerInput pi;
    private Manager_Dialogue md;

    private bool isInRange;
    private bool startDialogue;

    // Start is called before the first frame update
    void Start()
    {
        Reset_Chests();
    }

    // Update is called once per frame
    void Update()
    {
        Chest_Visuals();

        if (pi == null)
        {
            pi = FindObjectOfType<PlayerInput>();
        }

        if (md == null)
        {
            md = FindObjectOfType<Manager_Dialogue>();
        }

        if (isInRange)
        {
            if (pi.interactButton)
            {
                if (!chestProperties.isChestOpen)
                {
                    StartCoroutine(Open_Chest());
                }

            }

            if (startDialogue)
            {
                if (pi.interactButton)
                {
                    if (!md.isTalking)
                    {
                        Debug.Log("Start dialogue");
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

    void Reset_Chests ()
    {
        chestProperties.isChestOpen = false;
    }

    IEnumerator Open_Chest ()
    {
        FindObjectOfType<PlayerController>().enabled = false;
        chestProperties.isChestOpen = true;
        pi.gameObject.transform.GetChild(5).gameObject.SetActive(true);
        pi.gameObject.transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>().sprite = chestProperties.itemInBox.GetComponent<SpriteRenderer>().sprite;
        pi.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("hasItem", true);
        yield return new WaitForSeconds(2f);
        pi.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("hasItem", false);
        pi.gameObject.transform.GetChild(5).gameObject.SetActive(false);
        Instantiate(chestProperties.itemInBox, pi.gameObject.transform.position, Quaternion.identity);
        gameObject.GetComponent<Dialogue_Trigger>().TriggerDialogue();
        startDialogue = true;
        yield return null;
    }

    void Chest_Visuals ()
    {
        if (chestProperties.isChestOpen)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = chestProperties.openedChest;
        }

        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = chestProperties.closedChest;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("In range");
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Out range");
            isInRange = false;
        }
    }
}
