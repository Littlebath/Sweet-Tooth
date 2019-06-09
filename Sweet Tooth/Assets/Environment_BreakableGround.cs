using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_BreakableGround : MonoBehaviour
{
    private bool isGroundBroken;

    // Start is called before the first frame update
    void Start()
    {
        /*if (gameObject.GetComponent<Save_ObjState>() != null)
        {
            if (gameObject.GetComponent<Save_ObjState>().obj.saveState == 1)
            {
                BreakGround();
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BreakGround ()
    {
        if (!isGroundBroken)
        {
            isGroundBroken = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
