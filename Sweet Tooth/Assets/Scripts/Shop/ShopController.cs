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
    public GameObject seedCounter;

    public GameObject normalItemList;
    public GameObject specialItemList;

    [Header("Item variables")]
    public GameObject DescriptionText;

    public GameObject firstNormalButton;
    public GameObject firstSpecialButton;

    private bool isShopOpen;

    private PlayerInput pi;

    private EventSystem mySystem;


    private enum Tabs
    {
        NormalItems,
        SpecialItems
    }

    private Tabs tabState;

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

        if (shopUI.activeInHierarchy == true)
        {
            Switch_Tabs();
        }
    }

    public void OpenShop ()
    {
        Debug.Log("Open shop");
        pi = FindObjectOfType<PlayerInput>();
        isShopOpen = true;
        shopUI.SetActive(true);
        FindObjectOfType<PlayerController>().enabled = false;
        FindObjectOfType<PlayerController>().isMelee = false;
        FindObjectOfType<PlayerController>().GetComponent<Animator>().SetBool("isMoving", false);
        gameObject.GetComponent<QuestNPC>().enabled = false;
        FindObjectOfType<Player_Inventory>().enabled = false;
        FindObjectOfType<Player_Map>().enabled = false;

        UpdatePlayerMoney();
        OpenNormalShopItems();
        mySystem.SetSelectedGameObject(firstNormalButton, new BaseEventData(mySystem));
    }

    public void CloseShop ()
    {
        Debug.Log("Close shop");
        isShopOpen = false;
        shopUI.SetActive(false);
        FindObjectOfType<PlayerController>().enabled = true;
        FindObjectOfType<Player_Inventory>().enabled = true;
        gameObject.GetComponent<QuestNPC>().enabled = true;
        FindObjectOfType<Manager_Dialogue>().enabled = true;
        FindObjectOfType<Player_Map>().enabled = true;
    }

    void Switch_Tabs ()
    {
        if (tabState == Tabs.NormalItems)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenSpecialShopItems();
            }

        }

        else if (tabState == Tabs.SpecialItems)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            { 
                OpenNormalShopItems();
            }
        }
    }

    void OpenNormalShopItems ()
    {
        Debug.Log("Open normal tab");
        tabState = Tabs.NormalItems;
        normalItemList.SetActive(true);
        specialItemList.SetActive(false);

        mySystem.SetSelectedGameObject(firstNormalButton, new BaseEventData(mySystem));
    }

    void OpenSpecialShopItems ()
    {
        Debug.Log("Open special tab");
        normalItemList.SetActive(false);
        specialItemList.SetActive(true);
        tabState = Tabs.SpecialItems;

        mySystem.SetSelectedGameObject(firstSpecialButton, new BaseEventData(mySystem));
    }


    public void UpdatePlayerMoney()
    {
        moneyCounter.GetComponent<Text>().text = "$" + FindObjectOfType<Player_Inventory>().currency.ToString();
        seedCounter.GetComponent<Text>().text = "$" + FindObjectOfType<Player_Inventory>().seeds.ToString();
    }

}
