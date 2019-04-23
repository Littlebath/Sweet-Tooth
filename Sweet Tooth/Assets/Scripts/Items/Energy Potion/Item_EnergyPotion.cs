using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_EnergyPotion : MonoBehaviour
{
    public int energy;

    [SerializeField] private Item itemEffect;

    // Start is called before the first frame update
    void Start()
    {
        itemEffect = Resources.Load<GameObject>("Prefabs/Designer/Level/PickUps/Items/Energy Potion").GetComponent<Item>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
