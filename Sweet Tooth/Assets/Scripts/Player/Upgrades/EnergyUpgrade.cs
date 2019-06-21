using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyUpgrade : MonoBehaviour
{
    public Player_ScriptableObject pso;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pso.maxEnergy = 10;
            Destroy(gameObject);
        }
    }
}


