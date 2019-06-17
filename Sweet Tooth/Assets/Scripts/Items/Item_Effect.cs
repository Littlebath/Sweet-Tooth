using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Effect : MonoBehaviour
{
    private Player_ScriptableObject psc;

    [HideInInspector] public int healthBoost;
    [HideInInspector] public int energyBoost;

    [SerializeField] private GameObject[] consumables;

    // Start is called before the first frame update
    void Start()
    {
        psc = Resources.Load<Player_ScriptableObject>("Scriptable Objects/Player/Player Values");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Health_Potion_Effect()
    {
        healthBoost = consumables[0].GetComponent<Item_HealthPotion>().health;
        Debug.Log("Health Restored");
        psc.health += healthBoost;

        if (psc.health >= psc.maxHealth)
        {
            psc.health = psc.maxHealth;
        }
    }

    public void Energy_Potion_Effect()
    {
        energyBoost = consumables[1].GetComponent<Item_EnergyPotion>().energy;
        Debug.Log("Energy Restored");
        psc.energyCounter += energyBoost;

        if (psc.energyCounter >= psc.maxEnergy)
        {
            psc.energyCounter = psc.maxEnergy;
        }
    }

    public void Explosive_Nut_Effect ()
    {
        FindObjectOfType<Player_Inventory>().Drop_Item();
    }


}
