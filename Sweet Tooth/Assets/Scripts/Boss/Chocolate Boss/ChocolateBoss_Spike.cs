using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocolateBoss_Spike : MonoBehaviour
{
    [SerializeField] private ChocolateBossScriptableObject values;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KillIt ()
    {
        Destroy(gameObject.transform.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Hurt_Player(values.spikeDamage);
        }
    }
}
