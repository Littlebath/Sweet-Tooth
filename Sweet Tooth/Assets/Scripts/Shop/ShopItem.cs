﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour
{
    public ShopItemSO itemDetails;

    public GameObject itemName;
    public GameObject itemCost;
    public GameObject description;
    public GameObject itemSprite;

    private EventSystem mySystem;

    // Start is called before the first frame update
    void Start()
    {
        mySystem = EventSystem.current;

        if (itemDetails != null)
        {
            itemName.GetComponent<Text>().text = itemDetails.itemName;
            itemCost.GetComponent<Text>().text = "$" + itemDetails.itemCost.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (itemDetails != null)
        {
            if (mySystem.currentSelectedGameObject == gameObject)
            {
                description.GetComponent<Text>().text = itemDetails.description;
                itemSprite.GetComponent<Image>().sprite = itemDetails.itemSprite;
                Debug.Log(itemDetails.itemName);
            }
        }
    }

    public void Buy_Item ()
    {

    }
}
