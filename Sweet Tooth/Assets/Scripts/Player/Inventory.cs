using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Manages the properties for the player inventory")]
    [Space(20)]

    [Header("Max number of similar type of items player can hold")]
    public int maxItemsPerSlot;

    [Header("DON'T TOUCH BELOW PROPERTIES! FOR DEBUGGING")]
    public int[] numerOfItems;
    public GameObject[] slots;

    [Header("Important Items")]
    public int numberOfKeys;
    public int currency;
    public int seeds;

    /*public void AddItem(Item itemToAdd)
    {
        // Is the item a key?
        if (itemToAdd.isKey)
        {
            numberOfKeys++;
        }
        else
        {
            if (!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }
    }*/
}
