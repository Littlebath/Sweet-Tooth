using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest_1_Manager : MonoBehaviour
{
    public Quest1ScriptableObject questValues;

    public static bool questManager1Existing;

    [Header("Quest variables")]
    public GameObject shopkeeperNPC;

    void Start()
    {
        Destroy_Duplicates();

        Fill_Up_Variables();
        Hide_Objects();
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

    }

    void Hide_Objects ()
    {
        //All variables to disable
        shopkeeperNPC.SetActive(false);

        //Conditions
        if (SceneManager.GetActiveScene().name == "Shop")
        {
            Debug.Log("Reactivate");
            shopkeeperNPC.SetActive(true);
        }
    }

    void Fill_Up_Variables ()
    {

    }
}
