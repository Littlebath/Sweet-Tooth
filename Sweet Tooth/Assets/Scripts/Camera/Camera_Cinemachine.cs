using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera_Cinemachine : MonoBehaviour
{
    private CinemachineVirtualCamera cvc;

    // Start is called before the first frame update
    void Start()
    {
        cvc = gameObject.GetComponent<CinemachineVirtualCamera>();

        cvc.Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
