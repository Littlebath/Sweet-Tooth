using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_Metal : MonoBehaviour
{
    [SerializeField] private Vector3 newSize;
    [SerializeField] private float timeToShrink;

    public GameObject effect;

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
            Instantiate(effect, transform.position, Quaternion.identity);
            canBeHeated = false;
            Debug.Log("Pulse");
            float t = 0;
            Vector3 orignalSize = transform.localScale;
            

            for (int i = 0; i < 1 / 0.3; i++)
            {
                gameObject.transform.localScale = Vector3.Lerp(transform.localScale, newSize, t);
                yield return new WaitForSeconds(0.05f);
                t += 0.5f;
            }

            yield return new WaitForSeconds(timeToShrink);

            t = 0;

            for (int i = 0; i < 1 / 0.3f; i++)
            {
                gameObject.transform.localScale = Vector3.Lerp(transform.localScale, orignalSize, t);
                yield return new WaitForSeconds(0.05f);
                t += 0.5f;
            }
            //gameObject.transform.localScale = newSize;
            //yield return new WaitForSeconds(timeToShrink);
            //gameObject.transform.localScale = originalSize;
            canBeHeated = true;
        }
    }
}
