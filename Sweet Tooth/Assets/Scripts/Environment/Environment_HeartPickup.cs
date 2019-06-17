using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_HeartPickup : MonoBehaviour
{
    [SerializeField] private Player_ScriptableObject pso;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (pso.health < pso.maxHealth)
            {
                pso.health += 2;
                Destroy(gameObject);
            }
        }
    }
}
