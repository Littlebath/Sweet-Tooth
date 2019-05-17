using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopController : MonoBehaviour
{
    public GameObject shopUI;

    [Header("Shop Variables")]
    public GameObject moneyCounter;

    [Header("Item variables")]
    public GameObject DescriptionText;
    public GameObject itemImage;

    public GameObject firstButton;

    private bool isShopOpen;

    private PlayerInput pi;

    private EventSystem mySystem;

    // Start is called before the first frame update
    void Start()
    {
        mySystem = EventSystem.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (pi != null)
        {
            if (pi.attackButton)
            {
                CloseShop();
            }
        }
    }

    public void OpenShop ()
    {
        pi = FindObjectOfType<PlayerInput>();
        isShopOpen = true;
        shopUI.SetActive(true);
        FindObjectOfType<PlayerController>().enabled = false;
        FindObjectOfType<PlayerController>().isMelee = false;
        FindObjectOfType<PlayerController>().GetComponent<Animator>().SetBool("isMoving", false);
        gameObject.GetComponent<QuestNPC>().enabled = false;
        FindObjectOfType<Player_Inventory>().enabled = false;
        FindObjectOfType<Player_Map>().enabled = false;

        mySystem.SetSelectedGameObject(firstButton, new BaseEventData(mySystem));

        UpdatePlayerMoney();
    }

    public void CloseShop ()
    {
        isShopOpen = false;
        shopUI.SetActive(false);
        FindObjectOfType<PlayerController>().enabled = true;
        FindObjectOfType<Player_Inventory>().enabled = true;
        gameObject.GetComponent<QuestNPC>().enabled = true;
        FindObjectOfType<Manager_Dialogue>().enabled = true;
        FindObjectOfType<Player_Map>().enabled = true;
    }


    public void UpdatePlayerMoney()
    {
        moneyCounter.GetComponent<Text>().text = "$" + FindObjectOfType<Player_Inventory>().currency.ToString();
    }

}
