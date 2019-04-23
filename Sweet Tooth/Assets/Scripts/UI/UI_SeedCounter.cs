using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SeedCounter : MonoBehaviour
{
    public Text seedDisplay;

    public Player_Inventory piso;

    // Start is called before the first frame update
    void Start()
    {
        seedDisplay = gameObject.GetComponent<Text>();
        piso = FindObjectOfType<Player_Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        seedDisplay.text = "x" + piso.seeds.ToString();
    }
}
