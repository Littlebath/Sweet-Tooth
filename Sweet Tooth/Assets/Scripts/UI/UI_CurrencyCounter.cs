using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CurrencyCounter : MonoBehaviour
{
    private Text currentDisplay;

    // Start is called before the first frame update
    void Start()
    {
        currentDisplay = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        currentDisplay.text = "x" + FindObjectOfType<Player_Inventory>().currency.ToString();
    }

    public void Update_Currency ()
    {
        
    }
}
