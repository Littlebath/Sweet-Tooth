using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged_Bullet : MonoBehaviour
{
    [HideInInspector] public int damage;

    [SerializeField] bool isBossBullet;

    private Color[] rangeOfColors;

    private Color newColor;
    // Start is called before the first frame update
    void Start()
    {

        rangeOfColors = new Color[4];

        rangeOfColors[0] = Color.red;
        rangeOfColors[1] = Color.blue;
        rangeOfColors[2] = Color.yellow;
        rangeOfColors[3] = Color.green;

        newColor = rangeOfColors[Random.Range(0, 3)];
        gameObject.GetComponent<SpriteRenderer>().color = newColor;
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

            if (collision.gameObject.GetComponent<NPC>() != null)
            {
                //StartCoroutine(collision.gameObject.GetComponent<NPC>().Flash());
            }

            if (collision.gameObject.name != "Full Level" || collision.gameObject.tag != "Boss")
            {
                Destroy(gameObject);
            }

            //Debug.Log(collision.gameObject.name);
        }
    }
}
