using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_DropScript : MonoBehaviour
{
    [SerializeField] private int percentageOfSpawningItem;

    [SerializeField] private GameObject [] itemsToSpawn;

    private int itemSpawner;

    // Start is called before the first frame update
    void Start()
    {
        itemSpawner = Random.Range(0, 100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn_Item ()
    {
        if (itemSpawner <= percentageOfSpawningItem)
        {
            int itemRandomizer = Random.Range(0, itemsToSpawn.Length - 1);
            GameObject item = Instantiate(itemsToSpawn[itemRandomizer], transform.position, Quaternion.identity);
            Debug.Log("Item dropped is " + item);
        }

        else
        {
            Debug.Log("Don't drop item");
        }
    }
}
