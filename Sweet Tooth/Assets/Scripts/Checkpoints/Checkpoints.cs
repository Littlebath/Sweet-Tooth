using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to be added to the a checkpoint prefab
public class Checkpoints : MonoBehaviour 
{
	
	public enum state {Inactive, Active, Used, Locked}; 
	public state status; 
	public Sprite [] sprites;

    private string exitPoint;
    private PlayerController pc;


    private GameObject Player;
    private CheckPointHandler ch;

    [HideInInspector]
	public int trueState;



	// Use this for initialization
	void Start () 
	{
        pc = FindObjectOfType<PlayerController>();
        ch = FindObjectOfType<CheckPointHandler>();
        exitPoint = gameObject.name;
	}

	void Update ()
	{
		ChangeColor ();
	}
	
	// This function changes the sprite of the Checkpoint. (Has to be modified if animations need to be implemented.)
	public void ChangeColor () 
	{
		if (status == state.Inactive) 
		{
			GetComponent<SpriteRenderer> ().sprite = sprites [0];
		}

		else if (status == state.Active) 
		{
			GetComponent<SpriteRenderer> ().sprite = sprites [1];
		}

		else if (status == state.Used) 
		{
			GetComponent<SpriteRenderer> ().sprite = sprites [2];
		}

		else if (status == state.Locked) 
		{
			GetComponent<SpriteRenderer> ().sprite = sprites [3];
		}

						

	}
	//The collision detector for the checkpoint and the player
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			ChangeColor (); 
			ch.updateCheckPoint (this.gameObject);

            if (pc != null)
            {
                pc.startPoint = exitPoint;
            }

        }
    }

}
