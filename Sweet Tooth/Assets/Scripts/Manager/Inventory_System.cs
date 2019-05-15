﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_System : MonoBehaviour
{
    public GameObject[] slots;

    //[SerializeField] private GameObject selector;

    [SerializeField] private Player_Inventory piso;

    // Start is called before the first frame update
    void Start()
    {
        
        slots = new GameObject[gameObject.transform.GetChild(0).GetChild(1).childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = gameObject.transform.GetChild(0).GetChild(1).GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Update_Inventory()
    {
        piso = FindObjectOfType<Player_Inventory>();

        for (int i = 0; i < slots.Length; i++)
        {
            //Debug.Log("Spill it");
            slots[i].transform.GetChild(1).GetComponent<Text>().text = piso.numerOfItems[i].ToString();
            slots[i].transform.GetChild(3).GetComponent<Text>().text = piso.maxItemsPerSlot.ToString();
        }
        yield return null;
    }

}
