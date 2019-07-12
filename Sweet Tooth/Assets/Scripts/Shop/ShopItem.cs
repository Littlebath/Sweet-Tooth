using System.Collections;
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
            itemSprite.GetComponent<Image>().sprite = itemDetails.itemSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (itemDetails != null)
        {
            if (mySystem.currentSelectedGameObject == gameObject)
            {
                itemCost.GetComponent<Text>().text = itemDetails.itemCost.ToString();
                itemName.GetComponent<Text>().text = itemDetails.itemName;
                description.GetComponent<Text>().text = itemDetails.description;
                itemSprite.GetComponent<Image>().sprite = itemDetails.itemSprite;
                Debug.Log(itemDetails.itemName);
            }
        }
    }

    public void Buy_Item ()
    {
        if (itemDetails.itemType == ShopItemSO.typeOfItem.Normal)
        {
            if (FindObjectOfType<Player_Inventory>().currency >= itemDetails.itemCost)
            {
                FindObjectOfType<Player_Inventory>().currency -= itemDetails.itemCost;
                GameObject item = Instantiate(itemDetails.itemForPlayer, FindObjectOfType<PlayerController>().transform.position, Quaternion.identity);

                if (item.GetComponent<Item>() != null)
                {
                    item.GetComponent<Item>().Add_Item();
                    Destroy(item);
                }

                FindObjectOfType<ShopController>().UpdatePlayerMoney();
                Debug.Log("Bought Item");
            }

            else
            {
                Debug.Log("Not enough money");
            }
        }

        else if (itemDetails.itemType == ShopItemSO.typeOfItem.Special)
        {
            if (FindObjectOfType<Player_Inventory>().seeds >= itemDetails.itemCost)
            {
                FindObjectOfType<Player_Inventory>().seeds -= itemDetails.itemCost;
                Instantiate(itemDetails.itemForPlayer, FindObjectOfType<PlayerController>().transform.position, Quaternion.identity);

                FindObjectOfType<ShopController>().UpdatePlayerMoney();
                Debug.Log("Bought Item");
            }

            else
            {
                Debug.Log("Not enough money");
            }
        }
    }
}
