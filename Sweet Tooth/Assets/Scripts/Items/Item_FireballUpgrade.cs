using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_FireballUpgrade : MonoBehaviour
{
    [SerializeField] private Player_ScriptableObject values;
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
            values.hasFireball = true;
            Destroy(gameObject);
        }
    }
}
