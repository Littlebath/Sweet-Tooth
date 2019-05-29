using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_SaveManager : MonoBehaviour
{
    public SaveStateObj [] allSaves;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            for (int i = 0; i < allSaves.Length; i++)
            {
                allSaves[i].ResetValues();
                allSaves[i].ForceSerialization();
            }
        }
    }
}
