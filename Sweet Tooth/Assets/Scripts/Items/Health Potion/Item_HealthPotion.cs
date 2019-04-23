using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_HealthPotion : MonoBehaviour
{
    public int health;

    private Item itemEffect;

	// Use this for initialization
	void Start ()
    {
        itemEffect = Resources.Load<GameObject>("Prefabs/Designer/Level/PickUps/Items/Health Potion").GetComponent<Item>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

}
