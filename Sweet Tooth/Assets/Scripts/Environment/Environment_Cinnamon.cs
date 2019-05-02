using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_Cinnamon : MonoBehaviour
{
    public float burningTime;

    [SerializeField] private AnimationClip dyingTime;

    private bool isBurning;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Set_Alight ()
    {
        if (gameObject.transform.GetChild(0).gameObject != null)
        {
            isBurning = true;
            gameObject.GetComponent<Animator>().SetTrigger("burn");
            Debug.Log("Catch Fire");
            yield return new WaitForSeconds(burningTime);
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("isDying");
            yield return new WaitForSeconds(dyingTime.averageDuration);
            Destroy(gameObject);
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isBurning)
        {
            if (collision.gameObject.CompareTag("Boomerang") || collision.gameObject.CompareTag("Fire"))
            {
                StartCoroutine(Set_Alight());
            }
        }
    }
}
