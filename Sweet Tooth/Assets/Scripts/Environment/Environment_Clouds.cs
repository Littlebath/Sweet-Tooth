using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_Clouds : MonoBehaviour
{
    public float speed;
    private Vector3 origin;
    private Vector3 target;
    private bool isMovingRight;

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Change();
        Movement();
    }

    void SwitchBool ()
    {
        isMovingRight = !isMovingRight;
    }

    void Movement ()
    {
        if (isMovingRight)
        {
            //Go Right
            target = origin + Vector3.right;
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        else
        {
            target = origin + Vector3.left;
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }

    void Change ()
    {
        if (Vector3.Distance (transform.position, target) <= 0.1f)
        {
            SwitchBool();
        }
    }
}
