using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_DontDestroyOnLoad : MonoBehaviour
{
    public static Manager_DontDestroyOnLoad instance;

	// Use this for initialization
	void Start ()
    {
        DontDestroy();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void DontDestroy()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
            return;
        }
    }
}
