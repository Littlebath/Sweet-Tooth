using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_Seed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Add moneys");
            FindObjectOfType<Player_Inventory>().seeds++;
            Destroy(gameObject);
            //Add to currency
        }
    }
}
