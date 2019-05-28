using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HelpButton : MonoBehaviour
{
    public int ID;

    private EventSystem mySystem;

    private void Start()
    {
        mySystem = EventSystem.current;
    }

    private void Update()
    {
        if (mySystem.currentSelectedGameObject == gameObject)
        {
            if (ID == 0)
            {
                FindObjectOfType<Options_Controller>().Open_Keyboard();
            }

            else if (ID == 1)
            {
                FindObjectOfType<Options_Controller>().Open_Controller();
            }
        }
    }
}
