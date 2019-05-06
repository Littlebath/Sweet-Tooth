using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector]
    public float horizontalInput;
    [HideInInspector]
    public float verticalInput;
    [HideInInspector]
    public bool mapButton;
    [HideInInspector]
    public bool inventoryButton;
    [HideInInspector]
    public bool interactButton;
    [HideInInspector]
    public bool attackButton;
    [HideInInspector]
    public bool dashButton;
    [HideInInspector]
    public bool meleeButton;
    [HideInInspector]
    public bool dashToBoomerangButton;
    [HideInInspector]
    public bool spinAttackButton;

	// Use this for initialization
	void Start ()
    {
        //FindObjectOfType<Manager_AudioManager>().Stop();
        //FindObjectOfType<Manager_AudioManager>().Play("Theme");

        if (FindObjectOfType<PlayerController>() != null)
        {
            FindObjectOfType<PlayerController>().enabled = true;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        MovementInput();
        InteractionButtons();
	}

    void MovementInput ()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void InteractionButtons ()
    {
        mapButton = Input.GetButtonDown("Map");
        inventoryButton = Input.GetButtonDown("Inventory");
        interactButton = Input.GetButtonDown("Interact");
        attackButton = Input.GetButtonDown("Attack");
        dashButton = Input.GetButtonDown("Dash");
        meleeButton = Input.GetButtonDown("Melee");
        dashToBoomerangButton = Input.GetButtonDown("DashToBoomerang");
        spinAttackButton = Input.GetButton("SpinAttack");
    }
}
