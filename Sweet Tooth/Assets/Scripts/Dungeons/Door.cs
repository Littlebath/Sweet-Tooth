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
        if (GetComponent<Save_ObjState>() != null)
        {
            if (GetComponent<Save_ObjState>().obj.saveState == 0)
            {
                Close();
            }

            else if (GetComponent<Save_ObjState>().obj.saveState == 1)
            {
                Open();
            }

        }
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
        if (GetComponent<Save_ObjState>() != null)
        {
            GetComponent<Save_ObjState>().obj.saveState = 1;
            gameObject.GetComponent<Save_ObjState>().obj.ForceSerialization();
        }
        physicCollider.enabled = false;
        Debug.Log("Open");
        //Destroy(gameObject.transform.parent.gameObject);
    }

    public void Close()
    {
        Debug.Log("Close");
        doorSprite.enabled = true;
        open = false;
        if (GetComponent<Save_ObjState>() != null)
        {
            GetComponent<Save_ObjState>().obj.saveState = 0;
            gameObject.GetComponent<Save_ObjState>().obj.ForceSerialization();
        }
        physicCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (thisDoorType == DoorType.key)
            {
                if (GetComponent<Save_ObjState>().obj.saveState == 0)
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
