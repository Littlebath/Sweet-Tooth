using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public GameObject itemSprite;

    private Player_Inventory piso;

    [SerializeField] private Inventory_System system;

    private bool canBePickedUp;


    [SerializeField] private GameObject[] slots;

    [Header("Don't touch the ID number. It's used for identification")]
    [SerializeField] private int itemID;

    // Start is called before the first frame update
    private void Awake()
    {

    }

    void Start()
    {        
        piso = FindObjectOfType<Player_Inventory>();
        StartCoroutine(EnablePickup());

        if (gameObject.GetComponent<Save_ObjState>() != null)
        {
            if (gameObject.GetComponent<Save_ObjState>().obj != null)
            {
                if (gameObject.GetComponent<Save_ObjState>().obj.saveState == 1)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //FindObjects();

        if (piso == null)
        {
            piso = FindObjectOfType<Player_Inventory>();
        }

        if (system == null)
        {
            system = FindObjectOfType<Inventory_System>();
        }

        else
        {
            StartCoroutine(FindObjects());
        }
         
    }

    IEnumerator EnablePickup ()
    {
        yield return new WaitForSeconds(0.5f);
        canBePickedUp = true;
    }

    public void Use_Item ()
    {
        Debug.Log("use item");
        switch (itemID)
        {
            case 0:
                //gameObject.GetComponent<Item_HealthPotion>().Item_Used();
                FindObjectOfType<Item_Effect>().Health_Potion_Effect();
                break;

            case 1:
                //gameObject.GetComponent<Item_EnergyPotion>().Item_Used();
                FindObjectOfType<Item_Effect>().Energy_Potion_Effect();
                break;

                //Add more items accordingly
        }
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (system != null)
            {
                Add_Item();

                if (gameObject.GetComponent<Save_ObjState>() != null)
                {
                    if (gameObject.GetComponent<Save_ObjState>().obj != null)
                    {
                        gameObject.GetComponent<Save_ObjState>().obj.saveState = 1;
                        gameObject.GetComponent<Save_ObjState>().obj.ForceSerialization();
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (system != null)
            {
                Add_Item();

                if (gameObject.GetComponent<Save_ObjState>() != null)
                {
                    if (gameObject.GetComponent<Save_ObjState>().obj != null)
                    {
                        gameObject.GetComponent<Save_ObjState>().obj.saveState = 1;
                        gameObject.GetComponent<Save_ObjState>().obj.ForceSerialization();
                    }
                }
            }
        }

    }

    private void Add_Item()
    {
        if (canBePickedUp)
        {
            string pathName = "Prefabs/Designer/Level/PickUps/Items/" + gameObject.name;
            string nameOfGameObject = gameObject.name.Split('(')[0];

            GameObject referItem = Resources.Load<GameObject>("Prefabs/Designer/Level/PickUps/Items/" + nameOfGameObject);
            Debug.Log(referItem.name);

            for (int i = 0; i < piso.slots.Length; i++)
            {
                if (referItem != piso.slots[i])
                {
                    if (piso.slots[i] == null)
                    {
                        piso.slots[i] = referItem;
                        Debug.Log("Add one" + Time.time);
                        GameObject sprite = Instantiate(itemSprite, slots[i].transform, false);
                        sprite.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        piso.numerOfItems[i]++;
                        Destroy(gameObject);
                        return;
                    }

                    else
                    {
                        Debug.Log("Don't pick up item");
                    }
                }

                else
                {
                    if (piso.numerOfItems[i] < piso.maxItemsPerSlot)
                    {
                        Debug.Log("Add another" + Time.time);
                        piso.numerOfItems[i]++;
                        Destroy(gameObject);
                        return;
                    }

                    else
                    {
                        Debug.Log("Don't pick up item");
                    }
                }
            }
        }
    }


    /*if (piso.slots[i] == referItem)
    {
        if (piso.numerOfItems[i] < piso.maxItemsPerSlot)
        {
            Debug.Log("Add another" + Time.time);
            piso.numerOfItems[i]++;
            Destroy(gameObject);
            return;
        }
    }

    else
    {
        if (piso.slots[i] == null)
        {
            piso.slots[i] = referItem;
            Debug.Log("Add one" + Time.time);
            Instantiate(itemSprite, slots[i].transform, false);
            piso.numerOfItems[i]++;
            Destroy(gameObject);                                                                                                
            return;
        }
    }*/

   

    private IEnumerator FindObjects ()
    {
        slots = new GameObject[system.slots.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = system.slots[i];
        }
        yield return null;
    }
}
