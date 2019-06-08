using System;
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
    public GameObject jillNPC;
    public GameObject eddieNPC;
    public GameObject caveEntrance;
    public GameObject trapDoorEntrance;
    public GameObject jackNPC;
    public GameObject saveJackTrigger;
    public GameObject jackRescue;
    public GameObject foundPercyTrigger;
    public GameObject percyNPC;
    public GameObject percySave;
    public GameObject smallLango;
    public GameObject fatherLango;
    public GameObject SavingFatherTrigger;
    public GameObject FatherLangoSaved;


    private void Awake()
    {
        
    }

    void Start()
    {
        //Destroy_Duplicates();
    }

    private void OnEnable()
    {
        StartCoroutine(Hide_Objects());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            questValues.ResetQuest();
            questValues.ForceSerialization();
        }
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

    IEnumerator Hide_Objects ()
    {
        //All variables to disable
        shopkeeperNPC.SetActive(false);
        jillNPC.SetActive(false);
        eddieNPC.SetActive(false);
        caveEntrance.SetActive(false);
        trapDoorEntrance.SetActive(false);
        jackNPC.SetActive(false);
        saveJackTrigger.SetActive(false);
        jackRescue.SetActive(false);
        foundPercyTrigger.SetActive(false);
        percyNPC.SetActive(false);
        percySave.SetActive(false);
        smallLango.SetActive(false);
        fatherLango.SetActive(false);
        SavingFatherTrigger.SetActive(false);
        FatherLangoSaved.SetActive(false);


        Debug.Log("Hide");

        yield return new WaitForEndOfFrame ();

        //Conditions
        if (SceneManager.GetActiveScene().name == "Shop")
        {
            Debug.Log("Reactivate");
            shopkeeperNPC.SetActive(true);
        }

        else if (SceneManager.GetActiveScene().name == "Area 1 Zone 1")
        {
            jillNPC.SetActive(true);
            eddieNPC.SetActive(true);
            Debug.Log("Hid once");
        }

        else if (SceneManager.GetActiveScene().name == "Underground Part A")
        {
            jackNPC.SetActive(true);
            saveJackTrigger.SetActive(true);
            caveEntrance.SetActive(true);
            trapDoorEntrance.SetActive(true);
            Debug.Log("Hid twice");
        }

        else if (SceneManager.GetActiveScene().name == "Area 1 Zone 2")
        {
            jackRescue.SetActive(true);
            foundPercyTrigger.SetActive(true);

            if (questValues.objectives2[2] == false)
            {
                percyNPC.SetActive(true);
            }

            else
            {
                percySave.SetActive(true);
            }
        }

        else if (SceneManager.GetActiveScene().name == "Area 1 Zone 3")
        {

        }
    }
}
