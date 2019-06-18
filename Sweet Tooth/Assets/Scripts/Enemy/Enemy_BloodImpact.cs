using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BloodImpact : MonoBehaviour
{
    public GameObject[] blood;
    public GameObject effect;
    public Color bloodColor;

    public void SpawnBlood ()
    {
        int rng = Random.Range(0, blood.Length - 1);
        GameObject effect = Instantiate(blood[rng], transform.position, Quaternion.identity);
        effect.GetComponent<SpriteRenderer>().color = bloodColor;
    }

    public void SpawnEffect ()
    {
        Debug.Log("Spawn particles");
        //Instantiate(effect, transform.position, Quaternion.identity);
    }
}
