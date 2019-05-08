using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    private PlayerInput pi;
    // Start is called before the first frame update
    void Start()
    {
        pi = gameObject.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        Shop_Exit();
    }

    void Shop_Exit ()
    {
        if (pi.attackButton)
        {
            Debug.Log("Exit Scene");
            StartCoroutine(gameObject.GetComponent<LoadNewArea>().GoToNextLevel());
        }
    }
}
