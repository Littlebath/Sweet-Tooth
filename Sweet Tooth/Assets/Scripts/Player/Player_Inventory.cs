using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Inventory : Inventory
{
    //[SerializeField] private Player_InventoryScriptableObject inventoryProperties;

    public GameObject inventorySystem;
    [SerializeField] private GameObject selector;

    private int selection;

    [SerializeField] Transform spawnPoint;

    private PlayerInput pi;
    private PlayerController pc;

    private void Awake()
    {
        inventorySystem = FindObjectOfType<Inventory_System>().gameObject.transform.GetChild(0).gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        pi = FindObjectOfType<PlayerInput>();
        pc = FindObjectOfType<PlayerController>();

        numerOfItems = new int[inventorySystem.transform.GetChild(1).childCount];
        slots = new GameObject[numerOfItems.Length];

        Mathf.Clamp(selection, 0, numerOfItems.Length - 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (inventorySystem == null)
        {
            inventorySystem = FindObjectOfType<Inventory_System>().gameObject.transform.GetChild(0).gameObject;
            //Debug.Log("Locate");
        }

        if (inventorySystem.activeSelf)
        {
            if (pi.inventoryButton || pi.attackButton)
            {
                StartCoroutine(Close_Inventory());
            }
        }

        else
        {
            if (pi.inventoryButton)
            {
                StartCoroutine(Open_Inventory());
            }
        }

        if (inventorySystem != null)
        {
            if (inventorySystem.activeSelf)
            {
                //Selection process
                Selection_Procecss();
            }
        }

    }

    IEnumerator Open_Inventory()
    {
        //Debug.Log("Open Inventory");
        selector = inventorySystem.transform.GetChild(2).gameObject;
        inventorySystem.SetActive(true);
        StartCoroutine(FindObjectOfType<Inventory_System>().Update_Inventory());
        StartCoroutine(Stop_Player_Movement());
        //Time.timeScale = 0;
        yield return null;
    }

    IEnumerator Close_Inventory()
    {
        //Debug.Log("Close Inventory");
        inventorySystem.SetActive(false);
        StartCoroutine(Allow_Player_Movement());
        //Time.timeScale = 1;
        yield return null;
    }

    IEnumerator Stop_Player_Movement()
    {
        pc.rb2d.velocity = Vector2.zero;
        pc.isMoving = false;
        pc.enabled = false;
        pi.horizontalInput = 0f;
        pi.verticalInput = 0f;
        gameObject.transform.GetComponent<Animator>().SetBool("isMoving", false);
        yield return null;
    }

    IEnumerator Allow_Player_Movement()
    {
        yield return new WaitForSeconds(0.1f);
        pc.enabled = true;
    }

    void Selection_Procecss ()
    {
        if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
        {
            if (selection < numerOfItems.Length - 1)
            {
                selection++;
            }
        }

        else if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
        {
            if (selection > 0)
            {
                selection--;
            }
        }

        selector.transform.position = inventorySystem.transform.GetChild(1).GetChild(selection).transform.position;

        for (int i = 0; i < numerOfItems.Length; i++)
        {
            if (i == selection)
            {
                inventorySystem.transform.GetChild(1).GetChild(i).GetComponent<Shadow>().enabled = true;
                inventorySystem.transform.GetChild(1).GetChild(i).localScale = new Vector3(1.25f, 1.25f, 1.25f);
            }

            else
            {
                inventorySystem.transform.GetChild(1).GetChild(i).GetComponent<Shadow>().enabled = false;
                inventorySystem.transform.GetChild(1).GetChild(i).localScale = new Vector3(1f, 1f, 1f);
            }
        }

        Use_Item();

        Drop_Item();
    }

    private void Drop_Item()
    {
        if (pi.dashButton)
        {
            if (slots[selection] != null)
            {
                //Drop Item here!!
                Collider2D areaDrop = Physics2D.OverlapCircle(spawnPoint.position, 0.5f);

                if (areaDrop == null)
                {
                    GameObject item = Resources.Load<GameObject>("Prefabs/Designer/Level/PickUps/Items/" + slots[selection].GetComponent<Item>().gameObject.name);
                    Instantiate(item, pc.spawnPoint.transform.position, Quaternion.identity);
                    item.name = slots[selection].GetComponent<Item>().gameObject.name;

                    if (numerOfItems[selection] > 1)
                    {
                        numerOfItems[selection]--;
                    }

                    else
                    {
                        numerOfItems[selection]--;
                        slots[selection] = null;
                        Destroy(inventorySystem.transform.GetChild(1).GetChild(selection).GetChild(2).gameObject);
                    }

                    StartCoroutine(FindObjectOfType<Inventory_System>().Update_Inventory());
                }

                else
                {
                    Debug.Log("Blocked by object");
                    Debug.Log(areaDrop);
                }

            }
        }
    }

    void Use_Item ()
    {
        if (pi.interactButton)
        {
            if (slots[selection] != null)
            {
                //Use Item here!!
                slots[selection].GetComponent<Item>().Use_Item();

                if (numerOfItems[selection] > 1)
                {
                    numerOfItems[selection]--;
                }

                else
                {
                    numerOfItems[selection]--;
                    slots[selection] = null;
                    Destroy(inventorySystem.transform.GetChild(1).GetChild(selection).GetChild(2).gameObject);
                }

                StartCoroutine(FindObjectOfType<Inventory_System>().Update_Inventory());
            }
        }
    }
}
