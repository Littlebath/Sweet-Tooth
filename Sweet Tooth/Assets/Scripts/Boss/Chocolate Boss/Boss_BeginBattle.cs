using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_BeginBattle : MonoBehaviour
{
    public GameObject doors;

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
            //Debug.Log("Begin Battle");
            doors.SetActive(true);
            gameObject.transform.parent.GetComponent<Animator>().SetTrigger("begin");
            Destroy(gameObject);
        }
    }
}
