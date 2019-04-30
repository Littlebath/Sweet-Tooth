using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling_Chase : MonoBehaviour
{
    [SerializeField] private Enemy_Patrolling enemy;
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
            Debug.Log("Chase");
            enemy.EnemyBehavior = enemyBehavior.Chasing;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Return");
            enemy.EnemyBehavior = enemyBehavior.Patrolling;
        }
    }
}
