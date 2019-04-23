using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon_Key : MonoBehaviour
{
    private Player_Inventory piso;

    // Start is called before the first frame update
    void Start()
    {                                                                                                                                                       
        
    }

    // Update is called once per frame
    void Update()
    {
        if (piso == null)
        {
            piso = FindObjectOfType<Player_Inventory>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Pick up");
            piso.numberOfKeys++;                                                                                                                                                                
            Destroy(gameObject);                                                                                                                                                                                                                    
        }
    }
}
