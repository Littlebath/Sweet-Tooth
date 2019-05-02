using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_EnemySpawner : MonoBehaviour
{
    public GameObject[] enemiesToSpawn;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private GameObject enemyCounter;

    private int noOfEnemies;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        noOfEnemies = enemyCounter.transform.childCount;

        if (noOfEnemies <= 0)
        {
            //Spawn the enemy
            StartCoroutine(Spawn_Enemy());
        }

        else
        {
            //Don't do anything just wait
        }
    }

    IEnumerator Spawn_Enemy ()
    {
        for (int i = 0; i < enemiesToSpawn.Length; i++)
        {
            Instantiate(enemiesToSpawn[i], enemyCounter.transform, false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
