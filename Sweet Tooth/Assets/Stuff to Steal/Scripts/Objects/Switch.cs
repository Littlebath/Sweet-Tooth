﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool active;
    public BoolValue storedValue;
    public Sprite activeSprite;
    private SpriteRenderer mySprite;
    public Door thisDoor;

    public Door[] multipleDoors;

    private Sprite originalSprite;

    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        originalSprite = mySprite.sprite;
        active = storedValue.RuntimeValue;

        if(active)
        {
            ActivateSwitch();
        }
    }

    public void ActivateSwitch()
    {
        active = true;
        storedValue.RuntimeValue = active;
        thisDoor.Open();
        mySprite.sprite = activeSprite;
    }

    public void DeActivateSwitch ()
    {
        active = false;
        storedValue.RuntimeValue = active;
        thisDoor.Close();
        mySprite.sprite = originalSprite;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Is it the player?
        if(other.CompareTag("Player") || other.CompareTag("Stone") || other.CompareTag("Enemy") || other.gameObject.layer == 16)
        {
            ActivateSwitch();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Stone") || collision.CompareTag("Enemy") || collision.gameObject.layer == 16)
        {
            if (thisDoor.thisDoorType == DoorType.hold)
            {
                DeActivateSwitch();
            }
        }
    }
}
