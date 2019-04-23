using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings_Controller : MonoBehaviour
{
    //Floats
    private float selector;

    //References
    private Animator anim; 

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Selector_Controller();
        SetAnimations();
    }

    void SetAnimations ()
    {
        anim.SetFloat("Selector", selector);
    }

    private void Selector_Controller ()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                Debug.Log("MoveUp");
                selector--;
            }

            else if (Input.GetAxis("Vertical") < 0)
            {
                Debug.Log("MoveDown");
                selector++;
            }
        }
    }
}
