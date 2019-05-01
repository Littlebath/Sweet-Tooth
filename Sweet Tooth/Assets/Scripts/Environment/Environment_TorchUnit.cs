using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_TorchUnit : MonoBehaviour
{
    public bool isLit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Animator>().SetBool("isLit", isLit);
    }
}
