﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DashCounter : MonoBehaviour
{
    private Text dashCounterDisplay;

    [SerializeField] private Player_ScriptableObject psc;

    // Start is called before the first frame update
    void Start()
    {
        dashCounterDisplay = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        dashCounterDisplay.text = psc.energyCounter.ToString();

        //Set_Bar();
    }

    void Set_Bar ()
    {
        float maxEnergy = psc.maxEnergy;
        float curEnergy = psc.energyCounter;

        float value = curEnergy / maxEnergy;

        GetComponent<Image>().fillAmount = value;
    }
}
