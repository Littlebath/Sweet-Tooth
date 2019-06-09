using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_Metal : MonoBehaviour
{
    [SerializeField] private Vector3 newSize;
    [SerializeField] private float timeToShrink;

    private bool canBeHeated = true;
    private Vector3 originalSize; 

    // Start is called before the first frame update
    void Start()
    {
        originalSize = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag ("Boomerang"))
        {
            StartCoroutine(Metal_Process());
        }

        if (collision.gameObject.GetComponent<pot>() != null)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(10000F, 0F));
        }

    }

    public IEnumerator Metal_Process ()
    {
        if (canBeHeated)
        {
            canBeHeated = false;
            gameObject.transform.localScale = newSize;
            yield return new WaitForSeconds(timeToShrink);
            gameObject.transform.localScale = originalSize;
            canBeHeated = true;
        }
    }
}
