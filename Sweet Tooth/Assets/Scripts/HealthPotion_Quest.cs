using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion_Quest : MonoBehaviour
{
    public bool hasPotion;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<Save_ObjState>().obj.saveState == 1)
        {
            hasPotion = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Item_HealthPotion>() != null)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            hasPotion = true;
            Debug.Log("Potion in AOE");
            Destroy(collision.gameObject);
        }
    }
}
