using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_Switch_Multiple : MonoBehaviour
{
    public Sprite activeSprite;
    private Sprite originalSprite;

    public int doorSelector;

    private bool active;

    private SpriteRenderer mySprite;

    public Door[] multipleDoors;


    // Start is called before the first frame update
    void Start()
    {
        mySprite = gameObject.GetComponent<SpriteRenderer>();
        originalSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        DeActivateSwitch();

        if (gameObject.GetComponent<Save_ObjState>() != null)
        {
            if (gameObject.GetComponent<Save_ObjState>().obj != null)
            {
                multipleDoors[gameObject.GetComponent<Save_ObjState>().obj.saveState].Open();
            }
        }
    }

    public void ActivateSwitch()
    {
        active = true;

        if (doorSelector < multipleDoors.Length - 1)
        {
            doorSelector++;
        }

        else
        {
            doorSelector = 0;
        }

        multipleDoors[doorSelector].Open();
        mySprite.sprite = activeSprite;
    }

    public void DeActivateSwitch()
    {
        active = false;

        for (int i = 0; i < multipleDoors.Length; i++)
        {
            multipleDoors[i].Close();
        }
        multipleDoors[doorSelector].Open();

        if (gameObject.GetComponent<Save_ObjState>() != null)
        {
            if (gameObject.GetComponent<Save_ObjState>().obj != null)
            {
                gameObject.GetComponent<Save_ObjState>().obj.saveState = doorSelector;
                gameObject.GetComponent<Save_ObjState>().obj.ForceSerialization();
            }
        }

        mySprite.sprite = originalSprite;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Is it the player?
        if (other.CompareTag("Player") || other.CompareTag("Stone") || other.CompareTag("Enemy") || other.gameObject.layer == 16)
        {
            ActivateSwitch();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Stone") || collision.CompareTag("Enemy") || collision.gameObject.layer == 16)
        {
            DeActivateSwitch();
        }
    }
}
