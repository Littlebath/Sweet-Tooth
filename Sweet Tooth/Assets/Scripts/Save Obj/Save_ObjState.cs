using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_ObjState : MonoBehaviour
{
    public SaveStateObj obj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            //Reset World
            Reset_World();
        }
    }

    void Reset_World ()
    {
        obj.saveState = 0;
        Debug.Log("Reset");
    }
}
