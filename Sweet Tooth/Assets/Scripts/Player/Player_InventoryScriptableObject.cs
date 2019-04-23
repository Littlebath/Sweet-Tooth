using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Player/Inventory", order = 1)]

public class Player_InventoryScriptableObject : ScriptableObject
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
}
