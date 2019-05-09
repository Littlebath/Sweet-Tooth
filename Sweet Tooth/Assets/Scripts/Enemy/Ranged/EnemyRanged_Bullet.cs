using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged_Bullet : MonoBehaviour
{
    [HideInInspector] public int damage;

    [SerializeField] bool isBossBullet;
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
        if (!collision.isTrigger)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                FindObjectOfType<PlayerController>().Hurt_Player(damage); 

                if (isBossBullet)
                {
                    FindObjectOfType<PlayerController>().isSlow = true;
                }
            }

            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().Take_Damage(damage);
            }

            if (collision.gameObject.name != "Full Level" || collision.gameObject.tag != "Boss")
            {
                Destroy(gameObject);
            }

            //Debug.Log(collision.gameObject.name);
        }
    }
}
