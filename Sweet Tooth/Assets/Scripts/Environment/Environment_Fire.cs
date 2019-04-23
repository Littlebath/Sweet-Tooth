using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_Fire : MonoBehaviour
{
    [SerializeField] private float radiusOfCollider;
    [SerializeField] private LayerMask whatIsBurnables;

    private bool isBurningOthers;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Begin_Burning_Others());
        Debug.Log("Run");
    }

    // Update is called once per frame
    void Update()
    {
        if (isBurningOthers)
        {
            StartCoroutine(Set_Items_Alight());
        }
    }

    private IEnumerator Begin_Burning_Others ()
    {
        yield return new WaitForSeconds(GetComponentInParent<Environment_Cinnamon>().burningTime - 0.5f);
        Debug.Log("Run Code");
        isBurningOthers = true;
    }

    private IEnumerator Set_Items_Alight ()
    {
        isBurningOthers = false;
        Collider2D[] objectsToBurn = Physics2D.OverlapCircleAll(transform.position, radiusOfCollider, whatIsBurnables);
        Debug.Log("Collider Created");

        for (int i = 0; i < objectsToBurn.Length; i++)
        {
            //Cinnamon
            if (objectsToBurn[i].gameObject.CompareTag("Cinnamon"))
            {
                Debug.Log("Burning Cinnamon");
                StartCoroutine(objectsToBurn[i].gameObject.GetComponent<Environment_Cinnamon>().Set_Alight());
            }

            //Nuts
            if (objectsToBurn[i].gameObject.layer == 17)
            {
                yield return new WaitForSeconds(1.5f);
                StartCoroutine(objectsToBurn[i].GetComponent<Environment_ExplosiveNut>().Explode());
            }

            //Rasgulla
            if (objectsToBurn[i].gameObject.layer == 16)
            {
                yield return new WaitForSeconds(1.5f);
                StartCoroutine(objectsToBurn[i].GetComponent<Environment_Metal>().Metal_Process());
            }
        }

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusOfCollider);
    }
}
