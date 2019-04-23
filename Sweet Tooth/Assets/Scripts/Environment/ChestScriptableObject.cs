using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chest", menuName = "Chest/Special Items", order = 1)]

public class ChestScriptableObject : ScriptableObject
{
    public bool isChestOpen;

    public Sprite openedChest;
    public Sprite closedChest;

    public GameObject itemInBox;
}
