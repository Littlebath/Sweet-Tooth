using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_1_Manager : MonoBehaviour
{
    public Quest1ScriptableObject questValues;

    public static bool questManager1Existing;

    [Header("Quest variables")]
    public GameObject shopkeeperNPC;

    void Start()
    {
        Destroy_Duplicates();
    }

    void Update()
    {
        PathA();
    }

    void Destroy_Duplicates ()
    {
        if (!questManager1Existing)
        {
            questManager1Existing = true;
            DontDestroyOnLoad(transform.gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    void PathA ()
    {
        if (questValues.pathA[0] == false)
        {
            shopkeeperNPC.SetActive(true);
        }

        else
        {
            shopkeeperNPC.SetActive(false);
        }
    }
}
