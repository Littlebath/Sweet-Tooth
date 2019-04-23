using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_HealthDisplay : MonoBehaviour
{
    [SerializeField] private Player_ScriptableObject psc;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = "Health: " + psc.health.ToString();
    }
}
