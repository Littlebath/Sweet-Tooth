using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The script to identify what checkpoint and to teleport the player to that specific checkpoint
public class CheckPointHandler : MonoBehaviour 
{

	[SerializeField] private GameObject [] checkpoints; 

	public Vector3[] cPosition;

	private GameObject player; 


	// Use this for initialization

	void Start ()
	{

        checkpoints = new GameObject[gameObject.transform.childCount];

        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i] = gameObject.transform.GetChild(i).gameObject;
        }

        player = GameObject.FindGameObjectWithTag("Player");

        cPosition = new Vector3[checkpoints.Length];

        for (int i = 0; i < checkpoints.Length; i++)
        {
            cPosition[i] = checkpoints[i].transform.position;
        }
    }
	
	// Update is called once per frame
	void Update () 
	{

	}

	//This is the function used in the CheckPoint Script. It turns the checkpoint last touched into used and all other previous checkpoints into active. 

	public void updateCheckPoint (GameObject curCheck)
	{
		foreach (GameObject cp in checkpoints) 
		{
			//if (cp.GetComponent<Checkpoints> ().status != Checkpoints.state.Inactive) 
			if (cp.GetComponent<Checkpoints>().status != Checkpoints.state.Inactive)
			{
				cp.GetComponent<Checkpoints> ().status = Checkpoints.state.Used;
			}

		}
		curCheck.GetComponent<Checkpoints>().status = Checkpoints.state.Active; 
	}

	public void Teleport ()
	{
		foreach (GameObject cp in checkpoints) 
		{
			if (cp.GetComponent<Checkpoints> ().status == Checkpoints.state.Active) 
			{
				player.transform.position = cp.transform.position; 
			} 
		}

	}


}
