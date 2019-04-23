using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCounter : MonoBehaviour
{
    public Text keyDisplay;

    public Player_Inventory piso;

    // Start is called before the first frame update
    void Start()
    {
        keyDisplay = gameObject.GetComponent<Text>();
        piso = FindObjectOfType<Player_Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        keyDisplay.text = "x" + piso.numberOfKeys.ToString();
    }
}
