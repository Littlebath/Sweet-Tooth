using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Light2D;

public class Environment_TorchController : MonoBehaviour
{
    private Color originalColor;

    public bool isLit;

    [SerializeField] private GameObject mainLight;

    [SerializeField] private GameObject allTorches;

    [SerializeField] private float timer = 0.2f;

    private float counter;

    private int torch;

    // Start is called before the first frame update
    void Start()
    {
        originalColor = gameObject.GetComponent<SpriteRenderer>().color;
        torch = 0;
        counter = timer;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Animator>().SetBool("isLit", isLit);

        Set_Alight();
    }

    void Set_Alight ()
    {
        if (isLit)
        {
            if (torch < allTorches.transform.childCount)
            {
                allTorches.transform.GetChild(torch).GetComponent<Environment_TorchUnit>().isLit = true;
            }

            if (counter <= 0)
            {
                torch++;
                counter = timer;
                Debug.Log("Light another torch");
            }

            else
            {
                counter -= Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boomerang"))
        {
            Debug.Log("Light up");
            isLit = true;
        }
    }
}

