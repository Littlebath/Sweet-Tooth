using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Light2D;

public class Environment_TorchController : MonoBehaviour
{
    private Color originalColor;

    [SerializeField] private bool isLit;

    [SerializeField] private GameObject mainLight;

    [SerializeField] private GameObject allTorches;

    // Start is called before the first frame update
    void Start()
    {
        originalColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Animator>().SetBool("isLit", isLit);
    }

    public IEnumerator Light_Torches()
    {
        isLit = true;
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < allTorches.transform.childCount; i++)
        {
            allTorches.transform.GetChild(i).GetComponent<Environment_TorchUnit>().isLit = true;
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boomerang"))
        {
            Debug.Log("Light up");
            StartCoroutine(Light_Torches());
        }
    }
}

