using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="ShopItem", menuName ="Item Details")]
public class ShopItemSO : ScriptableObject
{
    public int itemID;
    public string itemName;
    public Sprite itemSprite;
    [TextArea(3, 10)]
    public string description;
    public int itemCost;
}
