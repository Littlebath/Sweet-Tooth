using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    key,
    enemy,
    trigger,
    hold,
    multiple
}
public class Door : Interactable
{
    [Header("Door Variables")]
    public DoorType thisDoorType;
    public bool open = false;
    private Player_Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicCollider;

    private void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (playerInRange && thisDoorType == DoorType.key)
        {
            if (playerInventory.numberOfKeys > 0)
            {
                playerInventory.numberOfKeys--;
                Open();
            }
        }*/
    }

    public void Open()
    {
        doorSprite.enabled = false;
        open = true;
        physicCollider.enabled = false;
        //Destroy(gameObject.transform.parent.gameObject);
    }

    public void Close()
    {
        doorSprite.enabled = true;
        open = false;
        physicCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("In Trigger");

            if (thisDoorType == DoorType.key)
            {
                if (!open)
                {
                    if (FindObjectOfType<Player_Inventory>().numberOfKeys > 0)
                    {
                        FindObjectOfType<Player_Inventory>().numberOfKeys--;
                        Open();
                    }
                }
            }
        }
    }
}
