using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_ArmorPickUp : MonoBehaviour
{
    [SerializeField] private Player_ScriptableObject pso;

    // Start is called before the first frame update
    void Start()
    {
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
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag ("Player"))
        {
            pso.armor++;
            //FindObjectOfType<UI_ArmorDisplay>().Init_Hearts();
            StartCoroutine (FindObjectOfType<UI_ArmorDisplay>().Pulse());
            Destroy(gameObject);


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
