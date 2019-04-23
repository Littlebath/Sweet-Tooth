using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HouseType
{
    Normal,
    Chocolate,
    Ice
}

public class Environment_EnemyHome : MonoBehaviour
{
    public HouseType houseType;

    [SerializeField] private GameObject[] enemiesToSpawn;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private int healthDrop;
    [SerializeField] private int speedDrop;
    private GameObject fire;
    private bool isBurning;
    private bool isEmpty;

    // Start is called before the first frame update
    void Start()
    {
        fire = gameObject.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Spawn_Enemies ()
    {
        for (int i = 0; i < enemiesToSpawn.Length; i++)
        {
            GameObject enemy = Instantiate(enemiesToSpawn[i], spawnPoint.position, Quaternion.identity);

            if (isBurning)
            {
                if (houseType == HouseType.Normal)
                {
                    enemy.GetComponentInChildren<Enemy>().health -= healthDrop;
                }

                else if (houseType == HouseType.Chocolate)
                {
                    enemy.GetComponentInChildren<Enemy>().moveSpeed -= speedDrop;
                }
            }

            yield return new WaitForSeconds(2.5f);
        }

        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boomerang"))
        {
            fire.SetActive(true);

            if (!isEmpty)
            {
                Debug.Log("Hit by fire");
                isEmpty = true;
                isBurning = true;
                StartCoroutine(Spawn_Enemies());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isEmpty)
            {
                Debug.Log("Spread out and attack");
                isEmpty = true;
                StartCoroutine(Spawn_Enemies());
            }
        }
    }
}
